using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogParser.CLI.Constants;
using LogParser.Parser;

namespace LogParser.CLI.CommandHandlers
{
    public class ParseCommandHandler: IHandler
    {
        private Parser.LogParser _parser;

        public ParseCommandHandler()
        {
            _parser = new Parser.LogParser();
        }
        public void Handle(params object[] args)
        {
            string mode = (string) args[0];
            string filePath = (string) args[1];
            Console.WriteLine("PARSER");
            Console.WriteLine($"{mode} {filePath}");
            if (mode == LogTypeConstants.Error.Arg)
            {
                throw new NotImplementedException();
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            _parser.Parse(mode, filePath);
        }
    }
}
