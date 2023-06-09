using IceCreamProject.Models;
using Microsoft.EntityFrameworkCore;

namespace IceCreamProject.Data
{
    public class RegDbContext : DbContext
    {
        public RegDbContext(DbContextOptions<RegDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Users> User{get;set;}
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Books>Book { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
    }
}
