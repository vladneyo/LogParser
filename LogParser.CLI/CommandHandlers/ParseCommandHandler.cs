using System;
using System.IO;
using LogParser.Data.Constants;
using LogParser.Parser;

namespace LogParser.CLI.CommandHandlers
{
    public class ParseCommandHandler: IHandler
    {
        private ParserModule _parser;

        public ParseCommandHandler()
        {
            _parser = new Parser.ParserModule();
        }
        public async void Handle(params object[] args)
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
            await _parser.ParseAsync(mode, File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }
    }
}
