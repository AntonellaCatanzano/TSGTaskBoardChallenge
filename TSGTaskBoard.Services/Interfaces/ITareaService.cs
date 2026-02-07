using TSGTaskBoard.Domain.DTO;
using TSGTaskBoard.Domain.Enums;

namespace TSGTaskBoard.Services.Interfaces
{
    public interface ITareaService
    {
        Task<List<TareaDTO>> GetAllTareasAsync();
        Task<TareaDTO?> GetTareaByIdAsync(int id);
        Task<TareaDTO> AddTareaAsync(TareaCreateDTO dto);
        Task<TareaDTO?> UpdateTareaAsync(int id, TareaUpdateDTO dto);
        Task<bool> DeleteTareaAsync(int id);
        Task CambiarEstadoAsync(int tareaId, string estado);
        List<EstadoTareaDTO> GetEstados();
        Task<List<string>> GetEstadosPosiblesAsync(int tareaId);
    }
}
