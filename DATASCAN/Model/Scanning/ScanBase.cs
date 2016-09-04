using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Scanning
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
        /// Коллекция элементов опроса
        /// </summary>
        public virtual ICollection<ScanMember> Members { get; private set; } = new HashSet<ScanMember>(); 
    }
}
