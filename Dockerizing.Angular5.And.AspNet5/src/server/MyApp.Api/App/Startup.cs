//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using MyApp.Api.Data;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace App
{
    /// <summary>The application startup class.</summary>
    public class Startup
    {
        private const string KeyForUseSwagger = "UseSwagger";
        private const string KeyForDefaultConnection = "DefaultConnection";
        private const string KeyForMigrateDatabaseOnStartups = "MigrateDatabaseOnStartups";
        private const string SwaggerJsonUrl = "v1/swagger.json";

        private readonly IConfiguration _configuration;

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>This method gets called by the runtime. Use this method to add services to the container.</summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddDbContextPool<ApplicationDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString(KeyForDefaultConnection)));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info());
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllEnumsAsStrings();

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, Assembly.GetExecutingAssembly().GetName().Name + ".xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseCors(o => o.AllowAnyOrigin().WithMethods("POST", "DELETE", "GET", "PUT").AllowAnyHeader().AllowCredentials());
            app.UseMvc();
            app.UseStaticFiles();

            if (_configuration.GetValue<bool>(KeyForUseSwagger))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(SwaggerJsonUrl, string.Empty);
                });
            }

            if (_configuration.GetValue<bool>(KeyForMigrateDatabaseOnStartups))
            {
                var contextOptions = app.ApplicationServices.GetService<DbContextOptions<ApplicationDbContext>>();
                using (var context = new ApplicationDbContext(contextOptions))
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (SqlException)
                    {
                        // Retry in 3 seconds.
                        Thread.Sleep(3000);
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
