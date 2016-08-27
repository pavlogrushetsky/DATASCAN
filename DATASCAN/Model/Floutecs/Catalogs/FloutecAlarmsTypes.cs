using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы аварий вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("FloutecAlarmsTypes")]
    public class FloutecAlarmsTypes : CatalogBase
    {
        /// <summary>
        /// Описание кода аварии для вычислителей с версией ПО от 45 включительно
        /// </summary>
        [Required]
        [MaxLength(400)]
        public string Description_45 { get; set; }
    }
}
