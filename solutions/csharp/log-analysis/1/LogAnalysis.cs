public static class LogAnalysis
{
    public static string SubstringAfter(this string s, string delimiter)
    {
        int index = s.IndexOf(delimiter);
        string substring = s;
        if (index != -1)
        {
            substring = s.Substring(index + delimiter.Length);
        }
        return substring;
    } 

    public static string SubstringBetween(this string s, string left_delimiter, string right_delimiter)
    {
        int left_index = s.IndexOf(left_delimiter);
        int right_index = s.IndexOf(right_delimiter, left_index + left_delimiter.Length);
        string substring = s;
        if (left_index != -1 && right_index != -1)
        {
            substring = s.Substring(left_index + left_delimiter.Length, right_index - left_index - left_delimiter.Length);
        }
        return substring;
    }

    public static string Message(this string s)
    {
        string message = s.SubstringAfter(": ");
        return message;
    }

    public static string LogLevel(this string s)
    {
        string log_level = s.SubstringBetween("[", "]");
        return log_level;
    }
}