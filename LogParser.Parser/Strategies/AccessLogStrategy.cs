using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogParser.Parser.Strategies
{
    public class AccessLogStrategy : IParseStrategy
    {
        private readonly string pattern =
                @"^(?<host>([\S+\.]+))( \- \- \[)(?<date>(\d{2}\/[a-zA-Z]{3}\/\d{4}:\d{2}:\d{2}:\d{2} \-\d{4}))(\] \"")(([A-Z]{3,4})? (?<route>(\/\w*)+)(?<params>(\?.+)*)( HTTP\/\d{1}\.\d{1})|\-?)\"" (?<status>(\d{3}))(?<size>( \d*|\-?)*)"
            ;
        public Dictionary<string, string> Parse(string input)
        {
            var match = Regex.Match(input, pattern, RegexOptions.Singleline);
            Dictionary<string, string> result = null;
            try
            {
                result = new Dictionary<string, string>
                {
                    { "Host", match.Groups["host"].Value},
                    { "Time", match.Groups["date"].Value},
                    { "Route", match.Groups["route"].Value},
                    { "QueryParams", match.Groups["params"].Value},
                    { "RequestStatus", match.Groups["status"].Value},
                    { "ResponseSize", match.Groups["size"].Value }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(input);
            }
            return result;
        }
    }
}
