//-----------------------------------------------------------------------
// <copyright file="ApplicationDbContextFactory.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace MyApp.Api.Data
{
    /// <summary>This factory i used only when creating migration locally.</summary>
    /// <seealso cref="IDesignTimeDbContextFactory{ApplicationDbContext}" />
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /// <summary>Creates a new instance of a derived context.</summary>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(GetConnectionString());
            return new ApplicationDbContext(builder.Options);
        }

        private static string GetConnectionString()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");
            using (var reader = new StreamReader(path))
            {
                dynamic configurations = JsonConvert.DeserializeObject(reader.ReadToEnd());

                return configurations.ConnectionStrings.DefaultConnection;
            }
        }
    }
}
