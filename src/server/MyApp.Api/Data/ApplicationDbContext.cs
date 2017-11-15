//-----------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using MyApp.Api.Data.Models;

namespace MyApp.Api.Data
{
    /// <summary>
    /// The default application database context.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class ApplicationDbContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="ApplicationDbContext"/> class.</summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>Gets or sets the files.</summary>
        /// <value>The files.</value>
        public virtual DbSet<ToDo> ToDos { get; set; }
    }
}
