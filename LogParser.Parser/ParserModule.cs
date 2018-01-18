using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogParser.Data.Constants;
using LogParser.Data.Dtos;
using LogParser.Parser.Parsers;
using LogParser.Parser.Strategies;
using LParser = LogParser.Parser.Parsers.LogParser;

namespace LogParser.Parser
{
    public class ParserModule
    {
        private readonly int _bufferSize = 20*1024; // 20 Kb buffer
        private readonly List<Task> _parsingTasks = new List<Task>();
        private readonly Dictionary<string, IParseStrategy> _strategies = new Dictionary<string, IParseStrategy>();

        public ParserModule()
        {
            this._strategies.Add(LogTypeConstants.Access.Arg, new AccessLogStrategy());
        }

        public async Task<object> ParseAsync(string mode, FileStream fs)
        {
            // stage 1: read file
            List<string> lines = ReadFile(fs);

            // stage 2: parse file
            ParserPool<LParser> pool = SetupPool(mode);

            #region FookingShit
            // initialize result set
            var resultSet = new ConcurrentBag<AccessLogDto>();

            foreach (var line in lines)
            {
                _parsingTasks.Add(new Task(() =>
                {
                    // get parser from pool
                    var parser = pool.GetObject();
                    // put result of parsing to concurrent collection
                    resultSet.Add((AccessLogDto)parser.Parse(line));
                }));
            }
            #endregion

            // wait unit all parsers end
            await Task.WhenAll(_parsingTasks);

            // stage 3: sort collection with dtos
            var result = resultSet.OrderBy(x => x.Time).ToList();

            // return collection
            return Task.FromResult(result);
        }

        private ParserPool<LParser> SetupPool(string mode)
        {
            // setup parsers pool
            ParserPool<LParser> pool = new ParserPool<LParser>();
            // configure parser with needed strategy
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                pool.PutObject(new LParser(_strategies[mode]));
            }
            return pool;
        }

        private List<string> ReadFile(FileStream fs)
        {
            List<string> lines = new List<string>();
            using (BufferedStream bs = new BufferedStream(fs, _bufferSize))
            using (StreamReader sr = new StreamReader(bs))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
            }
            return lines;
        }
    }
}
