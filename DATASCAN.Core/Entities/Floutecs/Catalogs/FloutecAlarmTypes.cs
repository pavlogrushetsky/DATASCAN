using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы аварий вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("AlarmTypes", Schema = "Floutec")]
    public class FloutecAlarmTypes : CatalogBase
    {
        /// <summary>
        /// Описание кода аварии для вычислителей с версией ПО от 45 включительно
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [MaxLength(400)]
        public string Description_45 { get; set; }
    }
}
