using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Model.Common;

namespace DATASCAN.Core.Model.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы параметров вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("ParamTypes", Schema = "Floutec")]
    public class FloutecParamTypes : CatalogBase
    {
        /// <summary>
        /// Аббревиатура параметра
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Param { get; set; }
    }
}
