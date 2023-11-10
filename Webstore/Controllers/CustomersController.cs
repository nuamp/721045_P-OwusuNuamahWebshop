using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain;
using DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Webstore.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DAL.Context _context;

        public CustomersController(DAL.Context context)
        {
            _context = context;
        }

        // Create a new customer
        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer(string firstname, string lastname, string email, string username, string pwd)

        {
            Domain.Customer customer = new Domain.Customer(firstname, lastname, email, username, pwd);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        // Retrieve a list of all customers
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            return Ok(_context.Customers);
        }

        // Retrieve a specific customer by their ID
        [HttpGet("{GetCustomerByID}")]
        public IActionResult GetCustomer(int customerID)
        {
            Domain.Customer customer = _context.Customers.Find(customerID);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // Update a customer's information
        [HttpPut("UpdateCustomer")]
        public IActionResult UpdateCustomer(Customer customer)
        {
            _context.Customers.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(_context.Customers);
        }

        // Delete a customer by their ID
        [HttpDelete("DeleteCustomer")]
        public IActionResult DeleteCustomer(int customerID)
        {
            Domain.Customer customer = _context.Customers.Find(customerID);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok(customer);
        }

        // To add: get customer orders
        // Retrieve a list of all orders for a specific customer
        [HttpGet("GetCustomerOrders")]
        public IActionResult GetCustomerOrders(int customerID)
        {
            Domain.Customer customer = _context.Customers.Include(c => c.Orders).FirstOrDefault(c => c.CustomerID == customerID);
            if (customer == null)
            {
                return NotFound();
            }

            // Check if Orders is not null
            if (customer.Orders == null)
            {
                // if Orders is null or not loaded, return an empty list
                return Ok(new List<string>());
            }

            return Ok(customer.Orders);
        }

    }
}
