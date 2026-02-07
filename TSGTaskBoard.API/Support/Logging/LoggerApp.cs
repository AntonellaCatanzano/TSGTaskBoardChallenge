namespace TSGTaskBoard.API.Support.Logging
{
    public class LoggerApp : ILogger
    {
        #region Dependencias
        protected readonly LoggerProviderApp _loggerProvider;
        private static readonly object _locker = new object();
        #endregion

        #region Constructor
        public LoggerApp(LoggerProviderApp loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }
        #endregion

        #region Métodos
        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter) 
        {
            if (!IsEnabled(logLevel)) return;

            try
            {
                string logDate = DateTimeOffset.UtcNow.ToLocalTime()
                    .ToString(_loggerProvider.Options.DateTimeFormat);
                string logLevelStr = logLevel.ToString();
                string logMessage = formatter(state, exception);
                string stackTrace = exception?.StackTrace ?? string.Empty;

                string logRecord = $"[{logDate}] [{logLevelStr}] {logMessage} {stackTrace}";

                if (_loggerProvider.Options.Type == LoggerType.FILE)
                {
                    if (!Directory.Exists(_loggerProvider.Options.FolderPath))
                        Directory.CreateDirectory(_loggerProvider.Options.FolderPath!);

                    var files = new DirectoryInfo(_loggerProvider.Options.FolderPath!).GetFiles().ToList();
                    long totalSize = files.Sum(f => f.Length);
                    if (totalSize > _loggerProvider.Options.MaxFolderSize)
                    {
                        var oldestFile = files.OrderBy(f => f.LastWriteTime).FirstOrDefault();
                        oldestFile?.Delete();
                    }

                    string fullPath = Path.Combine(
                        _loggerProvider.Options.FolderPath!,
                        _loggerProvider.Options.FilePath!.Replace("{date}", DateTime.Now.ToString("yyyyMMdd")));

                    lock (_locker)
                    {
                        using var fs = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.Read);
                        using var writer = new StreamWriter(fs);
                        writer.WriteLine(logRecord);
                    }
                }
                else if (_loggerProvider.Options.Type == LoggerType.CONSOLE)
                {
                    Console.WriteLine(logRecord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en LoggerApp: {ex}");
            }
        }
        #endregion
    }
}
