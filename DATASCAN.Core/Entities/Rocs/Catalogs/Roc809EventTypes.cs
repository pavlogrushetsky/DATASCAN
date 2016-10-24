using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы событий вычислителей ROC809"
    /// </summary>
    [Table("EventTypes", Schema = "Roc809")]
    public class Roc809EventTypes : CatalogBase
    {
    }
}
