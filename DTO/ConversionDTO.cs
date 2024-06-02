using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class ConversionDTO
    {
        public int ConversionId { get; set; }

        [Required (ErrorMessage = "An Conversion name is required")]
        [StringLength(100)]
        public string ConversionName { get; set; } = string.Empty;

        [Required (ErrorMessage = "A conversion value is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal ConversionValue { get; set; }
    }
}