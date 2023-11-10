using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain;
using DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Azure.Storage.Blobs;
using System.Reflection.Metadata;

namespace Webstore.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DAL.Context _context;

        public ProductsController(DAL.Context context)
        {
            _context = context;
        }

        // Create a new product
        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(string productName, string productDescription, string productCategory, decimal productPrice)
        {
            Domain.Product product = new Domain.Product(productName, productDescription, productCategory, productPrice);
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        // Retrieve a list of all products
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            return Ok(_context.Products);
        }

        // Retrieve a specific product by its ID
        [HttpGet("{GetProductByID}")]
        public IActionResult GetProduct(int productID)
        {
            Domain.Product product = _context.Products.Find(productID);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // Update a product's information
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(_context.Products);
        }

        // Delete a product by its ID
        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int productID)
        {
            Domain.Product product = _context.Products.Find(productID);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }


        // Add a new product image
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> AddProductImage(int productID, IFormFile imageFile)
        {
            Domain.Product product = _context.Products.Find(productID);
            if (product == null)
            {
                return NotFound();
            }
            // Add image to the Blob Storage
            var blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=blobstorageonlineshop;AccountKey=ktvCw628uFAkJOeqvtJTV79sf4HR+p0lcI9N8VaVhnHphDsfag4FKTNT2Bh4ltuIcau1TOVne95A+AStxMRdtg==;EndpointSuffix=core.windows.net";
            var blobStorageContainerName = "productimages";

            // Create a unique ID for the image using GUID and the image filename 
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            var container = new BlobContainerClient(blobStorageConnectionString, blobStorageContainerName);
            var blob = container.GetBlobClient(uniqueFileName);

            // Upload the image from the IFormFile provided as a parameter
            using (var stream = imageFile.OpenReadStream())
            {
                await blob.UploadAsync(stream);
            }

            // Get and append imageURL to the list of product images
            string imageURL = blob.Uri.ToString();
            product.ProductImages.Add(imageURL);

            _context.SaveChanges();
            return Ok("Image upload complete!");
        }

        // Retrieve a list of all images for a specific product
        [HttpGet("GetProductImages")]
        public IActionResult GetProductImages(int productID)
        {
            Domain.Product product = _context.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.ProductID == productID);
            if (product == null)
            {
                return NotFound();
            }

            // Check if ProductImages is not null
            if (product.ProductImages == null)
            {
                // if ProductImages is null or not loaded, return an empty list
                return Ok(new List<string>());
            }

            return Ok(product.ProductImages);
        }

        // Retrieve a list of all reviews for a specific product
        [HttpGet("GetProductReviews")]
        public IActionResult GetProductReviews(int productID)
        {
            Domain.Product product = _context.Products.Include(p => p.Reviews).FirstOrDefault(p => p.ProductID == productID);
            if (product == null)
            {
                return NotFound();
            }

            // Check if ProductImages is not null
            if (product.Reviews == null)
            {
                // if ProductImages is null or not loaded, return an empty list
                return Ok(new List<string>());
            }

            return Ok(product.Reviews);
        }












        // [HttpPost]
        // public async Task<ActionResult<Product>> PostProduct(Product product)
        // {
        // _context.Products.Add(product);
        // await _context.SaveChangesAsync();

        // return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        //}
    }
}
