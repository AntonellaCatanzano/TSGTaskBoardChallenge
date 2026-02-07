using Microsoft.OpenApi.Models;

namespace TSGTaskBoard.API.Support.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomizedSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "TSGTaskBoard - Task Tracker Lite - API",
                        Description = "Aplicación de Gestión de Tareas",
                        Version = "v1",
                    });                

                // ---------- XML Documentation ----------
                try
                {
                    string docName = "ServiceDocumentation";
                    var xmlFile = $"{docName}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                    if (File.Exists(xmlPath))
                        options.IncludeXmlComments(xmlPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Swagger XML Error: {ex.Message}");
                }
            });

            return services;
        }

        public static IApplicationBuilder UseTSGTaskBoardSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "TSGTaskBoard - Task Tracker Lite - Swagger UI";
                options.SwaggerEndpoint("../swagger/v1/swagger.json", "TSGTaskBoard.API v1");


                options.InjectStylesheet("../css/style.css");
                options.InjectJavascript("../js/swagger.js", "text/javascript");
            });

            return app;
        }
    }
}
