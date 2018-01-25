using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LParser = LogParser.Parser.Parsers.LogParser;

namespace LogParser.Parser
{
    public partial class ParserModule
    {
        public async Task ParseAsync(string mode, FileStream fs)
        {
            var parser = new LParser(_strategies[mode].Value);
            
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
                            parserResult = ParsingHandler(lines, parser);
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

        private async Task ParsingHandler(string[] lines, LParser parser)
        {
            var parsedObjects = new List<Dictionary<string, string>>(lines.Length);
            foreach (var line in lines)
            {
                parsedObjects.Add(parser.Parse(line));
            }
            await Parsed(parsedObjects);
        }
    }
}
