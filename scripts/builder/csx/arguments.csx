//-----------------------------------------------------------------------
// <copyright file="arguments.csx" company="MentorMate, Inc.">
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use these files except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0."
// </copyright>
// <author>Rosen Kolev</author>
//-----------------------------------------------------------------------

public static partial class Globals
{
    public static MM.CommandArguments Args = null;
    public static string DefaultCommand = null;
}

public static partial class MM
{
    public class CommandArguments
    {
        private string[] _keys;
        private string[] _values;
        private string _command;
        
        public string Command => _command ?? Globals.DefaultCommand;

        public string this[string key]    // Indexer declaration  
        {  
            get
            {
                if (_keys == null) return null;
                var index = Array.IndexOf(_keys, key);
                if (index < 0) return null;
                return _values[index];
            }
        }

        public bool Has(string key) => _keys != null && Array.IndexOf(_keys, key) > -1;
        
        public CommandArguments(IList<string> args)
        {
            var count = args.Count();
            if (count > 0)
            {
                _command = args[0];
            }
            
            if (count > 1)
            {
                var size = count - 1;
                _keys = new string[size];
                _values = new string[size];
                
                for (var index = 1; index < count; index++)
                {
                    var arg = args[index];
                    WriteLine($"Parsing argument {arg}.", LogLevel.Debug);
                    if (arg[0] != '/') { 
                        WriteLine("Invalid argument " + arg + " specify /key:value.", LogLevel.Error);
                        WriteLine("EXIT");
                        Environment.Exit(1);
                    }

                    var indexOfSeparator = arg.IndexOf(':');
                    var hasValue = indexOfSeparator > -1;
                    var key   = _keys[index - 1]   = hasValue ? arg.Substring(1, indexOfSeparator - 1) : arg.Substring(1);
                    var value = _values[index - 1] = hasValue ? arg.Substring(indexOfSeparator + 1)    : null;
                    WriteLine($"Valid argument {key} with value {value}", LogLevel.Debug);
                }
            }
        }
    }
}

Globals.Args = new MM.CommandArguments(Args);