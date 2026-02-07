namespace TSGTaskBoard.API.Support.Logging
{
    public static class LoggerExtension
    {
        public static IServiceCollection AddCustomLogging(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var loggerSettings = configuration
                .GetSection("Logging:AppLogger")
                .Get<LoggerSettings>();

            // 🔒 Protección clave (este era el problema real)
            if (loggerSettings == null ||
                (loggerSettings.Type == LoggerType.FILE &&
                 (string.IsNullOrWhiteSpace(loggerSettings.FolderPath) ||
                  string.IsNullOrWhiteSpace(loggerSettings.FilePath))))
            {
                // Fallback seguro a consola
                services.AddLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                });

                return services;
            }

            services.AddSingleton(loggerSettings);

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddProvider(new LoggerProviderApp(loggerSettings));
            });

            return services;
        }
    }
}
