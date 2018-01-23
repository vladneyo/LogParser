using System;

namespace LogParser.Data.Converters
{
    public class AccessLogDatetimeConverter
    {
        public DateTime Convert(string input)
        {
            var withOutOffset = input.Split(' ');

            var dateAndTime = withOutOffset[0].Split(new[] { ':' }, 2);
            var offset = withOutOffset[1];
            var offHours = offset.Substring(0, offset.Length - 2);
            var offMins = offset.Substring(offset.Length - 2);

            return System.Convert.ToDateTime(string.Concat(dateAndTime[0], " ", dateAndTime[1], " ", offHours, ":", offMins));
        }
    }
}
