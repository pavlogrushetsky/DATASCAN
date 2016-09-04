using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Floutecs.Common;

namespace DATASCAN.Model.Floutecs
{
    /// <summary>
    /// Сущность "Мгновенные данные вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("InstantData", Schema = "Floutec")]
    public class FloutecInstantData : FloutecDataBase
    {
        /// <summary>
        /// Дата и время момента опроса данных
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DAT { get; set; }

        /// <summary>
        /// Объём с начала часа
        /// </summary>
        [Required]
        public double QHOUR { get; set; }

        /// <summary>
        /// Объём за предыдущий час
        /// </summary>
        [Required]
        public double PQHOUR { get; set; }

        /// <summary>
        /// Текущий расход
        /// </summary>
        [Required]
        public double CURRSPEND { get; set; }

        /// <summary>
        /// Объём с начала контрактных суток
        /// </summary>
        [Required]
        public double DAYSPEND { get; set; }

        /// <summary>
        /// Объём за прошлые сутки
        /// </summary>
        [Required]
        public double YESTSPEND { get; set; }

        /// <summary>
        /// Объём с начала месяца
        /// </summary>
        [Required]
        public double MONTHSPEND { get; set; }

        /// <summary>
        /// Объём за прошлый месяц
        /// </summary>
        [Required]
        public double LASTMONTHSPEND { get; set; }

        /// <summary>
        /// Общий объём
        /// </summary>
        [Required]
        public double ALLSPEND { get; set; }

        /// <summary>
        /// Объём при с.у. за текущие сутки, накопленный при аварийных ситуациях
        /// </summary>
        [Required]
        public double ALARMSY { get; set; }

        /// <summary>
        /// Объём при р.у. за текущие сутки, накопленный при аварийных ситуациях
        /// </summary>
        [Required]
        public double ALARMRY { get; set; }

        /// <summary>
        /// Объём при с.у. за предыдущие сутки, накопленный при аварийных ситуациях
        /// </summary>
        [Required]
        public double PALARMSY { get; set; }

        /// <summary>
        /// Объём при р.у. за предыдущие сутки, накопленный при аварийных ситуациях
        /// </summary>
        [Required]
        public double PALARMRY { get; set; }

        /// <summary>
        /// Перепад давления (для счётчика - приращение объёма при р.у.)
        /// </summary>
        [Required]
        public double PERPRES { get; set; }

        /// <summary>
        /// Признак константы для перепада давления (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PP { get; set; }

        /// <summary>
        /// Статическое давление
        /// </summary>
        [Required]
        public double STPRES { get; set; }

        /// <summary>
        /// Признак константы для статического давления (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PD { get; set; }

        /// <summary>
        /// Абсолютное давление
        /// </summary>
        [Required]
        public double ABSP { get; set; }

        /// <summary>
        /// Температура
        /// </summary>
        [Required]
        public double TEMP { get; set; }

        /// <summary>
        /// Признак константы для температуры (* - константа)
        /// </summary>
        [MaxLength(1)]
        public string PT { get; set; }

        /// <summary>
        /// Вязкость газа
        /// </summary>
        [Required]
        public double GASVIZ { get; set; }

        /// <summary>
        /// Корень квадратный (значение плотности, если измеряется)
        /// </summary>
        [Required]
        public double SQROOT { get; set; }

        /// <summary>
        /// Плотность газа при р.у.
        /// </summary>
        [Required]
        public double GAZPLOTNRU { get; set; }

        /// <summary>
        /// Плотность газа при н.у.
        /// </summary>
        [Required]
        public double GAZPLOTNNU { get; set; }

        /// <summary>
        /// Суммарная длительность аварийных ситуаций за текущие сутки, сек.
        /// </summary>
        [Required]
        public int DLITAS { get; set; }

        /// <summary>
        /// Длительность измерительных аварийных ситуаций за текущие сутки, сек.
        /// </summary>
        [Required]
        public int DLITBAS { get; set; }

        /// <summary>
        /// Длительность методических аварийных ситуаций за текущие сутки, сек.
        /// </summary>
        [Required]
        public int DLITMAS { get; set; }

        /// <summary>
        /// Суммарная длительность аварийных ситуаций за предыдущие сутки, сек.
        /// </summary>
        [Required]
        public int PDLITAS { get; set; }

        /// <summary>
        /// Длительность измерительных аварийных ситуаций за предыдущие сутки, сек.
        /// </summary>
        [Required]
        public int PDLITBAS { get; set; }

        /// <summary>
        /// Длительность методических аварийных ситуаций за предыдущие сутки, сек.
        /// </summary>
        [Required]
        public int PDLITMAS { get; set; }

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