using AutoMapper;
using TSGTaskBoard.Domain.DTO;
using TSGTaskBoard.Domain.Entities;
using TSGTaskBoard.Domain.Enums;
using TSGTaskBoard.Repositories.Interfaces.UoW;
using TSGTaskBoard.Services.Interfaces;
using TSGTaskBoard.Domain.Rules;

namespace TSGTaskBoard.Services.Implementations
{
    public class TareaService : ITareaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TareaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TareaDTO>> GetAllTareasAsync()
        {
            var tareas = await _unitOfWork.TareaRepository.GetAllAsync();

            return _mapper.Map<List<TareaDTO>>(tareas);
        }

        public async Task<TareaDTO?> GetTareaByIdAsync(int id)
        {
            var tarea = await _unitOfWork.TareaRepository.GetByIdAsync(id);

            return tarea == null ? null : _mapper.Map<TareaDTO>(tarea);
        }

        public async Task<TareaDTO> AddTareaAsync(TareaCreateDTO dto)
        {
            if (dto.FechaVencimiento.HasValue && dto.FechaVencimiento < DateTime.Today)
                throw new ArgumentException("La fecha de vencimiento no puede ser menor a hoy.");

            var tarea = _mapper.Map<Tarea>(dto);

            tarea.Estado = EstadoTareaEnum.Backlog; 
            tarea.FechaInicio = DateTime.Today;

            await _unitOfWork.TareaRepository.InsertAsync(tarea);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TareaDTO>(tarea);
        }

        public async Task<TareaDTO?> UpdateTareaAsync(int id, TareaUpdateDTO dto)
        {
            var tarea = await _unitOfWork.TareaRepository.GetByIdAsync(id);

            if (tarea == null)
                return null;
           
            if (dto.Titulo != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Titulo))
                    throw new ArgumentException("El título no puede estar vacío.");

                tarea.Titulo = dto.Titulo;
            }
           
            tarea.Descripcion = dto.Descripcion ?? tarea.Descripcion;
            tarea.Categoria = dto.Categoria ?? tarea.Categoria;
            
            if (dto.FechaVencimiento.HasValue)
            {
                if (dto.FechaVencimiento < tarea.FechaInicio)
                    throw new ArgumentException(
                        "La fecha de vencimiento no puede ser menor a la fecha de inicio.");

                tarea.FechaVencimiento = dto.FechaVencimiento;
            }

            await _unitOfWork.TareaRepository.UpdateAsync(tarea);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TareaDTO>(tarea);
        }

        public async Task<bool> DeleteTareaAsync(int id)
        {
            var tarea = await _unitOfWork.TareaRepository.GetByIdAsync(id);

            if (tarea == null) return false;

            await _unitOfWork.TareaRepository.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        // Cambia el estado de Avance de una tarea
        public async Task CambiarEstadoAsync(int tareaId, string estado)
        {
            var tarea = await _unitOfWork.TareaRepository.GetByIdAsync(tareaId);

            if (tarea == null)
                throw new KeyNotFoundException("La tarea no existe.");

            var nuevoEstado = EstadoTareaParser.ToEnum(estado);

            if (!EstadoTareaRules.PuedeTransicionar(tarea.Estado, nuevoEstado))
                throw new InvalidOperationException($"No se puede cambiar el estado de {tarea.Estado} a {nuevoEstado}");

            tarea.Estado = nuevoEstado;

            await _unitOfWork.TareaRepository.UpdateAsync(tarea);

            await _unitOfWork.SaveChangesAsync();
        }

        public List<EstadoTareaDTO> GetEstados()
        {
            return Enum.GetValues<EstadoTareaEnum>()
                .Select(e => new EstadoTareaDTO
                {
                    Key = e.ToString(),
                    Value = (int)e
                })
                .ToList();
        }

        public async Task<List<string>> GetEstadosPosiblesAsync(int tareaId)
        {
            var tarea = await _unitOfWork.TareaRepository.GetByIdAsync(tareaId);

            if (tarea == null)
                throw new KeyNotFoundException("La tarea no existe.");

            var reglasEstado = EstadoTareaRules
                .GetTransiciones(tarea.Estado)
                .Select(e => e.ToString())
                .ToList();

            return reglasEstado;
        }

    }
}
