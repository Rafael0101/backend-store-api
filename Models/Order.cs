namespace BackendStoreAPI.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsClosed { get; set; } = false;
    public List<Product> Products { get; set; } = new List<Product>();
}