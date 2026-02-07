
using Microsoft.Extensions.DependencyInjection;
using TSGTaskBoard.Repositories.Implementations;
using TSGTaskBoard.Repositories.Implementations.UoW;
using TSGTaskBoard.Repositories.Interfaces;
using TSGTaskBoard.Repositories.Interfaces.UoW;

namespace TSGTaskBoard.Repositories.Support
{
    public static class Setup
    {
        ///<summary>
        ///Método extensivo para la Configuración de Repositorios
        ///</summary>
        ///<param name="services"></param>
        ///<param name="configuration"></param>
        ///<returs></returs>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITareaRepository, TareaRepository>();

            return services;
        }
    }
}
