﻿using Microsoft.EntityFrameworkCore;
using Reviews.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDbContext _context;

        public ReviewRepository(ReviewDbContext context)
        {
            _context = context;
        }

        public async void DeleteReview(int id)
        {
            var review = await _context.Review.FirstOrDefaultAsync(r => r.Id == id);
            review.IsVisible = false;
            _context.Review.Update(review);
        }

        public async Task<List<Review>> GetAll()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Review.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Review>> GetReviewsByProduct(int prodId)
        {
            var reviews = await _context.Review.Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToListAsync();
            return reviews;
        }

        public async Task InsertReview(Review review)
        {
            review.PurchaseId = (await _context.Purchase.Where
                (p => p.PurchaseRef == review.PurchaseRef).FirstOrDefaultAsync()).Id;
            _context.Review.Add(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetReviewsByAccount(int accId)
        {
            var reviews = await _context.Review.Where(r => r.Purchase.AccountId == accId).Include(r => r.Purchase.Account).ToListAsync();
            return reviews;
        }
    }
}