using Microsoft.Extensions.Options;
using TSGTaskBoard.API.Support.Extensions;
using TSGTaskBoard.API.Support.Logging;
using TSGTaskBoard.API.Support.Middleware;
using TSGTaskBoard.DataAccess;
using TSGTaskBoard.DataAccess.Support;
using TSGTaskBoard.Domain.Support;
using TSGTaskBoard.Repositories.Support;
using TSGTaskBoard.Services.Support;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ---------- LOGGING ----------
builder.Services.AddCustomLogging(builder.Configuration);

// ---------- CONTROLLERS ----------
builder.Services.AddControllers();

// ---------- SWAGGER ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomizedSwagger();

// ---------- DATA ACCESS ----------
builder.Services.AddCustomizedDatabase(builder.Configuration);

// ---------- DOMAIN ----------
builder.Services.AddEntitiesMappings();

// ---------- REPOSITORIES ----------
builder.Services.AddRepositories();

// ---------- SERVICES ----------
builder.Services.AddServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
/* if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} */

// ---------- SWAGGER ----------
app.UseTSGTaskBoardSwagger();

// ---------- GLOBAL EXCEPTION HANDLING ----------
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
