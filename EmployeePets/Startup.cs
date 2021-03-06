using EmployeePets.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmployeePets
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
            services.AddDbContext<ApplicationContext>(opts=>opts.UseSqlServer(Configuration.GetConnectionString("PetDbConnection")));
            services.AddControllers();
            services.AddSpaStaticFiles(conf => { conf.RootPath = "EmployeePetsApp/dist";});
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            
            app.UseRouting();

            app.UseEndpoints(endp => { endp.MapControllers(); });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "EmployeePetsApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer("start");
                }
            });

            // database initialization
            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var serviceScope = serviceScopeFactory.CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetService<ApplicationContext>();
            dbContext.Database.EnsureCreated();
            if (dbContext.AnimalTypes.Any()) return;
            dbContext.AnimalTypes.AddRange(
                new AnimalType { Name = "Cat", },
                new AnimalType { Name = "Dog",  },
                new AnimalType { Name = "Parrot", },
                new AnimalType { Name = "Hamster", },
                new AnimalType { Name = "Turtle", }
            );
            dbContext.SaveChanges();
        }
    }
}