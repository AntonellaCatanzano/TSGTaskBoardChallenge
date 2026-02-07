using Microsoft.AspNetCore.Mvc;
using TSGTaskBoard.Domain.DTO;
using TSGTaskBoard.UI.Models;
using TSGTaskBoard.UI.Services.Interfaces;

namespace TSGTaskBoard.UI.Controllers
{
    public class TareasController : Controller
    {
        private readonly ITareaHttpService _tareaService;

        public TareasController(ITareaHttpService tareaService)
        {
            _tareaService = tareaService;
        }

        // =====================================================
        // Index: Tablero Kanban
        // =====================================================
        public async Task<IActionResult> Index()
        {
            var tareasDto = await _tareaService.GetAllAsync();

            var tareasVm = tareasDto.Select(t => new TareaViewModel
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Categoria = t.Categoria,
                Estado = t.Estado,
                FechaInicio = t.FechaInicio,
                FechaVencimiento = t.FechaVencimiento
            }).ToList();

            return View(tareasVm);
        }

        // =====================================================
        // Create: GET
        // =====================================================
        public IActionResult Create() => View();

        // =====================================================
        // Create: POST
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> Create(TareaCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new TareaCreateDTO
            {
                Titulo = vm.Titulo,
                Descripcion = vm.Descripcion,
                Categoria = vm.Categoria,
                FechaVencimiento = vm.FechaVencimiento
            };

            await _tareaService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // Edit: GET
        // =====================================================
        public async Task<IActionResult> Edit(int id)
        {
            var tarea = await _tareaService.GetByIdAsync(id);
            if (tarea == null) return NotFound();

            var vm = new TareaUpdateViewModel
            {
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                Categoria = tarea.Categoria,
                FechaVencimiento = tarea.FechaVencimiento
            };

            return View(vm);
        }

        // =====================================================
        // Edit: POST
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TareaUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new TareaUpdateDTO
            {
                Titulo = vm.Titulo,
                Descripcion = vm.Descripcion,
                Categoria = vm.Categoria,
                FechaVencimiento = vm.FechaVencimiento
            };

            await _tareaService.UpdateAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // Delete
        // =====================================================
        public async Task<IActionResult> Delete(int id)
        {
            await _tareaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // Cambiar Estado: GET
        // =====================================================
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var tarea = await _tareaService.GetByIdAsync(id);
            if (tarea == null) return NotFound();

            var vm = new CambiarEstadoTareaViewModel { Estado = tarea.Estado };
            ViewBag.TareaId = id;
            return View(vm);
        }

        // =====================================================
        // Cambiar Estado: POST
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, CambiarEstadoTareaViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new CambiarEstadoTareaDTO { Estado = vm.Estado };
            await _tareaService.CambiarEstadoAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // Cambiar Estado vía AJAX (drag & drop Kanban)
        // =====================================================
        [HttpPost]
        public async Task<IActionResult> CambiarEstadoAjax(int id, [FromBody] CambiarEstadoTareaViewModel vm)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos inválidos");

            var dto = new CambiarEstadoTareaDTO { Estado = vm.Estado };

            try
            {
                await _tareaService.CambiarEstadoAsync(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
