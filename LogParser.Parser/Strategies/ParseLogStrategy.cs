namespace LogParser.Parser.Strategies
{
    public abstract class ParseLogStrategy<T>: IParseStrategy where T: class
    {
        public abstract T Parse(string input);

        object IParseStrategy.Parse(string input)
        {
            return Parse(input);
        }
    }
}
