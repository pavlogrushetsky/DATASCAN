using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы параметров вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("FloutecParamsTypes")]
    public class FloutecParamsTypes : CatalogBase
    {
        /// <summary>
        /// Аббревиатура параметра
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Param { get; set; }
    }
}
