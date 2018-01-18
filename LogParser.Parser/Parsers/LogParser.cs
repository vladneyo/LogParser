using LogParser.Parser.Strategies;

namespace LogParser.Parser.Parsers
{
    public class LogParser
    {
        private IParseStrategy _strategy;
        public LogParser(IParseStrategy strategy)
        {
            _strategy = strategy;
        }

        public object Parse(string input)
        {
            return _strategy.Parse(input);
        }
    }
}
