//-----------------------------------------------------------------------
// <copyright file="ProbesController.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Api.Controllers
{
    /// <summary>And enpoint for getting api version.</summary>
    [Route("api/[controller]")]
    public class ProbesController : Controller
    {
        /// <summary>Return the api version string.</summary>
        [HttpGet]
        public IActionResult Get() =>
            Ok(Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
    }
}