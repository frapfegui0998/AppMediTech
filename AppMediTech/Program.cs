using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AppMediTech.Datos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a la base de datos SQL Server.
// Asegúrate de tener la cadena de conexión "ConexionSQLLocalDB" en tu archivo appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQLLocalDB")));

// Agrega servicios al contenedor. Esto incluye los controladores con vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    // Usa el manejador de excepciones predeterminado que redirige a /Home/Error en producción
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 días. Puedes necesitar cambiar esto para escenarios de producción.
    app.UseHsts();
}

// Redirecciona automáticamente las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Sirve archivos estáticos (como CSS, JavaScript, imágenes) para tu aplicación.
app.UseStaticFiles();

// Habilita el enrutamiento.
app.UseRouting();

// Habilita la autorización si tu aplicación lo requiere.
app.UseAuthorization();

// Define las rutas predeterminadas para el enrutamiento MVC.
// Esto establece que una URL sin controlador y acción específicos usará "Home" como controlador y "Index" como acción.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia la aplicación.
app.Run();
