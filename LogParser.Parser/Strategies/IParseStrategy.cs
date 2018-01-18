namespace LogParser.Parser.Strategies
{
    public interface IParseStrategy
    {
        object Parse(string input);
    }
}
