using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;
using DATASCAN.Model.Scanning;

namespace DATASCAN.Model
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Точка измерения"
    /// </summary>
    [Table("MeasurePoints")]
    public abstract class MeasurePointBase : EntityBase
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

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
        /// Коллекция элементов опроса данных
        /// </summary>
        public virtual ICollection<ScanMember> Scans { get; private set; } = new HashSet<ScanMember>();
    }
}
