using System;
using System.Collections;
using System.Collections.Generic;
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

        public static List<string> COMPorts { get; set; } = new List<string>();

        public static string Baudrate { get; set; } = "9600";

        public static string Parity { get; set; } = "None";

        public static string DataBits { get; set; } = "8";

        public static string StopBits { get; set; } = "One";

        public static string Retries { get; set; } = "3";

        public static string Timeout { get; set; } = "10";

        public static string WriteDelay { get; set; } = "1";

        public static string ReadDelay { get; set; } = "1";

        public static string WaitingTime { get; set; } = "120";

        public static string DbfPath { get; set; } = @"C:\Dispatch\tabDbf";

        /// <summary>
        /// Получает значения настроек из файла
        /// </summary>
        public static void Get()
        {
            var fullPath = Path.Combine(_filePath, _fileName);

            if (File.Exists(fullPath))
            {
                using (var fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();

                    var settings = formatter.Deserialize(fileStream) as Hashtable;

                    if (settings == null)
                        return;

                    ServerName = settings["ServerName"]?.ToString() ?? "";
                    DatabaseName = settings["DatabaseName"]?.ToString() ?? "";
                    UserName = settings["UserName"]?.ToString() ?? "";
                    UserPassword = settings["UserPassword"]?.ToString() ?? "";
                    ConnectionTimeout = settings["ConnectionTimeout"]?.ToString() ?? "";
                    COMPorts = settings["COMPorts"] as List<string>;
                    Baudrate = settings["Baudrate"]?.ToString() ?? "";
                    Parity = settings["Parity"]?.ToString() ?? "";
                    DataBits = settings["DataBits"]?.ToString() ?? "";
                    StopBits = settings["StopBits"]?.ToString() ?? "";
                    Retries = settings["Retries"]?.ToString() ?? "";
                    Timeout = settings["Timeout"]?.ToString() ?? "";
                    WriteDelay = settings["WriteDelay"]?.ToString() ?? "";
                    ReadDelay = settings["ReadDelay"]?.ToString() ?? "";
                    DbfPath = settings["DbfPath"]?.ToString() ?? "";
                    WaitingTime = settings["WaitingTime"]?.ToString() ?? "";
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
            var settings = new Hashtable
            {
                { "ServerName", ServerName },
                { "DatabaseName", DatabaseName },
                { "UserName", UserName },
                { "UserPassword", UserPassword },
                { "ConnectionTimeout", ConnectionTimeout },
                { "COMPorts", COMPorts },
                { "Baudrate", Baudrate },
                { "Parity", Parity },
                { "DataBits", DataBits },
                { "StopBits", StopBits },
                { "Retries", Retries },
                { "Timeout", Timeout },
                { "WriteDelay", WriteDelay },
                { "ReadDelay", ReadDelay },
                { "DbfPath", DbfPath },
                { "WaitingTime", WaitingTime }
            };

            if (!Directory.Exists(_filePath))
                Directory.CreateDirectory(_filePath);

            using (var fileStream = new FileStream(Path.Combine(_filePath, _fileName), FileMode.Create))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(fileStream, settings);
            }
        }
    }
}