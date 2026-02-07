
using Microsoft.EntityFrameworkCore.Storage;

namespace TSGTaskBoard.Repositories.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        ITareaRepository TareaRepository { get; }

        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
