using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using SqlOfTheDead.Client.Pages;
using SqlOfTheDead.Components;
using SqlOfTheDead.Models;
using SqlOfTheDead.Routes;
using SqlOfTheDead.Services;

namespace SqlOfTheDead;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add MudBlazor services
        builder.Services.AddMudServices();

        builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration["ZombieTables:ConnectionString"]));

        builder.Services.AddScoped<IZombieTable, ServiceTableServer>();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.MapGet("api/table", RouteTable.GetTables);
        app.MapGet("api/table/id", RouteTable.GetTableIds);
        app.MapPost("api/table", RouteTable.AddTable);
        app.MapDelete("api/table/{tableId}", RouteTable.DeleteTable);

        app.Run();
    }
}
