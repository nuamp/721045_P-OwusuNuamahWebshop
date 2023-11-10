using Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Image key is auto-incremented
    public int ImageID { get; set; }
    public string ImageURL { get; set; }
    public int ProductID { get; set; } // Foreign key linking to the Product

    //[ForeignKey("ProductID")]
    //public Product Product { get; set; }
}
 // NOT NEEDED!!!