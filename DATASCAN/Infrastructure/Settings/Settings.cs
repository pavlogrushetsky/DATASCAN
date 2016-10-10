using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DATASCAN.Infrastructure.Settings
{
    /// <summary>
    /// Класс, позволяющий сохранять настройки сервера баз данных
    /// </summary>
    public static class Settings
    {
        private static readonly string _fileName = "DATASCAN.settings";

        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DATASCAN");

        public static string ServerName { get; set; } = @"GRUSHETSKY-PC\SQLEXPRESS";

        public static string DatabaseName { get; set; } = "DATASCAN";

        public static string UserName { get; set; } = "";

        public static string UserPassword { get; set; } = "";

        public static string ConnectionTimeout { get; set; } = "10";

        public static string COMPort1 { get; set; } = "";

        public static string COMPort2 { get; set; } = "";

        public static string COMPort3 { get; set; } = "";

        public static string Baudrate { get; set; } = "9600";

        public static string Parity { get; set; } = "None";

        public static string DataBits { get; set; } = "8";

        public static string StopBits { get; set; } = "One";

        public static string DbfPath { get; set; } = @"C:\Dispatch\tabDbf";

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
                        ConnectionTimeout = settings["ConnectionTimeout"]?.ToString() ?? "";
                        COMPort1 = settings["COMPort1"]?.ToString() ?? "";
                        COMPort2 = settings["COMPort2"]?.ToString() ?? "";
                        COMPort3 = settings["COMPort3"]?.ToString() ?? "";
                        Baudrate = settings["Baudrate"]?.ToString() ?? "";
                        Parity = settings["Parity"]?.ToString() ?? "";
                        DataBits = settings["DataBits"]?.ToString() ?? "";
                        StopBits = settings["StopBits"]?.ToString() ?? "";
                        DbfPath = settings["DbfPath"]?.ToString() ?? "";
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
                { "ConnectionTimeout", ConnectionTimeout },
                { "COMPort1", COMPort1 },
                { "COMPort2", COMPort2 },
                { "COMPort3", COMPort3 },
                { "Baudrate", Baudrate },
                { "Parity", Parity },
                { "DataBits", DataBits },
                { "StopBits", StopBits },
                { "DbfPath", DbfPath }
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