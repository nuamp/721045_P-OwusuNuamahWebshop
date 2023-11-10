using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain;
using DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Webstore.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DAL.Context _context;

        public OrdersController(DAL.Context context)
        {
            _context = context;
        }

        // Create a new order
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(int customerID, DateTime orderdate, DateTime shippingDate, string orderStatus, IEnumerable<Product> products)

        {
            Domain.Order order = new Domain.Order(customerID, orderdate, shippingDate, orderStatus, products);

            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(order);
        }


        // Retrieve a specific customer by their ID
        [HttpGet("{GetOrderByID}")]
        public IActionResult GetCustomer(System.Guid orderID)
        {
            Domain.Order order = _context.Orders.Find(orderID);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // Update an order
        [HttpPut("UpdateOrder")]
        public IActionResult UpdateOrder(Order order)
        {
            _context.Orders.Entry(order).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(_context.Orders);
        }

        // Cancelling an order by their ID
        [HttpDelete("CancelOrder")]
        public IActionResult CancelOrder(System.Guid orderID)
        {
            Domain.Order order = _context.Orders.Find(orderID);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok(order);
        }

        // Retrieve a list of all products in a specific order
        [HttpGet("GetOrderProducts")]
        public IActionResult GetOrderProducts(System.Guid orderID)
        {
            Domain.Order order = _context.Orders.Include(o => o.Products).FirstOrDefault(o => o.OrderID == orderID);
            if (order == null)
            {
                return NotFound();
            }

            // Check if ProductImages is not null
            if (order.Products == null)
            {
                // if ProductImages is null or not loaded, return an empty list
                return Ok(new List<string>());
            }

            return Ok(order.Products);
        }
    }
}
