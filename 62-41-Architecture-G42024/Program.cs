using DAL;
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
            }


            //try to add a user
            var user = new User
            {
                Username = "johndoe",
                Password = "password",
                FirstName = "John",
                LastName = "Doe",
                Email = "a@a.ch",
            };

            context.Users.Add(user);
        }
    }
}
