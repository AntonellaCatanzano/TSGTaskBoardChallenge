using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TSGTaskBoard.DataAccess;
using TSGTaskBoard.Repositories.Interfaces;
using TSGTaskBoard.Repositories.Interfaces.UoW;

namespace TSGTaskBoard.Repositories.Implementations.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TSGTaskBoardDBContext _dbContext;

        public ITareaRepository TareaRepository { get; }

        public UnitOfWork(
            TSGTaskBoardDBContext dbContext,
            ITareaRepository tareaRepository)
        {
            _dbContext = dbContext;
            TareaRepository = tareaRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}
