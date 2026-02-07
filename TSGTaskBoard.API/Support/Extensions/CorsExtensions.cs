namespace TSGTaskBoard.API.Support.Extensions
{
    public static class CorsExtension
    {
        private const string DefaultPolicy = "CorsPolicy";

        public static IServiceCollection AddPolicyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultPolicy, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }

        public static string PolicyName => DefaultPolicy;
    }
}
