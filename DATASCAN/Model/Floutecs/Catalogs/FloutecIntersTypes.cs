using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы вмешательств вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("FloutecIntersTypes")]
    public class FloutecIntersTypes : CatalogBase
    {
        /// <summary>
        /// Описание кода вмешательства для вычислителей с версией ПО от 45 включительно
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string Description_45 { get; set; }
    }
}
