using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DATASCAN.Infrastructure.Settings
{
    /// <summary>
    /// Класс, позволяющий сохранять настройки сервера баз данных
    /// </summary>
    public static class ServerSettings
    {
        private static readonly string _fileName = "DATASCAN.settings";

        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DATASCAN");

        public static string ServerName { get; set; } = @"GRUSHETSKY-PC\SQLEXPRESS";

        public static string DatabaseName { get; set; } = "DATASCAN";

        public static string UserName { get; set; } = "";

        public static string UserPassword { get; set; } = "";

        /// <summary>
        /// Получает значения настроек из файла
        /// </summary>
        public static void Get()
        {
            string fullPath = Path.Combine(_filePath, _fileName);

            if (File.Exists(fullPath))
            {
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    Hashtable settings = formatter.Deserialize(fileStream) as Hashtable;

                    if (settings != null)
                    {
                        ServerName = settings["ServerName"]?.ToString() ?? "";
                        DatabaseName = settings["DatabaseName"]?.ToString() ?? "";
                        UserName = settings["UserName"]?.ToString() ?? "";
                        UserPassword = settings["UserPassword"]?.ToString() ?? "";
                    }
                }
            }
            else
            {
                Save();
            }
        }

        /// <summary>
        /// Сохраняет значения настроек в файл
        /// </summary>
        public static void Save()
        {
            Hashtable settings = new Hashtable
            {
                { "ServerName", ServerName },
                { "DatabaseName", DatabaseName },
                { "UserName", UserName },
                { "UserPassword", UserPassword },
            };

            if (!Directory.Exists(_filePath))
                Directory.CreateDirectory(_filePath);

            using (FileStream fileStream = new FileStream(Path.Combine(_filePath, _fileName), FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(fileStream, settings);
            }
        }
    }
}