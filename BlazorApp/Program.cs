using BlazorApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents() // Blazoru se ��k� Razor Components
    .AddInteractiveServerComponents(); // k�d se vykon�v� na serveru

builder.Services.AddScoped<BlazorApp.Services.SimpleCounter>(); // p�id�n� slu�by pro po��tadlo
builder.Services.AddScoped<BlazorApp.Data.DataSet>(); // p�id�n� slu�by pro data
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// zap�n�me routov�n� pro Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
