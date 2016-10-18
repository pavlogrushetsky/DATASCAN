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
        /// Период опроса
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Тип периода опроса: 0-минут, 1-часов
        /// </summary>
        public bool PeriodType { get; set; }        

        /// <summary>
        /// Возвращает текстовое описание опроса
        /// </summary>
        public override string ToString()
        {
            return $"{Title}, Id = {Id}";
        }
    }
}
