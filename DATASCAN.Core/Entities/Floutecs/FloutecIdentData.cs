using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Floutecs.Common;

namespace DATASCAN.Core.Entities.Floutecs
{
    /// <summary>
    /// Сущность "Данные идентификации вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("IdentData", Schema = "Floutec")]
    public class FloutecIdentData : FloutecDataBase
    {
        /// <summary>
        /// Контрактный час
        /// </summary>
        [Required]
        [Column(TypeName = "time")]
        public TimeSpan KONTRH { get; set; }

        /// <summary>
        /// Метод измерения расхода (0 - перепад, 1 - счётчик)
        /// </summary>
        [Required]
        public int SCHET { get; set; }

        /// <summary>
        /// Молярная концентрация N2, %
        /// </summary>
        [Required]
        public double NO2 { get; set; }

        /// <summary>
        /// Тип отбора (0 - угловой, 1 - фланцевый)
        /// </summary>
        [Required]
        public int OTBOR { get; set; }

        /// <summary>
        /// Коэффициент Ае для расчёта КТР трубы
        /// </summary>
        [Required]
        public double ACP { get; set; }

        /// <summary>
        /// Коэффициент Ае для расчёта КТР СУ
        /// </summary>
        [Required]
        public double ACS { get; set; }

        /// <summary>
        /// Коэффициент шероховатости трубопровода
        /// </summary>
        [Required]
        public double SHER { get; set; }

        /// <summary>
        /// Верхний предел измерения давления, кг/см2
        /// </summary>
        [Required]
        public double VERXP { get; set; }

        /// <summary>
        /// Плотность газа, если не измеряется
        /// </summary>
        [Required]
        public double PLOTN { get; set; }

        /// <summary>
        /// Диаметр трубы
        /// </summary>
        [Required]
        public double DTRUB { get; set; }

        /// <summary>
        /// Отсечка
        /// </summary>
        [Required]
        public double OTSECH { get; set; }

        /// <summary>
        /// Коэффициент Ве для расчёта КТР трубы
        /// </summary>
        [Required]
        public double BCP { get; set; }

        /// <summary>
        /// Коэффициент Ве для расчёта КТР СУ
        /// </summary>
        [Required]
        public double BCS { get; set; }

        /// <summary>
        /// Верхний предел измерения перепада давления, кг/м2
        /// </summary>
        [Required]
        public double VERXDP { get; set; }

        /// <summary>
        /// Нижний предел измерения температуры
        /// </summary>
        [Required]
        public double NIZT { get; set; }

        /// <summary>
        /// Молярная концентрация СО2, %
        /// </summary>
        [Required]
        public double CO2 { get; set; }

        /// <summary>
        /// Диаметр СУ
        /// </summary>
        [Required]
        public double DSU { get; set; }

        /// <summary>
        /// Коэффициент Се для расчёта КТР трубы
        /// </summary>
        [Required]
        public double CCP { get; set; }

        /// <summary>
        /// Коэффициент Се для расчёта КТР СУ
        /// </summary>
        [Required]
        public double CCS { get; set; }

        /// <summary>
        /// Нижний предел измерения давления, кг/см2
        /// </summary>
        [Required]
        public double NIZP { get; set; }

        /// <summary>
        /// Верхний предел измерения температуры
        /// </summary>
        [Required]
        public double VERXT { get; set; }

        /// <summary>
        /// Тип субстанции (0 - газ, 1 - конденсат)
        /// </summary>
        [Required]
        public int KONDENS { get; set; }

        /// <summary>
        /// Количество импульсов на 1 м3 счётчика
        /// </summary>
        [Required]
        public double KALIBSCH { get; set; }

        /// <summary>
        /// Расход, при котором счётчик останавливается, м3/час
        /// </summary>
        [Required]
        public double MINRSCH { get; set; }

        /// <summary>
        /// Максимально допустимый расход через счётчик, м3/час
        /// </summary>
        [Required]
        public double MAXRSCH { get; set; }

        /// <summary>
        /// Версия программного обеспечения
        /// </summary>
        [Required]
        public double TYPDAN { get; set; }

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