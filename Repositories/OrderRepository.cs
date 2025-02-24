using BackendStoreAPI.Data;
using BackendStoreAPI.Models;

namespace BackendStoreAPI.Repositories;

public class OrderRepository
{
    private readonly StoreContext _context;

    public OrderRepository(StoreContext context)
    {
        _context = context;
    }

    public IEnumerable<Order> GetAll()
    {
        return _context.Orders.ToList();
    }

    public Order GetById(int id)
    {
        return _context.Orders.FirstOrDefault(o => o.Id == id);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void Delete(Order order)
    {
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
    
}