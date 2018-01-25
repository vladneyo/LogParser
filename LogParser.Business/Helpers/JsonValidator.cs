namespace LogParser.Business.Helpers
{
    public static class JsonValidator
    {
        public static bool IsValid(string input)
        {
            var trimmed = input.Trim();
            return trimmed.StartsWith("{") && trimmed.EndsWith("}");
        }
    }
}
