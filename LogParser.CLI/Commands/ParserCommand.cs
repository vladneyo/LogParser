using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogParser.CLI.Constants;

namespace LogParser.CLI.Commands
{
    public class ParserCommand: ConsoleCommand
    {
        public override void Process(string command, string param)
        {
            if(command != LogTypeConstants.Access.Arg &&
                command != LogTypeConstants.Error.Arg)
            {
                CallNext(command, param);
                return;
            }
            if (string.IsNullOrEmpty(param))
            {
                throw new ArgumentException("No file path was specified");
            }
            Console.WriteLine("PARSER");
        }
    }
}
