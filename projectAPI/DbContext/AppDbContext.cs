using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projectAPI.Core.Domain.Entities;
using projectAPI.Core.Domain.IdentityEntities;


namespace projectAPI
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
       
       public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<ShopingCart> ShopingCart { get; set; }

    }
}
