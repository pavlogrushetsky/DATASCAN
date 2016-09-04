using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Floutecs.Common;

namespace DATASCAN.Model.Floutecs
{
    /// <summary>
    /// Сущность "Данные вмешательств вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("InterData", Schema = "Floutec")]
    public class FloutecInterData : FloutecDataBase
    {
        /// <summary>
        /// Дата и время вмешательства
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DAT { get; set; }

        /// <summary>
        /// Тип вмешательства
        /// </summary>
        [Required]
        public int CH_PAR { get; set; }

        /// <summary>
        /// Тип параметра
        /// </summary>
        public int? T_PARAM { get; set; }

        /// <summary>
        /// Старое значение
        /// </summary>
        [MaxLength(20)]
        public string VAL_OLD { get; set; }

        /// <summary>
        /// Новое значение
        /// </summary>
        [MaxLength(20)]
        public string VAL_NEW { get; set; }

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