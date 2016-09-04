using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы событий вычислителей ROC809"
    /// </summary>
    [Table("EventTypes", Schema = "Roc809")]
    public class Roc809EventTypes : CatalogBase
    {
    }
}
