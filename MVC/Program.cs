using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddScoped<ISocksService, SocksService>();


var windsorContainer = new WindsorContainer();
// Registrace slu�eb do Windsor kontejneru
windsorContainer.Register(
    Classes.FromAssembly(Assembly.GetExecutingAssembly())
        .BasedOn<IService>()
        .WithService.FromInterface()
        .LifestyleScoped()
);

// Fix: Use the correct constructor for WindsorServiceProviderFactory
builder.Host.UseServiceProviderFactory(new WindsorServiceProviderFactory(windsorContainer));

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Toto je tu kv�li registraci u�ivatel�. .Net je m� vyroben� jako razor pages.
app.MapRazorPages();

app.Run();
