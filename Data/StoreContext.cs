using BackendStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendStoreAPI.Data;

public class StoreContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }

    public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
}