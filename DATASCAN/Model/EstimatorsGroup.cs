using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model
{
    /// <summary>
    /// Сущность "Группа вычислителей"
    /// </summary>
    [Table("Groups", Schema = "General")]
    public class EstimatorsGroup : EntityBase
    {
        /// <summary>
        /// Первичный ключ заказчика
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Заказчик
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Коллекция вычислителей
        /// </summary>
        public virtual ICollection<EstimatorBase> Estimators { get; set; } = new HashSet<EstimatorBase>(); 
    }
}
