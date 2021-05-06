using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CommonLayer.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                UserId = 1,
                FirstName = "Uncle",
                LastName = "Bob",
                //Gender = 'M',
                Email = "uncle.bob@gmail.com",
                Password = "abrakadabra"

            }, new UserModel
            {
                UserId = 2,
                FirstName = "Jan",
                LastName = "Kirsten",
                //Gender = 'F',
                Email = "jan.kirsten@gmail.com",
                Password = "++LoveisLife++",

            });
        }
    }
}
