//-----------------------------------------------------------------------
// <copyright file="Startup.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using LightInject;
using LightInject.Microsoft.DependencyInjection;
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
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var containerOptions = new ContainerOptions { EnablePropertyInjection = false };
            var container = new ServiceContainer(containerOptions);

            container.AssemblyLoader = new CoreAssemblyLoader();
            container
                .AssemblyLoader
                .Load("FeatureToggle.*Calculator.dll")
                .AsParallel()
                .ForAll(assembly => container.RegisterAssembly(assembly, () => new PerRequestLifeTime(), (service, imp) => service.IsAbstract));

            return container.CreateServiceProvider(services);
        }

        /// <summary>This method gets called by the runtime. Use this method to configure the HTTP request pipeline.</summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}