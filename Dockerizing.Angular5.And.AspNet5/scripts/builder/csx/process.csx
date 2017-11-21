//-----------------------------------------------------------------------
// <copyright file="process.csx" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

#load "assert.csx"

using System.Diagnostics;

public static partial class MM
{
    public class Command
    {
        internal StringBuilder lastStandardErrorOutput = new StringBuilder();
        internal StringBuilder lastStandardOutput = new StringBuilder();
        internal Process process = new Process();

        public Command(string commandPath, string arguments, string workingDirectory, LogLevel outputLogLevel = LogLevel.Verbose)
        {
            WriteLine($"{commandPath} {arguments}", LogLevel.Debug);

            process.StartInfo = new ProcessStartInfo(commandPath);
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;

            if (!string.IsNullOrEmpty(workingDirectory))
            {
                WriteLine($"Start in directory {workingDirectory}", LogLevel.Debug);
                process.StartInfo.WorkingDirectory = workingDirectory;
            }

            process.ErrorDataReceived += (s, e) => Log(lastStandardErrorOutput, e.Data, outputLogLevel);
            process.OutputDataReceived += (s, e) => Log(lastStandardOutput, e.Data, outputLogLevel);
            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();

            WriteLine("----------------------", outputLogLevel);
        }

        public string WaitForResult()
        {
            process.WaitForExit();
            
            return lastStandardOutput.ToString().Trim();;
        }

        public int ExitCode => process.ExitCode;

        public void FailWhenExitCode(int allowedExitCode) =>
            Assert.Truthy(
                process.ExitCode == allowedExitCode,
                lastStandardErrorOutput.ToString().Trim() +
                Environment.NewLine +
                "Exit code is "
                + process.ExitCode,
                "Check if exit code is " + allowedExitCode.ToString());

        public static string Execute(string commandPath, string arguments, string workingDirectory = null, LogLevel outputLogLevel = LogLevel.Verbose)
        {
            var command = new Command(commandPath, arguments, workingDirectory, outputLogLevel);

            var result = command.WaitForResult();

            command.FailWhenExitCode(0);

            return result;
        }

        private static void Log(StringBuilder output, string message, LogLevel level)
        {
            var msg = message?.TrimEnd() ?? string.Empty;
            WriteLine(msg, level);
            output.AppendLine(msg);
        }
    }
}