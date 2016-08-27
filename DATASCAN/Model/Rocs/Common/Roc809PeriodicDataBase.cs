using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Rocs.Common
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Периодические данные вычислителя ROC809"
    /// </summary>
    public abstract class Roc809PeriodicDataBase : DataRecordBase
    {       
        /// <summary>
        /// Период накопления (усреднения) данных
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Period { get; set; }

        /// <summary>
        /// Накопленное (усреднённое) значение
        /// </summary>
        [Required]
        public double Value { get; set; }
    }
}
