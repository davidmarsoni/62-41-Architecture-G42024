using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ConversionDTO
    {
        public int ConversionId { get; set; }

        [Required]
        [StringLength(100)]
        public string ConversionName { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal ConversionValue { get; set; }
    }
}