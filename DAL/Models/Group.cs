using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string? Acronym { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; } = new List<User>();
        public ICollection<User_Group> User_Groups { get;  } = new List<User_Group>();

    }
}