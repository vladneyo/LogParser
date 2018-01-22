using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogParser.Parser.Strategies
{
    public class AccessLogStrategy : IParseStrategy
    {
        private readonly string pattern =
                @"^(?<host>\S+)\s[\-\s]*\[(?<date>[^\]]+)\]\s+((\""\w+\s+(?<route>([/\w\d\-_.~%@]+)|\*)(?<params>\?\S+)?\s+[/\w\d\.]+\"")|(\""\-\""))\s+(?<status>\d{3})\s+(?<size>(\d+))|(\-)$";

        public Dictionary<string, string> Parse(string input)
        {
            var match = Regex.Match(input, pattern, RegexOptions.Singleline);
            Dictionary<string, string> result = null;
            result = new Dictionary<string, string>
                {
                    { "Host", match.Groups["host"].Value},
                    { "Time", match.Groups["date"].Value},
                    { "Route", match.Groups["route"].Value},
                    { "QueryParams", match.Groups["params"].Value},
                    { "RequestStatus", match.Groups["status"].Value},
                    { "ResponseSize", match.Groups["size"].Value }
                };
            return result;
        }
    }
}
