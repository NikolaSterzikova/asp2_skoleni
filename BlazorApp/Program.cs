using BlazorApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents() // Blazoru se øíká Razor Components
    .AddInteractiveServerComponents(); // kód se vykonává na serveru

builder.Services.AddScoped<BlazorApp.Services.SimpleCounter>(); // pøidání služby pro poèítadlo
builder.Services.AddScoped<BlazorApp.Data.DataSet>(); // pøidání služby pro data
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

// zapínáme routování pro Razor Components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
