using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace DATASCAN.Infrastructure.Logging
{
    /// <summary>
    /// Класс для логирования сообщений приложения
    /// </summary>
    public static class Logger
    {
        private static string _fileName;

        private static int _day, _month, _year;

        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DATASCAN");

        /// <summary>
        /// Логирует сообщение в файл и в окно сообщений
        /// </summary>
        /// <param name="console"><see cref="ListView"/>Элемент ListView для отображения сообщений</param>
        /// <param name="entry">Элемент для логирования</param>
        public static void Log(ListView console, LogEntry entry)
        {
            if (_day != DateTime.Now.Day || _month != DateTime.Now.Month || _year != DateTime.Now.Year)
            {
                _day = DateTime.Now.Day;
                _month = DateTime.Now.Month;
                _year = DateTime.Now.Year;

                _fileName = DateTime.Now.ToString("yyyy.MM.dd") + "_log.json";         
            }

            LogToFile(entry);

            LogToConsole(console, entry);
        }

        // Логирует сообщение в файл
        private static void LogToFile(LogEntry entry)
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(_filePath);
            }

            using (StreamWriter log = new StreamWriter(Path.Combine(_filePath, _fileName), true))
            {
                log.WriteLine(JsonConvert.SerializeObject(entry));
            }
        }

        // Логирует сообщение в окно сообщений
        private static void LogToConsole(ListView console, LogEntry entry)
        {
            string type = "";
            switch (entry.Type)
            {
                case LogType.System:
                    type = "Система";
                    break;
                case LogType.Floutec:
                    type = "ФЛОУТЭК";
                    break;
                case LogType.Roc:
                    type = "ROC809";
                    break;
            }

            ListViewItem item = new ListViewItem(new[]
            {
                "",
                type,
                entry.Timestamp.ToString("dd.MM.yyyy HH:mm:ss"),
                entry.Message
            });

            switch (entry.Status)
            {
                case LogStatus.Info:
                    item.ImageIndex = 0;
                    item.StateImageIndex = 0;
                    break;
                case LogStatus.Success:
                    item.ImageIndex = 1;
                    item.StateImageIndex = 1;
                    break;
                case LogStatus.Error:
                    item.ImageIndex = 3;
                    item.StateImageIndex = 3;
                    break;
                case LogStatus.Warning:
                    item.ImageIndex = 2;
                    item.StateImageIndex = 2;
                    break;
            }
            console.BeginUpdate();
            console.Items.Add(item);

            console.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            console.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
            console.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            console.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);

            console.Items[console.Items.Count - 1].EnsureVisible();
            console.EndUpdate();
        }
    }
}