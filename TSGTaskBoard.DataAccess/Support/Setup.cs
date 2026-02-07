using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TSGTaskBoard.DataAccess.Support
{
    public static class Setup
    {
        /// <summary>
        /// Base de Datos SQLite
        /// </summary>
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TSGTaskBoardDBContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("TSGTaskBoardDB")
                )
            );

            return services;
        }
    }
}
