using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//2Tutorial
using SignalrDemo.EFModels;
using SignalrDemo.HubConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SignalrDemo.Controllers;

namespace SignalrDemo
{
    public class Startup
    {
        //2Tutorial
        public IConfiguration Configuration { get; }

        //2Tutorial
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();
            //2Tutorial
            services.AddDbContextPool<ChatAppContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"))
            );
        //    services.AddDbContext<SignalrContext>(
        //options => options.UseSqlServer(Configuration.GetConnectionString("AzureDb")));

            //1Tutorial
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

            //1Tutorial
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            //1Tutorial
            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //1Tutorial
            app.UseCors("AllowAllHeaders");


            //1Tutorial
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              endpoints.MapHub<MyHub>("/toastr");
             // endpoints.MapHub<ChatController>("/toastr");
            });
        }
    }
}
