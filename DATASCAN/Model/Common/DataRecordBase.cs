using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Common
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Данные"
    /// </summary>
    public abstract class DataRecordBase
    {
        /// <summary>
        /// Идентификатор (первичный ключ)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Дата и время создания (добавления)
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DateAdded { get; set; }
    }
}
