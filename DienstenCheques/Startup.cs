using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DienstenCheques.Data;
using DienstenCheques.Services;
using DienstenCheques.Models.Domain;
using DienstenCheques.Filters;
using System.Security.Claims;
using DienstenCheques.Data.Repositories;

namespace DienstenCheques
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            options.AddPolicy("Customer", policy => policy.RequireClaim(ClaimTypes.Role, "Customer")));

            services.AddScoped<GebruikerFilter>();
            services.AddScoped<IGebruikersRepository, GebruikersRepository>();
            services.AddTransient<DienstenChequesInitializer>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DienstenChequesInitializer dienstenChequesInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            dienstenChequesInitializer.InitializeData().Wait();
        }
    }
}
