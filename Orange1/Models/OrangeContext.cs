using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orange1.Models;

public class OrangeContext : IdentityDbContext<ApplicationUser>
{
    public OrangeContext(DbContextOptions<OrangeContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductInCard> productInCards { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Tastimonial> Tastimonials { get; set; }
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
