using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Middleware;
using MVC.Services;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// Pro DbContext a identity slu�by pou��vejte v�dy vestav�n� DI kontejner (builder.Services), ne Windsor.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();



// Vestav�n� DI kontejner
//builder.Services.AddScoped<ISocksService, SocksService>();
// AddScoped - po dobu jednoho HTTP po�adavku
// Addtransient - v r�mci jedn� metody
//builder.Services.AddSingleton<SimpleFileLogger>(); // Singleton - jedna instance pro celou aplikaci

// Fix: Use the correct constructor for WindsorServiceProviderFactory
builder.Host.UseServiceProviderFactory(new WindsorServiceProviderFactory());

builder.Host.ConfigureContainer<IWindsorContainer>(windsor =>
{
    windsor.Register(
        Classes.FromAssembly(Assembly.GetExecutingAssembly())
            .BasedOn<IService>() // Z tohoto rozhran� mus� d�dit ostatn� rozhran�, ne konkrt�nt� t��dy, jako d��ve
            .WithService.FromInterface()
            .LifestyleCustom<MsScopedLifestyleManager>(),//Scope v ASP.NET Core je spravov�n frameworkem, Windsor nev�
                                                         //nic o tom, kdy za��n� scope. Pokud chceme n�co m�t v r�mci scope,
                                                         //pou�ijte spr�vn� lifestyle manager
                                                         // Ale pro v�t�inu b�n�ch sc�n��� v ASP.NET Core sta�� .LifestyleTransient().
        Component.For<SimpleFileLogger>()
            .LifestyleSingleton()
    );
    
});



var app = builder.Build();

// Odtud z�lo�e� na po�ad� p�id�v�n� middlewar�.
// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Vlastn� middleware
app.UseRequestLog();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Toto je tu kv�li registraci u�ivatel�. .Net je m� vyroben� jako razor pages.
app.MapRazorPages();

app.Run();
