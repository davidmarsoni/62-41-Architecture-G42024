using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class UserRoles
    {
        public const string Admin = "admin";
        public const string User = "user";
        public const string Faculties = "faculties";
    }

    public static class UserDefault
    {
        public static User returnDefaultUser() {
            User user = new User
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "default@email.default",
                IsDeleted = false,
                UserName = "default",
                NormalizedUserName = "DEFAULT",
                NormalizedEmail = "DEFAULT@EMAIL.DEFAULT",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            return user;
        }
    }
}
