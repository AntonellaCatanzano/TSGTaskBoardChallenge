using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TSGTaskBoard.UI.Services.Implementations;
using TSGTaskBoard.UI.Services.Interfaces;

namespace TSGTaskBoard.UI.Services.Support
{
    public static class Setup 
    { 
        public static IServiceCollection AddHttpServices(this IServiceCollection services, string apiBaseUrl)
        { 
            void AddHttpClientWithBase<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface 
            {
                services.AddHttpClient<TInterface, TImplementation>(client => { client.BaseAddress = new Uri(apiBaseUrl); }); 
            }

            AddHttpClientWithBase<ITareaHttpService, TareaHttpService>();
                
                return services; 
        } 
    }
}
