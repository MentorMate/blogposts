//-----------------------------------------------------------------------
// <copyright file="common.csx" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

#load "arguments.csx"

public enum LogLevel
{
    Error = 0,
    Message,
    Info,
    Verbose,
    Debug,
    None
}

public static partial class Globals
{
    public static LogLevel MaxLogLevel = LogLevel.Verbose;
}

public static partial class MM
{
    private static string[] _consoleColors = new [] { "[1;31m" /*Error:Red*/, "[37m" /*Message: White*/, "[1;33m" /*Info:Yellow*/, "[1;35m" /*Verbose:Magenta*/, "[1;30m" /*Debug: BrightGray*/ };

    /// <summary>
    /// Write to the default console an message.
    /// Based on the log level (logLevel) the message will be with different color and offset.
    /// If the LogLevel is higher than the Global.MaxLogLevel the message will not be shown.
    /// </summary>
    public static void WriteLine(string message, LogLevel logLevel = LogLevel.Message)
    {
        var level = (int)logLevel;
        if ((int)Globals.MaxLogLevel < level) return;
        
        var lines = message.Split(Environment.NewLine);
        var color = _consoleColors[level];
        var offset = new String(' ', level * 2);
        foreach (var line in lines)
        {
            Console.WriteLine(offset + color + line + "[0m");
        }
    }

    /// <summary>
    /// Execute an action (Function).
    /// Before action is executed the description is shown.
    /// After the action is executed time is shown.
    /// The action is not executed if the first argument passed to dotnet-script do not match provided commands.
    /// Example when the action will be executed 'dotnet script build.csx -- publish-my-app'... Execute(() => do_something, "Do something", "publish-my-app");
    /// </summary>
    public static void Execute(Action action, string description, params string[] commands)
    {
        if (commands == null || commands.Length == 0 || commands.Contains(Globals.Args.Command))
        {
            WriteLine(description);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            action();
            watch.Stop();
            WriteLine($"...Done! ({watch.ElapsedMilliseconds} ms)");
        }
    }

    /// <summary> Execute a system command. Example Cmd("/bin/bash -c ls").</summary>
    public static string Cmd(string command, string workingDirectory = null)
    {
        var index = command.IndexOf(' ');
        return Command.Execute(command.Substring(0, index), command.Substring(index + 1), workingDirectory);
    }
}