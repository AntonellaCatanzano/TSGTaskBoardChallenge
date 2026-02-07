
using TSGTaskBoard.Domain.Entities;

namespace TSGTaskBoard.Repositories.Interfaces
{
    public interface ITareaRepository
    {
        Task<List<Tarea>> GetAllAsync();
        Task<Tarea?> GetByIdAsync(int id);
        Task InsertAsync(Tarea entity);
        Task UpdateAsync(Tarea entity);
        Task<bool> DeleteAsync(int id);
    }
}
