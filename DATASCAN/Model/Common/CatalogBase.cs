﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Common
{
    /// <summary>
    /// Абстрактный класс общего содержания справочных таблиц
    /// </summary>
    public abstract class CatalogBase
    {
        /// <summary>
        /// Код записи
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        /// <summary>
        /// Описание записи
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string Description { get; set; }
    }
}