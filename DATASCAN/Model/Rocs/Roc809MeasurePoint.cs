using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Rocs
{
    /// <summary>
    /// Сущность "Точка измерения вычислителя ROC809"
    /// </summary>
    [Table("MeasurePoints", Schema = "Roc809")]
    public class Roc809MeasurePoint : MeasurePointBase
    {
        /// <summary>
        /// Исторический сегмент
        /// </summary>
        [Required]
        public int HistSegment { get; set; }

        /// <summary>
        /// Дата и время последнего обновления минутных данных
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? DateMinuteDataLastUpdated { get; set; }

        /// <summary>
        /// Дата и время последнего обновления периодических данных
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? DatePeriodicDataLastUpdated { get; set; }

        /// <summary>
        /// Дата и время последнего обновления суточных данных
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? DateDailyDataLastUpdated { get; set; }

        /// <summary>
        /// Коллекция минутных данных
        /// </summary>
        public virtual ICollection<Roc809MinuteData> MinuteData { get; private set; } = new HashSet<Roc809MinuteData>();
        
        /// <summary>
        /// Коллекция периодических данных
        /// </summary>
        public virtual ICollection<Roc809PeriodicData> PeriodicData { get; private set; } = new HashSet<Roc809PeriodicData>();
        
        /// <summary>
        /// Коллекция суточных данных
        /// </summary>
        public virtual ICollection<Roc809DailyData> DailyData { get; private set; } = new HashSet<Roc809DailyData>();

        /// <summary>
        /// Возвращает текстовое описание точки измерения
        /// </summary>
        public override string ToString()
        {
            return $"{Name}, Id = {Id}";
        }
    }
}
