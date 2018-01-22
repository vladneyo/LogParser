using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LogParser.CLI.Constants;
using LogParser.Data.Constants;
using LogParser.Parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LogParser.CLI.CommandHandlers
{
    public class ParseCommandHandler : IHandler
    {
        private readonly ParserModule _parser;

        private readonly HttpClient _client = new HttpClient();

        private readonly Dictionary<string, string> _routes = new Dictionary<string, string>();

        public ParseCommandHandler()
        {
            _parser = new ParserModule(1024 * int.Parse(ConfigurationManager.AppSettings[AppSettingsConstants.BufferSize]),
                int.Parse(ConfigurationManager.AppSettings[AppSettingsConstants.MaxPackageCapacity]));

            _client.BaseAddress = new Uri(ConfigurationManager.AppSettings[AppSettingsConstants.ApiEndpoint]);

            SetupRoutes();
        }

        public async void Handle(params object[] args)
        {
            string mode = (string)args[0];
            string filePath = (string)args[1];
            if (mode == LogTypeConstants.Error.Arg)
            {
                throw new NotImplementedException();
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            Func<List<Dictionary<string, string>>, Task> OnParsed = objs => SendingHandler(objs, _routes[mode]);
            _parser.Parsed += OnParsed;

            await _parser.ParseAsync(mode, File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));
        }

        public async Task SendingHandler(List<Dictionary<string, string>> objects, string route)
        {
            HttpResponseMessage response = null;
            // send until response is succeeded
            do
            {
                var content = new StringContent(JsonConvert.SerializeObject(objects,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }),
                    Encoding.UTF8,
                    "application/json");

                response = await _client.PostAsync(route, content);
            } while (response != null && !response.IsSuccessStatusCode);
        }

        private void SetupRoutes()
        {
            _routes.Add(LogTypeConstants.Access.Arg, RoutesConstants.AccessLogRoute);
        }
    }
}
