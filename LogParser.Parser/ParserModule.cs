using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LogParser.Data.Constants;
using LogParser.Parser.Strategies;
using LParser = LogParser.Parser.Parsers.LogParser;

namespace LogParser.Parser
{
    public class ParserModule
    {
        private readonly int _bufferSize = 20*1024; // 20 Kb buffer

        private readonly Dictionary<string, IParseStrategy> _strategies = new Dictionary<string, IParseStrategy>();

        private readonly int _maxPacketCapacity = 50; // capacity of List will be parsed and send to API

        private SpinLock _parsingLock = new SpinLock();

        private Action<string[], LParser> _parsing = null;

        private Func<List<Dictionary<string,string>>, Task> _sendToApi = null;

        public ParserModule()
        {
            _strategies.Add(LogTypeConstants.Access.Arg, new AccessLogStrategy());
            _parsing = ParsingHandler;
            _sendToApi = SendingHandler;
        }

        public async Task ParseAsync(string mode, FileStream fs)
        {
            //System.Diagnostics.Debugger.Launch();
            var parser = new LParser(_strategies[mode]);
            // run 1st task for reading file by chunks
            var reading = Task.Run(() =>
            {
                string[] lines = new string[_maxPacketCapacity];
                using (BufferedStream bs = new BufferedStream(fs, _bufferSize))
                using (StreamReader sr = new StreamReader(bs))
                {
                    int runNumbers = 0;
                    IAsyncResult parserResult = null;
                    int count = 0;
                    while (!sr.EndOfStream)
                    {
                        if (count >= _maxPacketCapacity)
                        {
                            if (runNumbers >= 2)
                            {
                                // if new chunk is already read we need to wait until previous chuck is parsed
                                _parsing.EndInvoke(parserResult);
                                runNumbers = 0;
                            }
                            // trigger parser to parse part of lines
                            parserResult = _parsing.BeginInvoke(lines, parser, null, null);
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

        private async void ParsingHandler(string[] lines, LParser parser)
        {
            bool lockTaken = false;
            try
            {
                _parsingLock.Enter(ref lockTaken);
                var objects = new List<Dictionary<string, string>>(lines.Length);
                foreach (var line in lines)
                {
                    objects.Add(parser.Parse(line));
                }
                var result = _sendToApi.BeginInvoke(objects, null, null);
                await _sendToApi.EndInvoke(result);
            }
            finally
            {
                if (lockTaken)
                {
                    _parsingLock.Exit();
                }
            }

        }

        private async Task SendingHandler(List<Dictionary<string, string>> objects)
        {
            // send request
        }
    }
}
