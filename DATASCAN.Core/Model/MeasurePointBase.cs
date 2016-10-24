using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Model.Common;

namespace DATASCAN.Core.Model
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Точка измерения"
    /// </summary>
    [Table("MeasurePoints", Schema = "General")]
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
        /// Номер
        /// </summary>
        [Required]
        public int Number { get; set; }

        /// <summary>
        /// Первичный ключ вычислителя
        /// </summary>
        public int EstimatorId { get; set; }

        /// <summary>
        /// Вычислитель
        /// </summary>
        [ForeignKey("EstimatorId")]
        public virtual EstimatorBase Estimator { get; set; }
    }
}
