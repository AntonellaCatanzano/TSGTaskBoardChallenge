using TSGTaskBoard.UI.Services.Support;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Leer URL desde configuración
var apiUrl = builder.Configuration["ApiSettings:BaseUrl"];

// Registrar servicios de UI
builder.Services.AddHttpServices(apiUrl);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
