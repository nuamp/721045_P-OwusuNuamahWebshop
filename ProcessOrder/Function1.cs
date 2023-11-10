using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace ProcessOrder
{
    public class ProcessOrder
    {
        //private readonly ILogger _logger;

        //public ProcessOrder(ILoggerFactory loggerFactory)
        //{
        //    _logger = loggerFactory.CreateLogger<ProcessOrder>();
        //}

        [Function("ProcessOrder")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "processorder")] HttpRequestData req, ILogger logger)
        {
            DAL.Context OrderContext = new DAL.Context();


            try 
            {
                // Parse and validate the order data returned from the HTTP request.
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var order = JsonSerializer.Deserialize<Domain.Order>(requestBody);

                if (order == null || order.OrderStatus != "Processing")
                {
                    return new BadRequestObjectResult("Invalid order data.");

                }

                logger.LogInformation($"Processing order request for order ID {order.OrderID} received.");

                // Update order status
                order.OrderStatus = "Processed";

                // Simulating a shipping date
                order.ShippingDate = DateTime.Now;
                order.OrderStatus = "Shipped";

                // Adding the order in the Db context
                OrderContext.Orders.Add(order);
            }

            catch (Exception ex) 
            {
                // In case of a generic error during the order processing the corresponding statu code should be returned
                logger.LogError($"Order processing error: {ex.Message}");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);

            }

            return new OkResult();
        }
    }
}
