using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        [Key]
        public System.Guid OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Orderdate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string OrderStatus { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public Order(int customerID, DateTime orderdate, DateTime shippingDate, string orderStatus, IEnumerable<Product> products)
        {
            this.OrderID = System.Guid.NewGuid();
            this.CustomerID = customerID;
            this.Orderdate = DateTime.Now;
            this.ShippingDate = shippingDate;
            this.OrderStatus = orderStatus;
            this.Products = products;
    }

    } 
}
