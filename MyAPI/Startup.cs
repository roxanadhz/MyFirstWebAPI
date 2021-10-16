using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Middlewares;

namespace MyAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

                services.AddControllers();
                services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
        }
       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomExceptionHandler();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
               {
                   endpoints.MapControllerRoute(name: "default", pattern: "api/{controller=Home}/{action=Index}/{id?}");
               });
        }
    }
}
