using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Scanning
{
    /// <summary>
    /// Сущность "Элемент опроса данных"
    /// </summary>
    [Table("Members", Schema = "Scan")]
    public abstract class ScanMemberBase : EntityBase
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
