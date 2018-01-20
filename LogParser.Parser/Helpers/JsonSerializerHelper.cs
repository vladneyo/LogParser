using System.Collections.Generic;
using Newtonsoft.Json;

namespace LogParser.Parser.Helpers
{
    public static class JsonSerializerHelper
    {

        public static string Serialize(Dictionary<string, string> obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static string Serialize(List<Dictionary<string, string>> objs)
        {
            return JsonConvert.SerializeObject(objs);
        }
    }
}
