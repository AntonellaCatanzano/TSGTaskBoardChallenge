using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TSGTaskBoard.Domain.DTO;
using TSGTaskBoard.Services.Interfaces;

namespace TSGTaskBoard.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly ITareaService _tareaService;

        public TareasController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        // =====================================================
        // GET: api/tareas
        // =====================================================
        /// <summary>
        /// Obtiene todas las tareas.
        /// </summary>
        /// <returns>Listado completo de tareas.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<TareaDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tareas = await _tareaService.GetAllTareasAsync();

            return Ok(tareas);
        }

        // =====================================================
        // GET: api/tareas/{id}
        // =====================================================
        /// <summary>
        /// Obtiene una tarea por su id.
        /// </summary>
        /// <param name="id">Identificador único de la tarea.</param>
        /// <returns>Obtiene la tarea solicitada si existe o 404 si la tarea no existe.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TareaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var tarea = await _tareaService.GetTareaByIdAsync(id);

            if (tarea == null)
            {
                return NotFound(new
                {
                    message = $"La tarea con id {id} no existe"
                });
            }

            return Ok(tarea);
        }

        // =====================================================
        // POST: api/tareas/create
        // =====================================================
        /// <summary>
        /// Crea una nueva tarea.
        /// </summary>
        /// <remarks>
        /// Estados posibles:
        /// - Backlog
        /// - ToDo
        /// - InProgress
        /// - Done
        /// </remarks>
        /// <param name="dto">Datos de la nueva tarea que vamos a crear.</param>
        /// <returns>Tarea creada.</returns>
        [HttpPost("create")]
        [ProducesResponseType(typeof(TareaDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] TareaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tarea = await _tareaService.AddTareaAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = tarea.Id },
                tarea
            );
        }

        // =====================================================
        // PUT: api/tareas/update/{id}
        // =====================================================
        /// <summary>
        /// Actualiza los datos de una tarea existente.
        /// </summary>
        /// <remarks>
        /// Permite actualizar uno o más campos.
        /// El estado no se modifica por este endpoint.
        /// </remarks>
        /// <param name="id">Identificador de la tarea.</param>
        /// <param name="dto">Datos a actualizar.</param>
        /// <returns>Tarea actualizada si existe o 404 si no existe.</returns>
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(typeof(TareaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] TareaUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _tareaService.UpdateTareaAsync(id, dto);

            if (updated == null)
            {
                return NotFound(new
                {
                    message = $"La tarea con id {id} no existe"
                });
            }

            return Ok(updated);
        }

        // =====================================================
        // DELETE: api/tareas/delete/{id}
        // =====================================================
        /// <summary>
        /// Elimina una tarea.
        /// </summary>
        /// <param name="tarea">Entidad tarea a eliminar.</param>

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _tareaService.DeleteTareaAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = $"La tarea con id {id} no existe"
                });
            }

            return NoContent();
        }

        // =====================================================
        // PATCH: api/tareas/{id}/estado
        // =====================================================

        [HttpPatch("{id:int}/estados-tarea")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoTareaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _tareaService.CambiarEstadoAsync(id, dto.Estado);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // =====================================================
        // GET: api/tareas/estados
        // =====================================================
        /// <summary>
        /// Obtiene todos los estados posibles de una tarea.
        /// </summary>
        /// <returns>Listado completo de los estados de una tarea.</returns>
        [HttpGet("estados")]
        [ProducesResponseType(typeof(List<EstadoTareaDTO>), StatusCodes.Status200OK)]
        public IActionResult GetEstados()
        {
            return Ok(_tareaService.GetEstados());
        }

        // =====================================================
        // GET: api/tareas/transicion-estados-posibles
        // =====================================================
        /// <summary>
        /// Obtiene las transiciones posibles de cada estado en los que puede estar una tarea.
        /// </summary>
        [HttpGet("{id:int}/transicion-estados-posibles")]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEstadosPosibles(int id)
        {
            try
            {
                var estados = await _tareaService.GetEstadosPosiblesAsync(id);
                return Ok(estados);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    message = $"La tarea con id {id} no existe"
                });
            }
        }
    }
}
