using System.Collections.Concurrent;

namespace LogParser.Parser.Parsers
{
    public class ParserPool<T> where T: LogParser
    {
        private ConcurrentBag<T> _parsers;

        public T GetObject()
        {
            T item;
            if (_parsers.TryTake(out item)) return item;
            return null; //_objectGenerator();
        }

        public void PutObject(T item)
        {
            _parsers.Add(item);
        }
    }
}

