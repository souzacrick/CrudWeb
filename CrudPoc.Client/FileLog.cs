using System;
using System.IO;

namespace CrudPoc.Helper
{
    public class FileLog
    {
        public enum EnumLogFileType
        {
            Info = 1,
            Error = 2
        }

        public void Initialize(string logDirectory, string rootPath)
        {
            if (string.IsNullOrEmpty(logDirectory))
                return;

            var directory = $"{rootPath}\\{logDirectory}";

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        public void WriteInfo(string message, string logDirectory)
        {
            Write(EnumLogFileType.Info, message, logDirectory);
        }

        public void WriteException(Exception error, string logDirectory)
        {
            var message = $"Message: {(error.InnerException != null ? error.InnerException.Message : error.Message)}";
            Write(EnumLogFileType.Error, message, logDirectory);

            var stack = $"Stack: {(error.InnerException != null ? error.InnerException.StackTrace : error.StackTrace)}";
            Write(EnumLogFileType.Error, stack, logDirectory);
        }

        private void Write(EnumLogFileType logFileType, string message, string logDirectory)
        {
            if (string.IsNullOrEmpty(logDirectory))
                return;

            var directory = logDirectory;

            directory += (@"\Log_CrudPoc_" + DateTime.Now.ToString("yyyy-MM-dd") + @".txt");

            var type = string.Empty;

            switch (logFileType)
            {
                case EnumLogFileType.Info:
                    type = "Info";
                    break;

                case EnumLogFileType.Error:
                    type = "Error";
                    break;
            }

            var writer = new StreamWriter(directory, true);

            try
            {
                writer.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff")} [{type}] {message}");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}
