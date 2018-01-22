using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogParser.Data.Constants;
using LogParser.Parser.Strategies;

namespace LogParser.Parser
{
    public partial class ParserModule
    {
        private readonly int _bufferSize;

        private readonly int _maxPackageCapacity;

        private readonly Dictionary<string, Lazy<IParseStrategy>> _strategies = new Dictionary<string, Lazy<IParseStrategy>>();

        public event Func<List<Dictionary<string, string>>, Task> Parsed;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="bufferSize">Buffer size in kb</param>
        /// <param name="maxPackageCapacity">Capacity of list will be parsed at once</param>
        public ParserModule(int bufferSize, int maxPackageCapacity)
        {
            _bufferSize = bufferSize;
            _maxPackageCapacity = maxPackageCapacity;
            SetupStrategies();
        }

        private void SetupStrategies()
        {
            _strategies.Add(LogTypeConstants.Access.Arg, new Lazy<IParseStrategy>(() => new AccessLogStrategy()));
        }
    }
}
