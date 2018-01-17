using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogParser.CLI.Constants;

namespace LogParser.CLI.Commands
{
    public class HelpCommand: ConsoleCommand
    {
        
        public override void Process(string command, string param)
        {
            if (command != ConsoleFlags.Help)
            {
                CallNext(command, param);
                return;
            }
            Console.WriteLine("--HELP--");
        }
    }
}
