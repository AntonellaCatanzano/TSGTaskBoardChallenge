using TSGTaskBoard.Domain.DTO;

namespace TSGTaskBoard.UI.Services.Interfaces
{
    public interface ITareaHttpService
    {
        Task<List<TareaDTO>> GetAllAsync();
        Task<TareaDTO?> GetByIdAsync(int id);
        Task<TareaDTO> CreateAsync(TareaCreateDTO dto);
        Task<TareaDTO?> UpdateAsync(int id, TareaUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task CambiarEstadoAsync(int id, CambiarEstadoTareaDTO dto);

    }
}
