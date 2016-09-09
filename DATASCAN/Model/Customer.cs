using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model
{
    /// <summary>
    /// Сущность "Заказчик"
    /// </summary>
    [Table("Customers", Schema = "General")]
    public class Customer : EntityBase
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Контактное лицо
        /// </summary>
        [MaxLength(100)]
        public string Person { get; set; }

        /// <summary>
        /// Контактный номер телефона
        /// </summary>
        [MaxLength(13)]
        public string Phone { get; set; }

        /// <summary>
        /// Контактный электронный адрес
        /// </summary>
        [MaxLength(200)]
        public string Email { get; set; }

        /// <summary>
        /// Коллекция вычислителей
        /// </summary>
        public virtual ICollection<EstimatorBase> Estimators { get; private set; } = new HashSet<EstimatorBase>(); 

        /// <summary>
        /// Коллекция групп вычислителей
        /// </summary>
        public virtual ICollection<EstimatorsGroup> Groups { get; set; } = new HashSet<EstimatorsGroup>(); 
    }
}
