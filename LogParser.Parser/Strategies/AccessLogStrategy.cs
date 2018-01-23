using System.Collections.Generic;
using System.Text.RegularExpressions;
using LogParser.Parser.Constants;

namespace LogParser.Parser.Strategies
{
    public class AccessLogStrategy : IParseStrategy
    {
        private readonly Regex regex = new Regex(StrategiesRegexes.AccessLogRegex, RegexOptions.Singleline | RegexOptions.Compiled);

        public Dictionary<string, string> Parse(string input)
        {
            var match = regex.Match(input);

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
