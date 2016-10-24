using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Model.Floutecs.Common;

namespace DATASCAN.Core.Model.Floutecs
{
    /// <summary>
    /// Сущность "Данные аварий вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("AlarmData", Schema = "Floutec")]
    public class FloutecAlarmData : FloutecDataBase
    {
        /// <summary>
        /// Дата и время возникновения аварии
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DAT { get; set; }

        /// <summary>
        /// Тип аварии
        /// </summary>
        [Required]
        public int T_AVAR { get; set; }

        /// <summary>
        /// Тип параметра
        /// </summary>
        [Required]
        public int T_PARAM { get; set; }

        /// <summary>
        /// Поле данных (объём с начала суток)
        /// </summary>
        [Required]
        public double VAL { get; set; }

        /// <summary>
        /// Первичный ключ нитки измерения 
        /// </summary>
        public int FloutecMeasureLineId { get; set; }

        /// <summary>
        /// Нитка измерения
        /// </summary>
        [ForeignKey("FloutecMeasureLineId")]
        public virtual FloutecMeasureLine MeasureLine { get; set; }
    }
}