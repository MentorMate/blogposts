using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    /// <summary>The application startup class.</summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        public Startup(IConfiguration configuration) =>
            _configuration = configuration;

        /// <summary>This method gets called by the runtime. Use this method to add services to the container.</summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvcWithDefaultRoute();
        }
    }
}