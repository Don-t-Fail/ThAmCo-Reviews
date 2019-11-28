using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Reviews.Models;

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
            // TODO - Possible Improvement - Passing review object directly from controller
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

        public async void HideReview(int id)
        {
            // TODO - Implement checking account here for permissions
            var review = await _context.Review.Where(r => r.Id == id).FirstOrDefaultAsync();
            if (review != null)
            {
                review.IsVisible = false;
                await Save();
            }

        }

        public void InsertReview(Review review)
        {
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
    }
}
