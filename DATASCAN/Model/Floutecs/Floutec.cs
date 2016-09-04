using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATASCAN.Model.Floutecs
{
    /// <summary>
    /// Сущность "Вычислитель ФЛОУТЭК"
    /// </summary>
    [Table("Estimators", Schema = "Floutec")]
    public class Floutec : EstimatorBase
    {
        /// <summary>
        /// Адрес
        /// </summary>
        [Required]
        public int Address { get; set; }
    }
}