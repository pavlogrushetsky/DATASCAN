using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;
using DATASCAN.Model.Scanning;

namespace DATASCAN.Model
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Вычислитель"
    /// </summary>
    [Table("Estimators")]
    public abstract class EstimatorBase : EntityBase
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
        /// Номер телефона дозвона
        /// </summary>
        [MaxLength(13)]
        public string Phone { get; set; }

        /// <summary>
        /// Первичный ключ заказчика
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Заказчик
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Коллекция точек измерения
        /// </summary>
        public virtual ICollection<MeasurePointBase> MeasurePoints { get; private set; } = new HashSet<MeasurePointBase>(); 

        /// <summary>
        /// Коллекция элементов опроса данных
        /// </summary>
        public virtual ICollection<ScanMember> Scans { get; private set; } = new HashSet<ScanMember>();
    }
}
