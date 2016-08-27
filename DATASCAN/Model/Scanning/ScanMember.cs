using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Scanning
{
    /// <summary>
    /// Сущность "Элемент опроса данных"
    /// </summary>
    [Table("ScanMembers")]
    public class ScanMember : EntityBase
    {
        /// <summary>
        /// Первичный ключ вычислителя
        /// </summary>
        public int EstimatorId { get; set; }

        /// <summary>
        /// Вычислитель
        /// </summary>
        [ForeignKey("EstimatorId")]
        public virtual EstimatorBase Estimator { get; set; }

        /// <summary>
        /// Первичный ключ точки измерения
        /// </summary>
        public int? MeasurePointId { get; set; }

        /// <summary>
        /// Точка измерения
        /// </summary>
        [ForeignKey("MeasurePointId")]
        public virtual MeasurePointBase MeasurePoint { get; set; }

        /// <summary>
        /// Тип опрашиваемых данных
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string DataType { get; set; }

        /// <summary>
        /// Первичный ключ опроса данных
        /// </summary>
        public int ScanBaseId { get; set; }

        /// <summary>
        /// Опрос данных
        /// </summary>
        [ForeignKey("ScanBaseId")]
        public virtual ScanBase Scan { get; set; }
    }
}
