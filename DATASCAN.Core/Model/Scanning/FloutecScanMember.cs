using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Core.Model.Scanning
{
    /// <summary>
    /// Сущность "Элемент опроса данных вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("FloutecMembers", Schema = "Scan")]
    public class FloutecScanMember : ScanMemberBase
    {
        /// <summary>
        /// Опрашивать данные идентификации
        /// </summary>
        public bool ScanIdentData { get; set; }

        /// <summary>
        /// Опрашивать данные аварий
        /// </summary>
        public bool ScanAlarmData { get; set; }

        /// <summary>
        /// Опрашивать мгновенные данные
        /// </summary>
        public bool ScanInstantData { get; set; }

        /// <summary>
        /// Опрашивать данные вмешательств
        /// </summary>
        public bool ScanInterData { get; set; }

        /// <summary>
        /// Опрашивать часовые данные
        /// </summary>
        public bool ScanHourlyData { get; set; }

        /// <summary>
        /// Возвращает текстовое описание элемента
        /// </summary>
        public override string ToString()
        {
            return $"ФЛОУТЕК, Id = {EstimatorId}";
        }
    }
}