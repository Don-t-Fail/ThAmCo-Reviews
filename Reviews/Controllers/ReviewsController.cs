using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reviews.Data;
using Reviews.Models;

namespace Reviews.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _repository;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewRepository reviewRepository, ILogger<ReviewsController> logger)
        {
            _repository = reviewRepository;
            _logger = logger;
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _repository.GetReview(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            review.Id = id;
            _repository.InsertReview(review);

            try
            {
                await _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            review.IsVisible = true;
            _repository.InsertReview(review);
            await _repository.Save();

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Review>> DeleteReview(int id)
        {
            var review = await _repository.GetReview(id);
            if (review == null)
            {
                return NotFound();
            }

            _repository.DeleteReview(id);
            await _repository.Save();

            return review;
        }        

        private bool ReviewExists(int id)
        {
            return _repository.GetAll().Result.Any(e => e.Id == id);
        }

        // GET: api/Reviews/ProdAvg?id=
        [HttpGet("productaverage")]
        public async Task<ActionResult<double>> ProductAverage(int id)
        {
            double avg = 0;
            var purchases = await _repository.GetReviewsByProduct(id);
            if (purchases.Any())
            {
                foreach (var item in purchases)
                {
                    avg += item.Rating;
                }
                return Math.Round((avg / purchases.Count()) * 2, MidpointRounding.AwayFromZero) / 2;
            }

            return NotFound();

        }

        /**/

        // GET: api/Reviews?ProdId={id}
        [HttpGet]
        public async Task<IEnumerable<Review>> GetReviewProduct(int prodId)
        {
            var reviews = await _repository.GetReviewsByProduct(prodId);
            if (reviews.Any())
            {
                return reviews;
            }

            return null;
        }
    }
}
