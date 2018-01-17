using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogParser.CLI.CommandHandlers;
using LogParser.CLI.Constants;

namespace LogParser.CLI.Commands
{
    public class ParseCommand: ConsoleCommand
    {
        public ParseCommand()
        {
            this.Handler = new ParseCommandHandler();
        }
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
            Handler.Handle(command, param);
        }
    }
}
