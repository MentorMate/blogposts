//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace App
{
    /// <summary>The application entry point class.</summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>Main application entry point method.</summary>
        /// <param name="args">Application arguments.</param>
        public static void Main(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseStartup<Startup>()
                   .Build()
                   .Run();
    }
}