using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Logging
{
    public class Log
    {
        private const string FILE_EXT = ".log";
        private readonly string datetimeFormat;
        private readonly string logFilename;

        public Log()
        {
            this.datetimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
            this.logFilename = Directory.GetCurrentDirectory() + "\\Log.log";
            string str = this.logFilename + " is created.";
            if (File.Exists(this.logFilename))
                return;
            this.WriteLine(DateTime.Now.ToString(this.datetimeFormat) + " " + str, false);
        }

        public void Debug(string text) => this.WriteFormattedLog(Log.LogLevel.DEBUG, text);

        public void Error(string text) => this.WriteFormattedLog(Log.LogLevel.ERROR, text);

        public void Fatal(string text) => this.WriteFormattedLog(Log.LogLevel.FATAL, text);

        public void Info(string text) => this.WriteFormattedLog(Log.LogLevel.INFO, text);

        public void Trace(string text) => this.WriteFormattedLog(Log.LogLevel.TRACE, text);

        public void Warning(string text) => this.WriteFormattedLog(Log.LogLevel.WARNING, text);

        private void WriteLine(string text, bool append = true)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(this.logFilename, append, Encoding.UTF8))
                {
                    if (string.IsNullOrEmpty(text))
                        return;
                    streamWriter.WriteLine(text);
                    streamWriter.Flush();
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch
            {
                throw;
            }
        }

        private void WriteFormattedLog(Log.LogLevel level, string text)
        {
            string str;
            switch (level)
            {
                case Log.LogLevel.TRACE:
                    str = DateTime.Now.ToString(this.datetimeFormat) + " [TRACE]   ";
                    break;
                case Log.LogLevel.INFO:
                    str = DateTime.Now.ToString(this.datetimeFormat) + " [INFO]    ";
                    break;
                case Log.LogLevel.DEBUG:
                    str = DateTime.Now.ToString(this.datetimeFormat) + " [DEBUG]   ";
                    break;
                case Log.LogLevel.WARNING:
                    str = DateTime.Now.ToString(this.datetimeFormat) + " [WARNING] ";
                    break;
                case Log.LogLevel.ERROR:
                    str = DateTime.Now.ToString(this.datetimeFormat) + " [ERROR]   ";
                    break;
                case Log.LogLevel.FATAL:
                    str = DateTime.Now.ToString(this.datetimeFormat) + " [FATAL]   ";
                    break;
                default:
                    str = "";
                    break;
            }
            this.WriteLine(str + text);
        }

        [Flags]
        private enum LogLevel
        {
            TRACE = 0,
            INFO = 1,
            DEBUG = 2,
            WARNING = DEBUG | INFO, // 0x00000003
            ERROR = 4,
            FATAL = ERROR | INFO, // 0x00000005
        }
    }
}
