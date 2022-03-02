
using Microsoft.EntityFrameworkCore;
using SehirRehberiAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SehirRehberiAPI.Data
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MSI\SQLEXPRESS;Database=SehirRehberi;Trusted_Connection=true");
        }
        public DbSet<Value> Values { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<User> Users { get; set; }




    }
}
