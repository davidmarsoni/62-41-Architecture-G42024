using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class PrintOMaticContextFactory : IDesignTimeDbContextFactory<PrintOMatic_Context>
    {
        public PrintOMatic_Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PrintOMatic_Context>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PrintOMatic;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new PrintOMatic_Context(optionsBuilder.Options);
        }
    }
}
