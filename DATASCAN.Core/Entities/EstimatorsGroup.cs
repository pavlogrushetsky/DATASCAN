﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities
{
    /// <summary>
    /// Сущность "Группа вычислителей"
    /// </summary>
    [Table("Groups", Schema = "General")]
    public class EstimatorsGroup : EntityBase
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

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

        /// <summary>
        /// Возвращает текстовое описание группы вычислителей
        /// </summary>
        public override string ToString()
        {
            return $"{Name}, Id = {Id}";
        }
    }
}
