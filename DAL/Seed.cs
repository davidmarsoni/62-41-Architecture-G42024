using DAL.Classes;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Net;

namespace DAL
{
    public class Seed
    {
        public static void SeedData(PrintOMatic_Context context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var user = new User
            {
                FirstName = "BBB",
                LastName = "BBB",
                Email = "BBB@a.com",
                IsDeleted = false
            };

            context.Users.Add(user);

            var group = new Group
            {
                Name = "Admin",
                IsDeleted = false
            };

            context.Groups.Add(group);

            var user_group = new User_Group
            {
                User = user,
                Group = group
            };

            context.User_Groups.Add(user_group);

            var account = new Account
            {
                User = user,
                Balance = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            context.Accounts.Add(account);

            var conversions = new List<Conversion>();

            for (int i = 1; i <= 10; i++)
            {
                var conversion = new Conversion
                {
                    Name = $"Conversion {i}",
                    Value = (decimal)(0.42 * i)
                };

                conversions.Add(conversion);
            }

            context.Conversions.AddRange(conversions);

            context.SaveChanges();

            var transaction = new TransactionHistory
            {
                Account = account,
                Amount = 100,
                DateTime = DateTime.Now,
                TransactionType = TransactionType.AddCredit,
                Src = Src.PayOnline,
                ConversionName = conversions.Last().Name,
                ConversionValue = conversions.Last().Value
            };

            context.TransactionHistory.Add(transaction);

            account.Deposit(transaction.Amount);

            context.SaveChanges();
        }

        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider, PrintOMatic_Context context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);

            string[] roles = new string[] {
                    UserRoles.Admin,
                    UserRoles.User
                };

            if (!context.Roles.Any(r => r.Name == UserRoles.Admin))
            {
                await roleStore.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!context.Roles.Any(r => r.Name == UserRoles.User))
            {
                await roleStore.CreateAsync(new IdentityRole(UserRoles.User));
            }

            var user = new User
            {
                FirstName = "XXXX",
                LastName = "XXXX",
                Email = "xxxx@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<User>(context);
                var result = await userStore.CreateAsync(user);
                if (result.Succeeded)
                {
                    await AssignRoles(serviceProvider, user.Email, roles);
                }
            }
            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<User> _userManager = services.GetService<UserManager<User>>();
            User? user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.AddToRolesAsync(user, roles);
                return result;
            }

            return IdentityResult.Failed();
        }
    }
}
