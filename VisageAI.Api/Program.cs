using Microsoft.EntityFrameworkCore;
using VisageAI.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Cargar variables de entorno explícitamente (además de appsettings.json)
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// 🔹 Obtener la cadena de conexión desde variables de entorno
var connectionString = builder.Configuration.GetConnectionString("AivenMySQL");

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("⚠️ Advertencia: No se encontró la cadena de conexión 'AivenMySQL'.");
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
}

// 🔹 Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "https://visageai-frontend.onrender.com",
            "http://localhost:5173") // Ajusta según tu frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();

// 🔹 Render asigna el puerto dinámicamente, así que hay que leerlo desde la variable de entorno
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

Console.WriteLine($"🚀 Servidor iniciado en el puerto {port}");

// 🔹 Ejecutar la aplicación
app.Run();