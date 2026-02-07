using Microsoft.EntityFrameworkCore;
using TSGTaskBoard.DataAccess;
using TSGTaskBoard.Domain.Entities;
using TSGTaskBoard.Repositories.Interfaces;

namespace TSGTaskBoard.Repositories.Implementations
{
    public class TareaRepository : ITareaRepository
    {
        private readonly TSGTaskBoardDBContext _dbContext;

        public TareaRepository(TSGTaskBoardDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Tarea>> GetAllAsync()
        {
            return await _dbContext.Tareas.AsNoTracking().ToListAsync();
        }

        public async Task<Tarea?> GetByIdAsync(int id)
        {
            return await _dbContext.Tareas.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task InsertAsync(Tarea entity)
        {
            await _dbContext.Tareas.AddAsync(entity);
        }

        public Task UpdateAsync(Tarea entity)
        {
            _dbContext.Tareas.Update(entity);

            return Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                return false;

            _dbContext.Tareas.Remove(entity);

            return true;
        }
    }
}
