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