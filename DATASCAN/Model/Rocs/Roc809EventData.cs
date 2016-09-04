using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DATASCAN.Model.Common;

namespace DATASCAN.Model.Rocs
{
    /// <summary>
    /// Сущность "Данные событий вычислителя ROC809"
    /// </summary>
    [Table("EventData", Schema = "Roc809")]
    public class Roc809EventData : DataRecordBase
    {
        /// <summary>
        /// Тип события
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// Дата и время события
        /// </summary>
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Идентификатор оператора
        /// </summary>
        [Required]
        public string OperatorId { get; set; }

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
        [MaxLength(20)]
        public string Value { get; set; }

        /// <summary>
        /// Новое значение
        /// </summary>
        [MaxLength(20)]
        public string NewValue { get; set; }

        /// <summary>
        /// Старое значение
        /// </summary>
        [MaxLength(20)]
        public string OldValue { get; set; }

        /// <summary>
        /// Некалиброванное значение
        /// </summary>
        [MaxLength(20)]
        public string RawValue { get; set; }

        /// <summary>
        /// Калиброванное значение
        /// </summary>
        [MaxLength(20)]
        public string CalibratedValue { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [MaxLength(20)]
        public string Description { get; set; }

        /// <summary>
        /// Дополнительный код типа события
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
