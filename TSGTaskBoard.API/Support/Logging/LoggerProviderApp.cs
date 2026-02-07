using System.Diagnostics.CodeAnalysis;

namespace TSGTaskBoard.API.Support.Logging
{
    [ProviderAlias("LoggerProviderApp")]
    public class LoggerProviderApp : ILoggerProvider
    {
        #region Dependencias
        public readonly LoggerSettings Options;
        #endregion

        #region Constructor
        public LoggerProviderApp([NotNull] LoggerSettings options)
        {
            Options = options;

            if (Options.Type == LoggerType.FILE && !string.IsNullOrEmpty(Options.FolderPath))
            {
                if (!Directory.Exists(Options.FolderPath))
                    Directory.CreateDirectory(Options.FolderPath);
            }
        }
        #endregion

        #region Métodos
        public ILogger CreateLogger(string categoryName)
        {
            return new LoggerApp(this);
        }

        public void Dispose() { }
        #endregion
    }
}
