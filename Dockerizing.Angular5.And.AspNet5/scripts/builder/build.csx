//-----------------------------------------------------------------------
// <copyright file="build.csx" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

#load "csx/process.csx"
using static MM;

Globals.MaxLogLevel = LogLevel.Debug;

WriteLine($"Command {Globals.Args.Command}", LogLevel.Info);

var pathToSolution = Directory.GetCurrentDirectory();
var pathToApiTests = Path.Combine(pathToSolution, @"server/MyApp.Tests/MyApp.Tests.csproj");
var pathToApiApp = Path.Combine(pathToSolution, @"server/MyApp.Api/MyApp.Api.csproj");
var pathToApiOut = Path.Combine(pathToSolution, @"server/MyApp.Api/obj/Docker/publish");
var pathToWebApp = Path.Combine(pathToSolution, @"client");

try
{
    Execute(() =>
    {
        Cmd($"dotnet restore {pathToSolution}/server");
        Cmd($"dotnet test {pathToApiTests}");
        Cmd($"dotnet publish {pathToApiApp} -o {pathToApiOut} -c Release");
    }, "Build ASP.NET Core API", "build-api");

    Execute(() =>
    {
        Cmd("yarn install --production=false", pathToWebApp);
        Cmd("ng build --app my-app --prod", pathToWebApp);
    }, "Build Angular 5 App", "build-web");

    Execute(() =>
    {
        Cmd("yarn install --production=false", pathToWebApp);
        Cmd("ng serve --host 0.0.0.0", pathToWebApp);
    }, "Serve Angular 5", "debug-web");
}
catch(Exception ex)
{
    if (Globals.MaxLogLevel == LogLevel.Debug) WriteLine("StackTrace: " + ex.StackTrace, LogLevel.Error);
    throw;
}
finally
{
    WriteLine("Finished");
}