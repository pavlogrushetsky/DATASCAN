using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Rocs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Дополнительные коды типов событий вычислителей ROC809"
    /// </summary>
    [Table("EventCodes", Schema = "Roc809")]
    public class Roc809EventCodes : CatalogBase
    {
    }
}
