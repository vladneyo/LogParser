using System.Collections.Generic;

namespace LogParser.Parser.Strategies
{
    public interface IParseStrategy
    {
        Dictionary<string, string> Parse(string input);
    }
}
