using Blazored.Modal;
using BlazorWasmUI.Services;
using BlazorWasmUI.Services.Bases;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWasmUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            #region IoC Container
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IOyunService, OyunService>();
            builder.Services.AddScoped<IYapimciService, YapimciService>();
            builder.Services.AddScoped<IHakkindaService, HakkindaService>();
            builder.Services.AddScoped<IYorumService, YorumService>();

            builder.Services.AddBlazoredModal();
            #endregion

            await builder.Build().RunAsync();
        }
    }
}
