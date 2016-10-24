using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Core.Entities.Common;

namespace DATASCAN.Core.Entities.Rocs
{
    /// <summary>
    /// Сущность "Данные аварий вычислителя ROC809"
    /// </summary>
    [Table("AlarmData", Schema = "Roc809")]
    public class Roc809AlarmData : DataRecordBase
    {
        /// <summary>
        /// Тип аварии
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// Дата и время аварии
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Флаг посылки отчёта по исключению
        /// </summary>
        [Required]
        public bool SRBX { get; set; }

        /// <summary>
        /// Флаг состояния аварии (1 - установлена)
        /// </summary>
        [Required]
        public bool Condition { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public int? T { get; set; }

        /// <summary>
        /// Location
        /// </summary>
        public int? L { get; set; }

        /// <summary>
        /// Parameter
        /// </summary>
        public int? P { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Value { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [Required]
        [MaxLength(10)]
        public string Description { get; set; }

        /// <summary>
        /// Дополнительный код типа аварии
        /// </summary>
        public int? Code { get; set; }

        /// <summary>
        /// Номер записи в таблице последовательности функций
        /// </summary>
        public int? FST { get; set; }

        /// <summary>
        /// Первичный ключ вычислителя
        /// </summary>
        public int Roc809Id { get; set; }

        /// <summary>
        /// Вычислитель
        /// </summary>
        [ForeignKey("Roc809Id")]
        public virtual Roc809 Roc809 { get; set; }
    }
}
