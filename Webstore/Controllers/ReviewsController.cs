using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain;
using DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Webstore.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly DAL.Context _context;

        public ReviewsController(DAL.Context context)
        {
            _context = context;
        }

        // Create a new review
        [HttpPost("CreateReview")]
        public IActionResult CreateReview(int productID, string body, decimal rating)

        {
            Domain.Review review = new Domain.Review(productID, body, rating);
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Ok(review);
        }

        // Retrieve a list of all reviews
        [HttpGet("GetAllReviews")]
        public IActionResult GetAllReviews()
        {
            return Ok(_context.Reviews);
        }

        // Retrieve a specific review by ID
        [HttpGet("{GetReviewByID}")]
        public IActionResult GetReviewByID(System.Guid reviewID)
        {
            Domain.Review review = _context.Reviews.Find(reviewID);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // Delete a review by ID
        [HttpDelete("DeleteReview")]
        public IActionResult DeleteReview(System.Guid reviewID)
        {
            Domain.Review review = _context.Reviews.Find(reviewID);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return Ok(review);
        }

    }
}
