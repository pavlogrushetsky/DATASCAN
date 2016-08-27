using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Rocs.Common;

namespace DATASCAN.Model.Rocs
{
    /// <summary>
    /// Сущность "Суточные данные вычислителя ROC809"
    /// </summary>
    [Table("Roc809DailyData")]
    public class Roc809DailyData : Roc809PeriodicDataBase
    {
        /// <summary>
        /// Первичный ключ точки измерения
        /// </summary>
        public int Roc809MeasurePointId { get; set; }

        /// <summary>
        /// Точка измерения
        /// </summary>
        [ForeignKey("Roc809MeasurePointId")]
        public virtual Roc809MeasurePoint MeasurePoint { get; set; }
    }
}
