using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Model.Common;

namespace DATASCAN.Core.Model.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы аварий вычислителей ROC809"
    /// </summary>
    [Table("AlarmTypes", Schema = "Roc809")]
    public class Roc809AlarmTypes : CatalogBase
    {
    }
}
