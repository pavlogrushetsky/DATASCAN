using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Floutecs.Catalogs
{
    /// <summary>
    /// Справочная таблица "Типы сенсоров для вычислителей ФЛОУТЭК"
    /// </summary>
    [Table("SensorTypes", Schema = "Floutec")]
    public class FloutecSensorTypes : CatalogBase
    {
    }
}
