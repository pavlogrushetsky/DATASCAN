using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Common
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Идентификатор (первичный ключ) сущности
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата и время создания (добавления) сущности
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Дата и время изменения (модификации) сущности
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Признак активности сущности
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}
