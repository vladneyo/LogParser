using System;

namespace LogParser.CLI.CommandHandlers
{
    class HelpCommandHandler: IHandler
    {
        public void Handle(params object[] args)
        {
            Console.WriteLine("--HELP--");
            Console.WriteLine("--help - for help");
            Console.WriteLine("-a [path] - to parse log with specified access file");
            Console.WriteLine("-e [path] - to parse log with specified error file");
        }
    }
}
