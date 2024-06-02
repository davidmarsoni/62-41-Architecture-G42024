using DTO;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        // user fields

        public string Username { get; set; }
        public string? LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        //account fields
        public int AccountId { get; set; }
        public decimal Balance { get; set; }

        // groups fields
        public List<GroupDTO> Groups { get; set; }


        public string DisplayName => $"{FirstName} {LastName} ({Email})";


        public UserViewModel()
        {
        }

        public UserViewModel(UserDTO userDTO, AccountDTO accountDTO, List<GroupDTO> groupsDTO)
        {
            if(userDTO != null)
            {
                UserId = userDTO.UserId;
                Username = userDTO.Username;
                LastName = userDTO.LastName;
                FirstName = userDTO.FirstName;
                Gender = userDTO.Gender;
                Address = userDTO.Address;
                Email = userDTO.Email;
                IsDeleted = userDTO.IsDeleted;
            }
            if(accountDTO != null)
            {
                AccountId = accountDTO.AccountId;
                Balance = accountDTO.Balance;
            }
            if (groupsDTO != null)
            {
                Groups = groupsDTO;
            }
        }
    }
}
