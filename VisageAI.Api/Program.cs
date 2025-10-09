using Microsoft.EntityFrameworkCore;
using VisageAI.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// --- CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// --- LEER CADENA DESDE VARIABLE DE ENTORNO ---
var connectionString = Environment.GetEnvironmentVariable("AIVEN_MYSQL_CONNECTION");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();

var app = builder.Build();

// --- USAR CORS ---
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Endpoint raíz de prueba
app.MapGet("/", () => "✅ API de Prompts funcionando correctamente");

app.Run();
