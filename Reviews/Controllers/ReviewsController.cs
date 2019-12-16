using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reviews.Data;
using Reviews.Data.Purchases;
using Reviews.Models;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models.ViewModels;

namespace Reviews.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewRepository _repository;
        private readonly IPurchaseRepository _purchaseRepo;
        private readonly ILogger<ReviewsApiController> _logger;

        public ReviewsController(IReviewRepository reviewRepository, IPurchaseRepository purchaseRepository, ILogger<ReviewsApiController> logger)
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
    }
}
