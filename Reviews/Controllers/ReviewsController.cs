using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reviews.Data;
using Reviews.Data.Purchases;
using Reviews.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reviews.Models.ViewModels;
using Reviews.Services;

namespace Reviews.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly ILogger<ReviewsController> _logger;

        public ReviewsController(IReviewRepository reviewRepository, IPurchaseRepository purchaseRepository, ILogger<ReviewsController> logger)
        {
            _repository = reviewRepository;
            _logger = logger;
            _purchaseRepo = purchaseRepository;
        }

        // GET: Reviews/Create/5
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null && id > 0)
            {
                var purchase = await _purchaseRepo.GetPurchase(id.Value);
                if (purchase != null)
                {
                    ViewData["Purchase"] = purchase;
                    return View();
                }
            }
            return BadRequest("Purchase ID is null or invalid");
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Review> Create([Bind("PurchaseId,IsVisible,Rating,Content")] Review review)
        {
            if (ModelState.IsValid)
            {
                // TODO - Check if review already exists
                _repository.InsertReview(review);
                await _repository.Save();
                return await _repository.GetReview(review.Id);
            }
            return review;
        }

        // GET: Reviews/IndexAccount/5
        public async Task<IActionResult> IndexAccount(int? id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _repository.GetReviewsByAccount(id.Value);
            var revData = reviews.Select
            (
                r => new ReviewAccountViewModel
                {
                    Id = r.Id,
                    Content = r.Content,
                    IsVisible = r.IsVisible,
                    PurchaseId = r.PurchaseId,
                    Purchase = r.Purchase,
                    AccountId = r.Purchase.AccountId,
                    ProductId = r.Purchase.ProductId,
                    Rating = r.Rating
                }
            );
            return View(revData.ToList());
        }

        private bool ReviewExists(int id)
        {
            return _repository.GetAll().Result.Any(e => e.Id == id);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest();
            }

            var review = await _repository.GetReview(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null || id < 0)
            {
                return BadRequest();
            }

            _repository.DeleteReview(id);
            await _repository.Save();
            return RedirectToAction(nameof(IndexAccount));
        }

        // GET: api/Reviews/ProdAvg?id=
        [HttpGet("productaverage")]
        public async Task<ActionResult<double>> ProductAverage(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            double avg = 0;
            var purchases = await _repository.GetReviewsByProduct(id);

            if (purchases.Any())
            {
                foreach (var item in purchases)
                {
                    avg += item.Rating;
                }
                return Ok(Math.Round((avg / purchases.Count()) * 2, MidpointRounding.AwayFromZero) / 2);
            }

            return NotFound();
        }

        // GET: api/Reviews?ProdId={id}
        [HttpGet]
        public async Task<ActionResult<List<Review>>> GetReviewProduct(int prodId)
        {
            if (prodId < 0)
            {
                return BadRequest();
            }

            var reviews = await _repository.GetReviewsByProduct(prodId);
            if (reviews.Any())
            {
                return Ok(reviews);
            }

            return NotFound();
        }

        // GET: Purchases/Details/5
        public async Task<ActionResult<ReviewDetailsDto>> Details(int? id)
        {
            if (id == null || id < 0)
            {
                return BadRequest();
            }

            var review = await _repository.GetReview(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(new ReviewDetailsDto
            {
                Id = review.Id,
                Content = review.Content,
                PurchaseId = review.PurchaseId,
                IsVisible = review.IsVisible,
                Rating = review.Rating
            });
        }
    }
}
