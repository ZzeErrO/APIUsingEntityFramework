using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        { 
        
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = 1,
                FirstName = "Uncle",
                LastName = "Bob",
                Gender = 'M',
                Email = "uncle.bob@gmail.com",
                Password = "abrakadabra"

            }, new User
            {
                UserId = 2,
                FirstName = "Jan",
                LastName = "Kirsten",
                Gender = 'F',
                Email = "jan.kirsten@gmail.com",
                Password = "++LoveisLife++"
            });
        }
    }
}
