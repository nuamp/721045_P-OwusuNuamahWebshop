using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Review
    {
        public System.Guid ReviewID { get; set; }
        public int ProductID { get; set; }
        public decimal ProductRating { get; set; }
        public string Body { get; set; }
        public DateTime ReviewDate { get; set; }
        public ICollection<string> media { get; set;}

        public Review(int productID, string body, decimal rating) 
        { 
            this.ReviewID = Guid.NewGuid();
            this.ProductID = productID;
            this.Body = body;
            this.ReviewDate = DateTime.Now;
            this.ProductRating = rating;
        }
    }
}
