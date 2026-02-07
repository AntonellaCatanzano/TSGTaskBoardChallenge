using Microsoft.AspNetCore.Mvc;

namespace TSGTaskBoard.API.Support.Extensions
{
    public static class ApiBehaviorExtension
    {
        public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }
    }
    
}
