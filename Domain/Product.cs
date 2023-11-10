using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Product key is auto-incremented
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public decimal ProductPrice { get; set;}
        public int ProductQuantity { get; set;}
        public ICollection<string> ProductImages { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        public Product(string productName, string productDescription, string productCategory, decimal productPrice) 
        {
            this.ProductName = productName;
            this.ProductDescription = productDescription;
            this.ProductCategory = productCategory;
            this.ProductPrice = productPrice;

        }

    }

}
