using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SqlOfTheDead.Client.Pages.Table;
using SqlOfTheDead.Models;

namespace SqlOfTheDead.Client;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.Services.AddMudServices();
        builder.Services.AddScoped(sp => new HttpClient()
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });
        builder.Services.AddScoped<IZombieTable, ServiceTableClient>();
        await builder.Build().RunAsync();
    }
}
