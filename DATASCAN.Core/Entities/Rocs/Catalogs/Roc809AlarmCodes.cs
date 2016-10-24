using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Дополнительные коды типов аварий вычислителей ROC809"
    /// </summary>
    [Table("AlarmCodes", Schema = "Roc809")]
    public class Roc809AlarmCodes : CatalogBase
    {
    }
}
