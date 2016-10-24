using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Model.Common;

namespace DATASCAN.Core.Model.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Дополнительные коды типов событий вычислителей ROC809"
    /// </summary>
    [Table("EventCodes", Schema = "Roc809")]
    public class Roc809EventCodes : CatalogBase
    {
    }
}
