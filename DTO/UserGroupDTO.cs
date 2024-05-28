using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserGroupDTO
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Acronym { get; set; }
    }
}
