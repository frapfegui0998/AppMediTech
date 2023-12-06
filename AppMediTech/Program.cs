using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AppMediTech.Datos;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de la conexi�n a la base de datos SQL Server.
// Aseg�rate de tener la cadena de conexi�n "ConexionSQLLocalDB" en tu archivo appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQLLocalDB")));

// Agrega servicios al contenedor. Esto incluye los controladores con vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    // Usa el manejador de excepciones predeterminado que redirige a /Home/Error en producci�n
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 d�as. Puedes necesitar cambiar esto para escenarios de producci�n.
    app.UseHsts();
}

// Redirecciona autom�ticamente las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Sirve archivos est�ticos (como CSS, JavaScript, im�genes) para tu aplicaci�n.
app.UseStaticFiles();

// Habilita el enrutamiento.
app.UseRouting();

// Habilita la autorizaci�n si tu aplicaci�n lo requiere.
app.UseAuthorization();

// Define las rutas predeterminadas para el enrutamiento MVC.
// Esto establece que una URL sin controlador y acci�n espec�ficos usar� "Home" como controlador y "Index" como acci�n.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia la aplicaci�n.
app.Run();
