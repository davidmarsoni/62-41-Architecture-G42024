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


            //seed data

            //conversions
            modelBuilder.Entity<Conversion>().HasData(
                new Conversion { Id = 1, Name = "A4 Black", Value = 0.05m },
                new Conversion { Id = 2, Name = "A4 Color", Value = 0.15m },
                new Conversion { Id = 3, Name = "A3 Black", Value = 0.10m },
                new Conversion { Id = 4, Name = "A3 Color", Value = 0.30m }
            );

            //Groups
            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "604 Full Time French", Acronym = "604-FT-F", IsDeleted = false },
                new Group { Id = 2, Name = "604 Full Time Deutsch", Acronym = "604-FT-D", IsDeleted = false },
                new Group { Id = 3, Name = "604 Part Time French", Acronym = "604-PT-F", IsDeleted = false },
                new Group { Id = 4, Name = "604 Part Time Deutsch", Acronym = "604-PT-D", IsDeleted = false },
                new Group { Id = 5, Name = "604 All Students", Acronym = "604-ALL", IsDeleted = false }
            );

            //Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "david.marsoni",
                    LastName = "Marsoni",
                    FirstName = "David",
                    Email = "david.marsoni@students.hevs.ch",
                    Gender = "Mr",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                new User
                {
                    Id = 2,
                    Username = "mathias.pitteloud",
                    LastName = "Pitteloud",
                    FirstName = "Mathias",
                    Email = "mathias.pitteloud@students.hevs.ch",
                    Gender = "Mr",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                //Biselx Thomas
                new User
                {
                    Id = 3,
                    Username = "thomas.biselx",
                    LastName = "Biselx",
                    FirstName = "Thomas",
                    Email = "thomas.biselx@students.hevs.ch",
                    Gender = "Mr",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                //Araújo Jonathan
                new User
                {
                    Id = 4,
                    Username = "jonathan.araujo",
                    LastName = "Araújo",
                    FirstName = "Jonathan",
                    Email = "jonathan.araujo@students.hevs.ch",
                    Gender = "Mr",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                //Summermatter Johanna
                new User
                {
                    Id = 5,
                    Username = "johanna.summermatter",
                    LastName = "Summermatter",
                    FirstName = "Johanna",
                    Email = "johanna.summermatter@students.hevs.ch",
                    Gender = "Mss",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                //Sanderson Dylan
                new User
                {
                    Id = 6,
                    Username = "dylan.sanderson",
                    LastName = "Sanderson",
                    FirstName = "Dylan",
                    Email = "dylan.sanderson@students.hevs.ch",
                    Gender = "Mr",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                // Fernández Rodríguez Zanya
                new User
                {
                    Id = 7,
                    Username = "zanya.fernandezrodriguez",
                    LastName = "Fernández Rodríguez",
                    FirstName = "Zanya",
                    Email = "zanya.fernandezrodriguez@students.hevs.ch",
                    Gender = "Mss",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                },
                //Von Roten Johann
                new User
                {
                    Id = 8,
                    Username = "johann.vonroten",
                    LastName = "Von Roten",
                    FirstName = "Johann",
                    Email = "johann.vonroten@students.hevs.ch",
                    Gender = "Mr",
                    Address = "Rue de la Gare 12, 1950 Sion",
                    IsDeleted = false
                });

            //UserGroups
            modelBuilder.Entity<UserGroup>().HasData(
                new UserGroup { Id = 1, UserId = 1, GroupId = 1 },
                new UserGroup { Id = 2, UserId = 2, GroupId = 1 },
                new UserGroup { Id = 3, UserId = 3, GroupId = 1 },
                new UserGroup { Id = 4, UserId = 4, GroupId = 1 },
                new UserGroup { Id = 5, UserId = 5, GroupId = 2 },
                new UserGroup { Id = 6, UserId = 6, GroupId = 3 },
                new UserGroup { Id = 7, UserId = 7, GroupId = 1 },
                new UserGroup { Id = 8, UserId = 8, GroupId = 3 },
                new UserGroup { Id = 9, UserId = 1, GroupId = 5 },
                new UserGroup { Id = 10, UserId = 2, GroupId = 5 },
                new UserGroup { Id = 11, UserId = 3, GroupId = 5 },
                new UserGroup { Id = 12, UserId = 4, GroupId = 5 },
                new UserGroup { Id = 13, UserId = 5, GroupId = 5 },
                new UserGroup { Id = 14, UserId = 6, GroupId = 5 },
                new UserGroup { Id = 15, UserId = 7, GroupId = 5 },
                new UserGroup { Id = 16, UserId = 8, GroupId = 5 }
            );

            //Accounts
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, UserId = 1, Balance = 10 },
                new Account { Id = 2, UserId = 2, Balance = 10 },
                new Account { Id = 3, UserId = 3, Balance = 10 },
                new Account { Id = 4, UserId = 4, Balance = 10 },
                new Account { Id = 5, UserId = 5, Balance = 10 },
                new Account { Id = 6, UserId = 6, Balance = 10 },
                new Account { Id = 7, UserId = 7, Balance = 10 },
                new Account { Id = 8, UserId = 8, Balance = 10 }
                );


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