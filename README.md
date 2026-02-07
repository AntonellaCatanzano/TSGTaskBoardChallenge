# TSGTaskBoard

Resumen
-------
TSGTaskBoard es una solución .NET 8 que implementa un Task Tracker ligero con arquitectura en capas. Incluye una API REST documentada con Swagger, una UI basada en Razor (Controllers+Views / Razor Pages compatible) y una capa cliente HTTP para la UI. Está diseñada para separación de responsabilidades, inyección de dependencias y uso de EF Core (SQLite) para persistencia.

Estado
------
- Target: .NET 8
- API: Swagger y middleware de excepciones
- Persistencia: Entity Framework Core (SQLite)
- UI: Razor (Controllers/Views; compatible con Razor Pages y Bootstrap 5.3.8)
- Arquitectura por capas completa

Arquitectura y capas
--------------------
(Backend)

- API 
  - Proyecto: `TSGTaskBoard.API`
  - Responsabilidad: Exponer endpoints HTTP, registrar middleware (ej. `ExceptionMiddleware`) y documentación Swagger.
  - Archivos clave: `TSGTaskBoard.API/Program.cs`, `TSGTaskBoard.API/Support/Extensions/SwaggerExtension.cs`

- Services 
  - Proyecto: `TSGTaskBoard.Services`
  - Responsabilidad: Lógica de negocio consumida por la API.
  - Registro DI: `TSGTaskBoard.Services\Support\Setup.cs` → `AddServices()`

- Repositories
  - Proyecto: `TSGTaskBoard.Repositories`
  - Responsabilidad: Acceso a datos, patrones Repository y UnitOfWork.
  - Registro DI: `TSGTaskBoard.Repositories\Support\Setup.cs` → `AddRepositories()`
 
- Domain 
  - Proyecto: `TSGTaskBoard.Domain`
  - Responsabilidad: Entidades, DTOs y mapeos compartidos entre capas (`TSGTaskBoard.Domain.DTO`).

- DataAccess 
  - Proyecto: `TSGTaskBoard.DataAccess`
  - Responsabilidad: `DbContext`, migraciones EF Core y configuración del proveedor (SQLite).
  - Paquetes: `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`
  - Archivo .csproj: `TSGTaskBoard.DataAccess/TSGTaskBoard.DataAccess.csproj`

(Frontend)

- UI 
  - Proyecto: `TSGTaskBoard.UI`
  - Responsabilidad: Interfaz web que consume la API. Implementado con Controllers + Views (compatible con Razor Pages si se adapta).
  - Archivos clave: `TSGTaskBoard.UI/Program.cs`, controladores en `TSGTaskBoard.UI/Controllers/*`

- Ui.Services 
  - Ubicación: `TSGTaskBoard.UI/Services`
  - Responsabilidad: Cliente HTTP que encapsula llamadas a la API (serialización, rutas, errores mínimos).
  - Interfaz e implementación: `TSGTaskBoard.UI/Services/Interfaces/ITareaHttpService.cs` y `TSGTaskBoard.UI/Services/Implementations/TareaHttpService.cs`
  - Registro: extensión `AddHttpServices(apiUrl)` usada en `TSGTaskBoard.UI/Program.cs`

Integración y registradores DI importantes
-----------------------------------------
- `TSGTaskBoard.API/Program.cs`:
  - `AddCustomLogging`, `AddCustomizedDatabase(configuration)`, `AddEntitiesMappings()`, `AddRepositories()`, `AddServices()`, `AddCustomizedSwagger()`
- `TSGTaskBoard.UI/Program.cs`:
  - `builder.Services.AddControllersWithViews();`
  - `builder.Services.AddHttpServices(apiUrl);` (lee `ApiSettings:BaseUrl`)

Configuración principal
-----------------------
- URL base API (UI): `TSGTaskBoard.UI/appsettings.json` → `ApiSettings:BaseUrl`
- Conexión DB: `TSGTaskBoard.DataAccess` configura la cadena en `appsettings.json` del proyecto API
- Swagger: `TSGTaskBoard.API/Support/Extensions/SwaggerExtension.cs` (inserta CSS/JS custom)

Requisitos previos
------------------
- Visual Studio 2022 (recomendado) o VS Code
- .NET 8 SDK
- (Opcional) SQLite browser para inspeccionar archivos `.db`

Arranque local (rápido)
-----------------------
1. Clona y abre la solución:
   - git clone [<repo>](https://github.com/AntonellaCatanzano/TSGTaskBoardChallenge)
   - Abrir la `.sln` en Visual Studio 2022

2. Restaurar paquetes:
   - __Build > Restore NuGet Packages__ o compilar la solución

3. Configurar proyectos de inicio:
   - Seleccionar `TSGTaskBoard.API` y `TSGTaskBoard.UI` como proyectos de inicio (multi-startup) mediante __Solution Explorer > Set Startup Projects...__ o ejecutar por separado.

4. Configurar DB y migraciones:
   - Abrir __Package Manager Console__ y seleccionar `TSGTaskBoard.DataAccess` como Default Project
     - `Add-Migration InitialCreate` (si procede)
     - `Update-Database`
   - O con EF CLI desde la raíz:
     - dotnet ef migrations add InitialCreate -p TSGTaskBoard.DataAccess -s TSGTaskBoard.API
     - dotnet ef database update -p TSGTaskBoard.DataAccess -s TSGTaskBoard.API

5. Ejecutar:
   - Iniciar `TSGTaskBoard.API` y `TSGTaskBoard.UI`.
   - Swagger UI normalmente en `https://localhost:{PUERTO}/swagger`

Comandos útiles (resumen)
-------------------------
- Compilar solución: __Build > Build Solution__
- Restaurar paquetes: __Build > Restore NuGet Packages__
- EF Migrations (PMC): `Add-Migration` / `Update-Database`
- EF CLI ejemplo: `dotnet ef database update -p TSGTaskBoard.DataAccess -s TSGTaskBoard.API`
