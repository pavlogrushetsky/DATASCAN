using System;

namespace DATASCAN.Infrastructure.Logging
{
    /// <summary>
    /// Модель элемента логирования
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Статус
        /// </summary>
        public LogStatus Status { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public LogType Type { get; set; }

        /// <summary>
        /// Дата и время возникновения
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }
    }
}