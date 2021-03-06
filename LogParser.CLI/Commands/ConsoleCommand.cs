﻿using System;
using LogParser.CLI.CommandHandlers;

namespace LogParser.CLI.Commands
{
    public abstract class ConsoleCommand
    {
        protected ConsoleCommand Successor { get; private set; }
        public IHandler Handler { get; protected set; }

        public void SetSuccessor(ConsoleCommand successor)
        {
            Successor = successor;
        }

        public abstract void Process(string command, string param);

        protected void CallNext(string command, string param)
        {
            if (Successor != null)
            {
                Successor.Process(command, param);
            }
            else
            {
                throw new ApplicationException("No one handler could process this command");
            }
        }
    }
}
