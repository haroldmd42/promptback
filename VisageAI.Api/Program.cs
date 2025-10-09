using Microsoft.EntityFrameworkCore;
using VisageAI.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// --- HABILITAR CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// --- SERVICIOS EXISTENTES ---
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
// ...otros servicios que tengas

var app = builder.Build();

// --- USAR CORS ---
app.UseCors("AllowAll");

// --- RESTO DE TU PIPELINE ---
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// (opcional) endpoint raíz de prueba
app.MapGet("/", () => "✅ API de Prompts funcionando correctamente");

app.Run();
