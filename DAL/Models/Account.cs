using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Account
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public User User { get; set; } = null!;
        public required decimal Balance { get; set; } = 0!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
