using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PrintOMatic_Context : DbContext
    { 
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<Group> Groups { get; set; } 
        public DbSet<TransactionHistory> TransactionHistory { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_Group> User_Groups { get; set; }

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
                .UsingEntity<User_Group>();
        }
    }
}