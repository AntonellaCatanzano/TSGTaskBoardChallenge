namespace TSGTaskBoard.API.Support.Logging
{
    public class LoggerSettings
    {
        public LogLevel MinLogLevel { get; set; } = LogLevel.Information;
        public LoggerType Type { get; set; } = LoggerType.CONSOLE;
        public string DateTimeFormat { get; set; } = "dd-MM-yyyy HH:mm:ss";
        public string? FilePath { get; set; }
        public string? FolderPath { get; set; }
        public long MaxFolderSize { get; set; } = 10_000_000; // 10 MB
    }
}
