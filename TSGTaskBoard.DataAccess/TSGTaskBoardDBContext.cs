using Microsoft.EntityFrameworkCore;
using TSGTaskBoard.Domain.Entities;

namespace TSGTaskBoard.DataAccess
{
    public class TSGTaskBoardDBContext : DbContext
    {
        public TSGTaskBoardDBContext(DbContextOptions<TSGTaskBoardDBContext> options) : base(options)
        {
        }

        public DbSet<Tarea> Tareas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }
    }
}
