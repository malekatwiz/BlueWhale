using BlueWhale.Security.Data;
using BlueWhale.Security.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlueWhale.Security
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
            services.ConfigureDbContext();
            services.ConfigureIdentity();

            services.ConfigureAuthentication(Configuration["Authentication:IssuerUrl"],
                Configuration["Authentication:AudienceUrl"],
                Configuration["Authentication:SecretKey"]);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.RegisterServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetService<UsersContext>();
                //DataSeed.SeedTestData(context);

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var user = new User
                {
                    UserName = "Malek",
                    Email = "malek.atwiz@hotmail.com"
                };

                var r = userManager.CreateAsync(user, "@MyPassword1").GetAwaiter();
            }
        }
    }
}
