﻿using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Rocs.Common;

namespace DATASCAN.Core.Entities.Rocs
{
    /// <summary>
    /// Сущность "Минутные данные вычислителя ROC809"
    /// </summary>
    [Table("MinuteData", Schema = "Roc809")]
    public class Roc809MinuteData : Roc809PeriodicDataBase
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
