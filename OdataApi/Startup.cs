using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdataApi.Configs;
using OdataApi.Contexts;
using OdataApi.Services;
using OdataApi.Services.Bases;

namespace OdataApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddControllers();

            services.AddDbContext<DefaultContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddOData();

            #region IoC Container
            services.AddScoped<IOyunService, OyunService>();
            services.AddScoped<IYorumService, YorumService>();
            services.AddScoped<IYapimciService, YapimciService>();
            services.AddScoped<IYapimciOyunYorumService, YapimciOyunYorumService>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // OData get servislerini test edebilmek için ekliyoruz,
                                  // kullanabilmek için de projemize wwwroot klasörünü ekleyip içindeki index.html altýnda test link'lerini yazýyoruz.

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            OdataConfig odataConfig = new OdataConfig(app);
            odataConfig.Configure();
        }
    }
}
