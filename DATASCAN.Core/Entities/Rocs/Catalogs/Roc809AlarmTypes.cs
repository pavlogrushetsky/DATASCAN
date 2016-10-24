using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы аварий вычислителей ROC809"
    /// </summary>
    [Table("AlarmTypes", Schema = "Roc809")]
    public class Roc809AlarmTypes : CatalogBase
    {
    }
}
