using BackendStoreAPI.Data;
using BackendStoreAPI.DTOs;
using BackendStoreAPI.Repositories;
using BackendStoreAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BackendStoreAPI.Tests;

public class OrderServiceTests
{
    private readonly OrderService _orderService;
        private readonly StoreContext _context;

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new StoreContext(options);
            var orderRepository = new OrderRepository(_context);
            _orderService = new OrderService(orderRepository);
        }

        [Fact]
        public void CreateOrder_ShouldReturnNewOrder()
        {
            var orderDto = new OrderDTO
            {
                IsClosed = false,
                Products = new List<ProductDTO>
                {
                    new ProductDTO { Name = "Product 1", Description = "Description 1", Price = 10.0m, Quantity = 2 },
                    new ProductDTO { Name = "Product 2", Description = "Description 2", Price = 20.0m, Quantity = 1 }
                }
            };

            // Act
            var createdOrder = _orderService.CreateOrder(orderDto);

            // Assert
            Assert.NotNull(createdOrder);
            Assert.False(createdOrder.IsClosed);
            Assert.Equal(2, createdOrder.Products.Count);
            Assert.Equal("Product 1", createdOrder.Products[0].Name);
            Assert.Equal(10.0m, createdOrder.Products[0].Price);
            Assert.Equal(2, createdOrder.Products[0].Quantity);
        }

        [Fact]
        public void GetOrderById_ShouldReturnOrder()
        {
            // Arrange
            var orderDto = new OrderDTO
            {
                IsClosed = false,
                Products = new List<ProductDTO>
                {
                    new ProductDTO { Name = "Product 1", Description = "Description 1", Price = 10.0m, Quantity = 2 }
                }
            };
            _orderService.CreateOrder(orderDto);

            // Act
            var retrievedOrder = _orderService.GetOrderById(1);

            // Assert
            Assert.NotNull(retrievedOrder);
            Assert.Equal(1, retrievedOrder.Id);
            Assert.False(retrievedOrder.IsClosed);
            Assert.Single(retrievedOrder.Products);
            Assert.Equal("Product 1", retrievedOrder.Products[0].Name);
        }

        [Fact]
        public void UpdateOrder_ShouldUpdateOrder()
        {
            // Arrange
            var orderDto = new OrderDTO
            {
                IsClosed = false,
                Products = new List<ProductDTO>
                {
                    new ProductDTO { Name = "Product 1", Description = "Description 1", Price = 10.0m, Quantity = 2 }
                }
            };
            _orderService.CreateOrder(orderDto);

            var updatedOrderDto = new OrderDTO
            {
                Id = 1,
                IsClosed = true,
                Products = new List<ProductDTO>
                {
                    new ProductDTO { Name = "Updated Product", Description = "Updated Description", Price = 15.0m, Quantity = 3 }
                }
            };

            // Act
            var updatedOrder = _orderService.UpdateOrder(updatedOrderDto);

            // Assert
            Assert.NotNull(updatedOrder);
            Assert.True(updatedOrder.IsClosed);
            Assert.Single(updatedOrder.Products);
            Assert.Equal("Updated Product", updatedOrder.Products[0].Name);
            Assert.Equal(15.0m, updatedOrder.Products[0].Price);
            Assert.Equal(3, updatedOrder.Products[0].Quantity);
        }

        [Fact]
        public void DeleteOrder_ShouldRemoveOrder()
        {
            // Arrange
            var orderDto = new OrderDTO
            {
                IsClosed = false,
                Products = new List<ProductDTO>
                {
                    new ProductDTO { Name = "Product 1", Description = "Description 1", Price = 10.0m, Quantity = 2 }
                }
            };
            _orderService.CreateOrder(orderDto);

            // Act
            var result = _orderService.DeleteOrder(1);

            // Assert
            Assert.True(result);
            var deletedOrder = _orderService.GetOrderById(1);
            Assert.Null(deletedOrder);
        }

        [Fact]
        public void AddProductToOrder_ShouldAddProduct()
        {
            // Arrange
            var orderDto = new OrderDTO
            {
                IsClosed = false,
                Products = new List<ProductDTO>()
            };
            _orderService.CreateOrder(orderDto);

            var productDto = new ProductDTO
            {
                Name = "New Product",
                Description = "New Description",
                Price = 25.0m,
                Quantity = 1
            };

            // Act
            var result = _orderService.AddProductToOrder(1, productDto);

            // Assert
            Assert.True(result);
            var updatedOrder = _orderService.GetOrderById(1);
            Assert.Single(updatedOrder.Products);
            Assert.Equal("New Product", updatedOrder.Products[0].Name);
        }

        [Fact]
        public void RemoveProductFromOrder_ShouldRemoveProduct()
        {
            // Arrange
            var orderDto = new OrderDTO
            {
                IsClosed = false,
                Products = new List<ProductDTO>
                {
                    new ProductDTO { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.0m, Quantity = 2 }
                }
            };
            _orderService.CreateOrder(orderDto);

            // Act
            var result = _orderService.RemoveProductFromOrder(1, 1);

            // Assert
            Assert.True(result);
            var updatedOrder = _orderService.GetOrderById(1);
            Assert.Empty(updatedOrder.Products);
        }
}