using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Model.Common;

namespace DATASCAN.Core.Model.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы сенсоров для вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("SensorTypes", Schema = "Floutec")]
    public class FloutecSensorTypes : CatalogBase
    {
    }
}
