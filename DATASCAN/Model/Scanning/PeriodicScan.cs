using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Scanning
{
    /// <summary>
    /// Сущность "Периодический опрос данных"
    /// </summary>
    [Table("PeriodicScans", Schema = "Scan")]
    public class PeriodicScan : ScanBase
    {
        /// <summary>
        /// Первичный ключ периода опроса
        /// </summary>
        public int ScanPeriodId { get; set; }

        /// <summary>
        /// Период опроса
        /// </summary>
        [ForeignKey("ScanPeriodId")]
        public virtual ScanPeriod Period { get; set; }
    }
}
