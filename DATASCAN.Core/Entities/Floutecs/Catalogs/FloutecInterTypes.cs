using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы вмешательств вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("InterTypes", Schema = "Floutec")]
    public class FloutecInterTypes : CatalogBase
    {
        /// <summary>
        /// Описание кода вмешательства для вычислителей с версией ПО от 45 включительно
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [MaxLength(400)]
        public string Description_45 { get; set; }
    }
}
