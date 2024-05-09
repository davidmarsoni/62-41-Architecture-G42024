using DAL;
using DAL.Classes;
using DAL.Models;
using System.Reflection.Metadata;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new PrintOMatic_Context();
            var created = context.Database.EnsureCreated();

            if (created)
            {
                System.Console.WriteLine("Database created");
            }
            else
            {
                System.Console.WriteLine("Database already exists");
                // drop the database
                context.Database.EnsureDeleted();
                System.Console.WriteLine("Database deleted");
                // create the database
                context.Database.EnsureCreated();
                System.Console.WriteLine("Database created");
            }


            //try to add a user
            var user = new User
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "Robert@gmail.com",
                Username = "Robert",
                Password = "password",
                Salt = "salt",
                CreatedAt = System.DateTime.Now,
                UpdatedAt = System.DateTime.Now,
                IsDeleted = false
            };

            context.Users.Add(user);

            //try to add a group
            var group = new Group
            {
                Name = "Admin",
                CreatedAt = System.DateTime.Now,
                UpdatedAt = System.DateTime.Now,
                IsDeleted = false
            };

            context.Groups.Add(group);

            //try to add a user_group
            var user_group = new User_Group
            {
                User = user,
                Group = group
            };

            context.User_Groups.Add(user_group);

            //try add an account
            var account = new Account
            {
                User = user,
                Balance = 0,
                CreatedAt = System.DateTime.Now,
                UpdatedAt = System.DateTime.Now
            };

            context.Accounts.Add(account);

            //try to add a conversion
            var conversion = new Conversion
            {
                Name = "1 BW A4",
                Value = (decimal) 0.42
            };

            context.Conversions.Add(conversion);

            context.SaveChanges();

            //try to add a transaction
            var transaction = new TransactionHistory
            {
                Account = account,
                Amount = 100,
                DateTime = System.DateTime.Now,
                TransactionType = TransactionType.AddCredit,
                Src = Src.PayOnline,
                ConversionName = conversion.Name,
                ConversionValue = conversion.Value
            };

            context.TransactionHistory.Add(transaction);

            // modify the account balance
            account.Deposit(transaction.Amount);

            // save the changes

            context.SaveChanges();
        }
    }
}
