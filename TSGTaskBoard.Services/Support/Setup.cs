using Microsoft.Extensions.DependencyInjection;
using TSGTaskBoard.Services.Implementations;
using TSGTaskBoard.Services.Interfaces;

namespace TSGTaskBoard.Services.Support
{
    public static class Setup
    {
        /// <summary>
        /// Método Extensivo para la configuración de los Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="=configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITareaService, TareaService>();            

            return services;
        }
    }
}
