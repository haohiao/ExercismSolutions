static class LogLine
{

    #region 类成员
    public static string Message(string log_line)
    {
        LogLineInformation log_line_information = LogLineInformation.Parse(log_line);
        return log_line_information.Message;
    }

    public static string LogLevel(string log_line)
    {
        LogLineInformation log_line_information = LogLineInformation.Parse(log_line);
        return log_line_information.LogTypeString;
    }

    public static string Reformat(string log_line)
    {
        LogLineInformation log_line_information = LogLineInformation.Parse(log_line);
        LogLineInformation new_log_line_information = new LogLineInformation(
            log_line_information.Message,
            log_line_information.LogType,
            log_line_information.HasSuffix,
            log_line_information.HasPrefix);

        return new_log_line_information.LogLine;
    }
    #endregion

    #region 类型
    private enum LogType
    {
        None,
        Information,
        Warning,
        Error,
    }

    private record struct LogLineInformation
    {
        #region 类成员
        private readonly static Dictionary<LogType, string> LogTypeStrings = new Dictionary<LogType, string>
        {
            { LogType.Information, "info" },
            { LogType.Warning, "warning" },
            { LogType.Error, "error" },
            { LogType.None, "" }
        };

        public static LogLineInformation Parse(string log_line)
        {
            LogLineInformation log_line_information = new LogLineInformation(log_line, LogType.None);
            foreach (LogType log_type in Enum.GetValues(typeof(LogType)))
            {
                if (log_type == LogType.None)
                {
                    continue;
                }

                string prefix = GetLogTypePrefix(log_type);
                string suffix = GetLogTypeSuffix(log_type);
                bool has_prefix = log_line.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
                bool has_suffix = log_line.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
                string message = log_line;
                if (has_prefix)
                {
                    message = message[prefix.Length..];
                }
                if (has_suffix)
                {
                    message = message[..^suffix.Length];
                }
                message = message.Trim();

                if (has_prefix || has_suffix)
                {
                    log_line_information = new LogLineInformation(message, log_type, has_prefix, has_suffix);
                    break;
                }
            }
            return log_line_information;
        }

        private static string GetLogTypePrefix(LogType log_type)
        {
            string prefix = log_type is not LogType.None ? $"[{LogTypeStrings[log_type]}]: " : "";
            return prefix;
        }
        private static string GetLogTypeSuffix(LogType log_type)
        {
            string suffix = log_type is not LogType.None ? $" ({LogTypeStrings[log_type]})" : "";
            return suffix;
        }
        #endregion 类成员


        #region 实例成员
        public LogLineInformation(
            string message,
            LogType log_type,
            bool has_prefix = false,
            bool has_suffix = false)
        {
            Message = message;
            LogType = log_type;
            HasPrefix = has_prefix;
            HasSuffix = has_suffix;
        }

        public string Message { get; init; }
        public LogType LogType { get; init; }
        public bool HasPrefix { get; init; }
        public bool HasSuffix { get; init; }
        public string LogLine
        {
            get
            {
                string prefix = HasPrefix ? GetLogTypePrefix(LogType) : "";
                string suffix = HasSuffix ? GetLogTypeSuffix(LogType) : "";
                string log_line = $"{prefix}{Message}{suffix}";
                return log_line;
            }
        }
        public string LogTypeString 
        {
            get
            {
                return LogTypeStrings[LogType];
            }
        }
        #endregion 实例成员
    
    }
    #endregion 类型
}