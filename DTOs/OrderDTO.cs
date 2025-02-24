namespace BackendStoreAPI.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsClosed { get; set; }
    public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
}