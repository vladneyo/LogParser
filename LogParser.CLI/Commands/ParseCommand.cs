using System;
using LogParser.CLI.CommandHandlers;
using LogParser.CLI.Commands;
using LogParser.Data.Constants;

namespace LogParser.Data.Commands
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
