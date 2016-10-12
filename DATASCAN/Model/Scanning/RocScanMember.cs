using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Scanning
{
    /// <summary>
    /// Сущность "Элемент опроса данных вычислителя ROC809"
    /// </summary>
    [Table("RocMembers", Schema = "Scan")]
    public class RocScanMember : ScanMemberBase
    {
        /// <summary>
        /// Опрашивать данные событий
        /// </summary>
        public bool ScanEventData { get; set; }

        /// <summary>
        /// Опрашивать данные аварий
        /// </summary>
        public bool ScanAlarmData { get; set; }

        /// <summary>
        /// Опрашивать минутные данные
        /// </summary>
        public bool ScanMinuteData { get; set; }

        /// <summary>
        /// Опрашивать периодические данные
        /// </summary>
        public bool ScanPeriodicData { get; set; }

        /// <summary>
        /// Опрашивать суточные данные
        /// </summary>
        public bool ScanDailyData { get; set; }

        /// <summary>
        /// Возвращает текстовое описание элемента
        /// </summary>
        public override string ToString()
        {
            return $"ROC, Id = {EstimatorId}";
        }
    }
}