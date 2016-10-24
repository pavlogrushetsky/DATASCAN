using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы сенсоров для вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("SensorTypes", Schema = "Floutec")]
    public class FloutecSensorTypes : CatalogBase
    {
    }
}
