﻿using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Rocs.Common;

namespace DATASCAN.Model.Rocs
{
    /// <summary>
    /// Сущность "Минутные данные вычислителя ROC809"
    /// </summary>
    [Table("Roc809MinuteData")]
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
