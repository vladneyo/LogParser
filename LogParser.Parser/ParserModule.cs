using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using LogParser.Parser.Helpers;
using LParser = LogParser.Parser.Parsers.LogParser;

namespace LogParser.Parser
{
    public partial class ParserModule
    {
        public async Task ParseAsync(string mode, FileStream fs)
        {
            //System.Diagnostics.Debugger.Launch();
            var parser = new LParser(_strategies[mode].Value);
            var apiData = SetupApiData(mode);

            // run 1st task for reading file by chunks
            var reading = Task.Run(() =>
            {
                string[] lines = new string[_maxPackageCapacity];
                using (BufferedStream bs = new BufferedStream(fs, _bufferSize))
                using (StreamReader sr = new StreamReader(bs))
                {
                    int runNumbers = 0;
                    Task parserResult = null;
                    int count = 0;
                    while (!sr.EndOfStream)
                    {
                        if (count >= _maxPackageCapacity)
                        {
                            if (runNumbers >= 2)
                            {
                                // if new chunk is already read we need to wait until previous chuck is parsed
                                parserResult.Wait();
                                runNumbers = 0;
                            }
                            // trigger parser to parse part of lines
                            parserResult = ParsingHandler(lines, parser, apiData);
                            runNumbers++;
                            count = 0;
                        }

                        lines[count] = sr.ReadLine();
                        count++;
                    }
                }
            });
            reading.Wait();
        }

        private async Task ParsingHandler(string[] lines, LParser parser, ApiData apiData)
        {
            try
            {
                var parsedObjects = new List<Dictionary<string, string>>(lines.Length);
                foreach (var line in lines)
                {
                    parsedObjects.Add(parser.Parse(line));
                }
                await SendingHandler(parsedObjects, apiData);
            }
            catch (Exception e)
            {
                
            }
        }

        private async Task SendingHandler(List<Dictionary<string, string>> objects, ApiData apiData)
        {
            try
            {
                HttpResponseMessage response = null;
                // send until response is succeeded
                do
                {
                    var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(apiData.key, JsonSerializerHelper.Serialize(objects))
                    });
                    response = await _client.PostAsync(apiData.route, content);
                } while (response != null && !response.IsSuccessStatusCode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}
