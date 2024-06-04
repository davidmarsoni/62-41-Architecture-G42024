using DAL.Models;
using DTO;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class AppUserBuyViewModel
    {
        [Required(ErrorMessage = "AccountId is required")]
        public int AccountId { get; set; } // temporary, will be replaced when the login system is implemented
        [Required(ErrorMessage = "ConversionId is required")]
        public int ConversionId { get; set; }
        public decimal? ConversionValue { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 1000, ErrorMessage = "Please enter a value between {1} and {2}")]
        public int Quantity { get; set; }
        public decimal calculatedPrice
        {
            get
            {
                if (ConversionValue == null)
                {
                    return 0;
                }
                decimal totalPrice = (decimal)ConversionValue * Quantity;
                if (totalPrice < 0)
                {
                    return 0;
                }
                else
                {
                    return totalPrice;
                }
            }
        }
    }
}
