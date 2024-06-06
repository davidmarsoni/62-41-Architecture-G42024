using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class AppUserPayOnlineViewModel
    {
        [Required(ErrorMessage = "AccountId is required")]
        public int AccountId { get; set; }
        [Required(ErrorMessage = "Amount is required")]
        [Range(5, 1000, ErrorMessage = "Please enter a value between {1} and {2}")]
        public decimal Amount { get; set; }
    }
}
