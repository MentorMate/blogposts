//-----------------------------------------------------------------------
// <copyright file="assert.csx" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

#load "common.csx"

public static partial class Globals
{
    public static bool ExitWhenAssertFail = true;
}

public static partial class MM
{
    public static class Assert
    {
        public static void Truthy(bool condition, string error, string description = "The provided value must be true.")
        {
            WriteLine(description, LogLevel.Debug);
            if (!condition) Fail(error);
        }

        public static void Fail(string error)
        {
            WriteLine(string.Empty);
            WriteLine(error, LogLevel.Error);
            if (Globals.ExitWhenAssertFail)
            {
                WriteLine("EXIT");
                Environment.Exit(1);
            }
        }
    }
}