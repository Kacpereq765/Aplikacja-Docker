using BlazorApp2.Components;
using BlazorApp2.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Blazor Server / Interactive
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// PostgreSQL (kontener db)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        "Host=db;Port=5432;Database=appdb;Username=user;Password=password"));

var app = builder.Build();

// ===== MIGRACJE Z RETRY (KLUCZ DO DOCKERA) =====
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var retries = 20;

    while (true)
    {
        try
        {
            db.Database.Migrate();
            Console.WriteLine("Migracje OK");
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"Czekam na bazę... zostalo prób: {retries}");

            if (retries == 0)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            Thread.Sleep(2000);
        }
    }
}

// ===== middleware =====
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();