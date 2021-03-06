﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Core.Entities.Floutecs
{
    /// <summary>
    /// Сущность "Нитка измерения вычислителя ФЛОУТЭК"
    /// </summary>
    [Table("MeasureLines", Schema = "Floutec")]
    public class FloutecMeasureLine : MeasurePointBase
    {        
        /// <summary>
        /// Тип датчика
        /// </summary>
        [Required]
        public int SensorType { get; set; }

        /// <summary>
        /// Коллекция часовых данных
        /// </summary>
        public virtual ICollection<FloutecHourlyData> HourlyData { get; private set; } = new HashSet<FloutecHourlyData>();

        /// <summary>
        /// Коллекция мгновенных данных
        /// </summary>
        public virtual ICollection<FloutecInstantData> InstantData { get; private set; } = new HashSet<FloutecInstantData>();

        /// <summary>
        /// Коллекция данных идентификации
        /// </summary>
        public virtual ICollection<FloutecIdentData> IdentData { get; private set; } = new HashSet<FloutecIdentData>();

        /// <summary>
        /// Коллекция данных вмешательств
        /// </summary>
        public virtual ICollection<FloutecInterData> InterData { get; private set; } = new HashSet<FloutecInterData>();

        /// <summary>
        /// Коллекция данных аварий
        /// </summary>
        public virtual ICollection<FloutecAlarmData> AlarmData { get; private set; } = new HashSet<FloutecAlarmData>();

        /// <summary>
        /// Возвращает текстовое описание нитки измерения
        /// </summary>
        public override string ToString()
        {
            return $"{Name}, Id = {Id}";
        }
    }
}
