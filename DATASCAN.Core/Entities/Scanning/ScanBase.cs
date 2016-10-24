using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Scanning
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Опрос данных"
    /// </summary>
    [Table("Scans", Schema = "Scan")]
    public abstract class ScanBase : EntityBase
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Дата и время последнего опроса
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? DateLastScanned { get; set; }

        /// <summary>
        /// Коллекция элементов опроса
        /// </summary>
        public virtual ICollection<ScanMemberBase> Members { get; private set; } = new HashSet<ScanMemberBase>(); 
    }
}
