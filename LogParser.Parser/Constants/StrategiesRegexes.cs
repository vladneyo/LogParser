namespace LogParser.Parser.Constants
{
    // should be moved to config but not today
    public static class StrategiesRegexes
    {
        public static readonly string AccessLogRegex = @"^(?<host>\S+)\s[\-\s]*\[(?<date>[^\]]+)\]\s+((\""\w+\s+(?<route>([/\w\d\-_.~%@]+)|\*)(?<params>\?\S+)?\s+[/\w\d\.]+\"")|(\""\-\""))\s+(?<status>\d{3})\s+(?<size>(\d+))|(\-)$";
    }
}
