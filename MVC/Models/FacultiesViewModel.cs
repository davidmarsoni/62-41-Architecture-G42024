using DTO;

namespace MVC.Models
{
    public class FacultiesViewModel
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public List<UserViewModel> SelectedUsers { get; set; }

        public string Username { get; set; }
        public string GroupName { get; set; }
    }
}
