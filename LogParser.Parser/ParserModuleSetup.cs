using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogParser.Data.Constants;
using LogParser.Parser.Strategies;

namespace LogParser.Parser
{
    public partial class ParserModule
    {
        private readonly int _bufferSize = 1024 * int.Parse(ConfigurationManager.AppSettings["bufferSize"]); // 20 Kb buffer

        private readonly int _maxPackageCapacity = int.Parse(ConfigurationManager.AppSettings["maxPackageCapacity"]); // capacity of List will be parsed and send to API
        
        private readonly HttpClient _client = new HttpClient();

        private readonly Dictionary<string, Lazy<IParseStrategy>> _strategies = new Dictionary<string, Lazy<IParseStrategy>>();

        private readonly Dictionary<string, string> _routes = new Dictionary<string, string>();

        private readonly Dictionary<string, string> _keys = new Dictionary<string, string>();

        public ParserModule(string endpoint)
        {
            SetupStrategies();
            SetuppRoutes();
            SetupKeys();
            _client.BaseAddress = new Uri(endpoint);
        }

        private void SetupStrategies()
        {
            _strategies.Add(LogTypeConstants.Access.Arg, new Lazy<IParseStrategy>(() => new AccessLogStrategy()));
        }

        private void SetuppRoutes()
        {
            // move to constants
            _routes.Add(LogTypeConstants.Access.Arg, "log/access");
        }

        private void SetupKeys()
        {
            // move to constants
            _keys.Add(LogTypeConstants.Access.Arg, "accesslog");
        }

        private ApiData SetupApiData(string mode)
        {
            return new ApiData
            {
                route = _routes[mode],
                key = _keys[mode]
            };
        }

        class ApiData
        {
            public string route { get; set; }
            public string key { get; set; }
        }
    }
}
