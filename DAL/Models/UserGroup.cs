using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserGroup
    {
        public int Id { get; set; } 
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
        public required int GroupId { get; set; }
        public Group Group { get; set; } = null!;
    }
}