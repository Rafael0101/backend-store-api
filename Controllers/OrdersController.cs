using BackendStoreAPI.DTOs;
using BackendStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendStoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }
        
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> GetOrder(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        
        [HttpPost]
        public ActionResult<OrderDTO> CreateOrder([FromBody] OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdOrder = _orderService.CreateOrder(orderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }
    
        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] OrderDTO orderDto)
        {
            if (id != orderDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOrder = _orderService.UpdateOrder(orderDto);
            if (updatedOrder == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var result = _orderService.DeleteOrder(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost("{orderId}/products")]
        public ActionResult AddProductToOrder(int orderId, [FromBody] ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _orderService.AddProductToOrder(orderId, productDto);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
        
        [HttpDelete("{orderId}/products/{productId}")]
        public ActionResult RemoveProductFromOrder(int orderId, int productId)
        {
            var result = _orderService.RemoveProductFromOrder(orderId, productId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

}