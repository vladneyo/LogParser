using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.CLI.Constants
{
    public static class LogTypeConstants
    {
        public static readonly LogType Access = new LogType {Arg = "-a", Name="Access"};
        public static readonly LogType Error = new LogType { Arg = "-e", Name = "Error"};
    }

    public class LogType
    {
        public string Name { get; set; }
        public string Arg { get; set; }
    }
}
