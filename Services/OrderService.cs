using BackendStoreAPI.DTOs;
using BackendStoreAPI.Models;
using BackendStoreAPI.Repositories;

namespace BackendStoreAPI.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var orders = _orderRepository.GetAll();
            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                IsClosed = o.IsClosed,
                Products = o.Products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            });
        }

        public OrderDTO GetOrderById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return null;
            }

            return new OrderDTO
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                IsClosed = order.IsClosed,
                Products = order.Products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };
        }

        public OrderDTO CreateOrder(OrderDTO orderDto)
        {
            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
                IsClosed = orderDto.IsClosed,
                Products = orderDto.Products.Select(p => new Product
                {
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };

            _orderRepository.Add(order);
            return orderDto;
        }

        public OrderDTO UpdateOrder(OrderDTO orderDto)
        {
            var order = _orderRepository.GetById(orderDto.Id);
            if (order == null)
            {
                return null;
            }

            order.IsClosed = orderDto.IsClosed;
            order.Products = orderDto.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = p.Quantity
            }).ToList();

            _orderRepository.Update(order);
            return orderDto;
        }

        public bool DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return false;
            }

            _orderRepository.Delete(order);
            return true;
        }

        public bool AddProductToOrder(int orderId, ProductDTO productDto)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null || order.IsClosed)
            {
                return false;
            }

            order.Products.Add(new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            });

            _orderRepository.Update(order);
            return true;
        }

        public bool RemoveProductFromOrder(int orderId, int productId)
        {
            var order = _orderRepository.GetById(orderId);
            if (order == null || order.IsClosed)
            {
                return false;
            }

            var product = order.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return false;
            }

            order.Products.Remove(product);
            _orderRepository.Update(order);
            return true;
        }
}