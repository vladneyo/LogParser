﻿using System.Threading.Tasks;

namespace LogParser.CLI.Commands
{
    public class CommandInvoker
    {
        private ConsoleCommand _start;
        public void Configure()
        {
            //build handler chaing
            _start = new HelpCommand();
            _start.SetSuccessor(new ParseCommand());
        }

        public async Task Invoke(string command, string param)
        {
            await Task.Run(() => _start.Process(command, param));
        }
    }
}
