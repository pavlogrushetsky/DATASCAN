using System.ComponentModel.DataAnnotations;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Floutecs.Common
{
    /// <summary>
    /// Абстрактный класс общего содержания сущностей "Данные вычислителя ФЛОУТЭК"
    /// </summary>
    public abstract class FloutecDataBase : DataRecordBase
    {
        /// <summary>
        /// Адрес вычислителя * 10 + номер нитки измерения
        /// </summary>
        [Required]
        public int N_FLONIT { get; set; }
    }
}