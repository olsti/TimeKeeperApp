using MudBlazor.Services;
using TimeKeeperApp.Components;
using TimeKeeperApp.Data;
using TimeKeeperApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Configuration.AddJsonFile("appsettings.json",
                 optional: false,
                 reloadOnChange: true);
builder.Services.AddMudServices();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "timerecord.db";
builder.Services.AddDatabaseMigrationRunner(connectionString);
builder.Services.AddSingleton<DbAcccessFactory>();
builder.Services.AddScoped(sp => sp.GetRequiredService<DbAcccessFactory>().GetDbAcccess());
builder.Services.AddScoped<TimeKeeperService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

using (var ss = app.Services.CreateScope())
{
    DatabaseMigrationRunner.Run(ss.ServiceProvider);
}

app.UseStaticFiles();
app.UseAntiforgery();

// default server interactive
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
