using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Scanning
{
    /// <summary>
    /// Сущность "Период опроса"
    /// </summary>
    [Table("Periods", Schema = "Scan")]
    public class ScanPeriod : EntityBase
    {
        /// <summary>
        /// Период (время) опроса
        /// </summary>
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan Period { get; set; }

        /// <summary>
        /// Первичный ключ опроса данных
        /// </summary>
        public int ScanId { get; set; }

        /// <summary>
        /// Опрос данных
        /// </summary>
        [ForeignKey("ScanId")]
        public virtual ScheduledScan Scan { get; set; }
    }
}
