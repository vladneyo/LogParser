using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LogParser.Parser.Strategies
{
    public class AccessLogStrategy: IParseStrategy
    {
        public Dictionary<string, string> Parse(string input)
        {
            var splitted = Regex.Split(input, " ", RegexOptions.Singleline);
            var hasQueryParams = splitted[6].Any(x => x == '?');
            Dictionary<string, string> result = null;
            try
            {
                result = new Dictionary<string, string>
                {
                    { "Host", splitted[0]},
                    { "Time", string.Concat(splitted[3].Substring(1),  " ", splitted[4].Substring(0, splitted[4].Length-2))},
                    { "Route", hasQueryParams ?
                        splitted[6].Substring(0, splitted[6].IndexOf("?")) :
                        splitted[6]},
                    { "QueryParams", hasQueryParams ?
                        splitted[6].Substring(splitted[6].IndexOf("?") + 1):
                        ""},
                    { "RequestStatus", splitted[8]},
                    {"ResponseSize", splitted[9] }
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
