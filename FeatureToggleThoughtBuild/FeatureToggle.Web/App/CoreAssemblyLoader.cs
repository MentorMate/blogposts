//-----------------------------------------------------------------------
// <copyright file="CoreAssemblyLoader.cs" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LightInject;

namespace App
{
    internal class CoreAssemblyLoader : IAssemblyLoader
    {
        /// <summary>Loads the specified search pattern.</summary>
        /// <param name="searchPattern">The search pattern.</param>
        public IEnumerable<Assembly> Load(string searchPattern)
        {
            var currentAssemblyCodeBase = new Uri(Assembly.GetEntryAssembly().CodeBase).LocalPath;
            var directory = Path.GetDirectoryName(currentAssemblyCodeBase);
            return searchPattern.Split('|')
                                .SelectMany(sp => Directory.GetFiles(directory, sp))
                                .Where(file => Path.GetExtension(file) == ".dll")
                                .Select(fileName => Assembly.LoadFrom(fileName));
        }
    }
}