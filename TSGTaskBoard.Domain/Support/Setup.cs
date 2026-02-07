using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TSGTaskBoard.Domain.Profiles;

namespace TSGTaskBoard.Domain.Support
{
    public static class Setup
    {
        public static IServiceCollection AddEntitiesMappings(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new TareaProfile());                
            });

            IMapper mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
    
}
