using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Scanning
{
    /// <summary>
    /// Сущность "Опрос данных по расписанию"
    /// </summary>
    [Table("SheduledScans", Schema = "Scan")]
    public class ScheduledScan : ScanBase
    {
        /// <summary>
        /// Коллекция периодов опроса
        /// </summary>
        public virtual ICollection<ScanPeriod> Periods { get; private set; } = new HashSet<ScanPeriod>();

        /// <summary>
        /// Возвращает текстовое описание опроса
        /// </summary>
        public override string ToString()
        {
            return $"{Title}, Id = {Id}";
        }
    }
}
