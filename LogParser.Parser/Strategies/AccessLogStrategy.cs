using LogParser.Data.Dtos;

namespace LogParser.Parser.Strategies
{
    public class AccessLogStrategy: ParseLogStrategy<AccessLogDto>, IParseStrategy
    {
        public override AccessLogDto Parse(string input)
        {
            // implement parsing logic
            return new AccessLogDto();
        }
    }
}
