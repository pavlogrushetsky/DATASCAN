using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Floutecs.Common;

namespace DATASCAN.Model.Floutecs
{
    /// <summary>
    /// Сущность "Часовые данные вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("HourlyData", Schema = "Floutec")]
    public class FloutecHourlyData : FloutecDataBase
    {
        /// <summary>
        /// Дата и время начала периода накопления
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DAT { get; set; }

        /// <summary>
        /// Дата и время окончания периода накопления
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DAT_END { get; set; }

        /// <summary>
        /// Количество, м3 или т
        /// </summary>
        [Required]
        public double RASX { get; set; }

        /// <summary>
        /// Среднее давление
        /// </summary>
        [Required]
        public double DAVL { get; set; }

        /// <summary>
        /// Признак константы среднего давления (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PD { get; set; }

        /// <summary>
        /// Средняя температура
        /// </summary>
        [Required]
        public double TEMP { get; set; }

        /// <summary>
        /// Признак константы средней температуры (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PT { get; set; }

        /// <summary>
        /// Средний перепад давления
        /// </summary>
        [Required]
        public double PEREP { get; set; }

        /// <summary>
        /// Признак константы среднего перепада давления (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PP { get; set; }

        /// <summary>
        /// Средняя плотность
        /// </summary>
        [Required]
        public double PLOTN { get; set; }

        /// <summary>
        /// Признак константы средней плотности (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PL { get; set; }

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