using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Reviews.Data;
using Reviews.Data.Purchases;
using Reviews.Models;

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
            //ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "Id", review.PurchaseId);
            return review;
        }

        //// GET: Reviews/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var review = await _context.Review.FindAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "Id", review.PurchaseId);
        //    return View(review);
        //}

        //// POST: Reviews/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,PurchaseId,IsVisible,Rating,Content")] Review review)
        //{
        //    if (id != review.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(review);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ReviewExists(review.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PurchaseId"] = new SelectList(_context.Purchase, "Id", "Id", review.PurchaseId);
        //    return View(review);
        //}

        //// GET: Reviews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var review = await _context.Review
        //        .Include(r => r.Purchase)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(review);
        //}

        //// POST: Reviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var review = await _context.Review.FindAsync(id);
        //    _context.Review.Remove(review);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ReviewExists(int id)
        {
            return _repository.GetAll().Result.Any(e => e.Id == id);
        }
    }
}
