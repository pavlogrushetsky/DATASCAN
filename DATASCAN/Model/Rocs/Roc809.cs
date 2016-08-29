﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Rocs
{
    /// <summary>
    /// Сущность "Вычислитель ROC809"
    /// </summary>
    [Table("Roc809s")]
    public class Roc809 : EstimatorBase
    {
        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string Address { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        [Required]
        public int Port { get; set; }

        /// <summary>
        /// RocUnit
        /// </summary>
        [Required]
        public int RocUnit { get; set; }

        /// <summary>
        /// RocGroup
        /// </summary>
        [Required]
        public int RocGroup { get; set; }

        /// <summary>
        /// HostUnit
        /// </summary>
        [Required]
        public int HostUnit { get; set; }

        /// <summary>
        /// HostGroup
        /// </summary>
        [Required]
        public int HostGroup { get; set; }

        /// <summary>
        /// Дата и время последнего обновления данных аварий
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? DateAlarmDataLastUpdated { get; set; }

        /// <summary>
        /// Дата и время последнего обновления данных событий
        /// </summary>
        [Column(TypeName = "datetime2")]
        public DateTime? DateEventDataLastUpdated { get; set; }

        /// <summary>
        /// Коллекция данных аварий
        /// </summary>
        public virtual ICollection<Roc809AlarmData> AlarmData { get; private set; } = new HashSet<Roc809AlarmData>();

        /// <summary>
        /// Коллекция данных событий
        /// </summary>
        public virtual ICollection<Roc809EventData> DailyData { get; private set; } = new HashSet<Roc809EventData>();
    }
}