using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.CLI.CommandHandlers
{
    public interface IHandler
    {
        void Handle(params object[] args);
    }
}
