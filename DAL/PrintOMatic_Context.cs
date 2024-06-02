using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class PrintOMatic_Context : DbContext
    { 
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        public PrintOMatic_Context(DbContextOptions<PrintOMatic_Context> options) : base(options)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
           builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=PrintOMatic");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many
            modelBuilder.Entity<User>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.Users)
                .UsingEntity<UserGroup>();
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Account && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentDateTime = DateTime.Now;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Account)entity.Entity).CreatedAt = currentDateTime;
                }

                ((Account)entity.Entity).UpdatedAt = currentDateTime;
            }
        }
    }


}