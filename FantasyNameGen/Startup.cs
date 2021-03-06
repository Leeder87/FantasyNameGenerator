﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FantasyNameGen.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyNameGen
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
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст MobileContext в качестве сервиса в приложение
            //services.AddDbContext<NamesContext>(options =>
            //    options.UseSqlServer(connection));
            services.AddEntityFrameworkNpgsql().AddDbContext<NamesContext>(options => options.UseNpgsql(connection));
            services.AddEntityFrameworkNpgsql().AddDbContext<SurnamesContext>(options => options.UseNpgsql(connection));
            services.AddEntityFrameworkNpgsql().AddDbContext<DeNamesContext>(options => options.UseNpgsql(connection));
            services.AddEntityFrameworkNpgsql().AddDbContext<DeSurnamesContext>(options => options.UseNpgsql(connection));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Names}/{action=GetRandom}/{id?}");
            });
        }
    }
}
