using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _context.Review.Remove(review);
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<Review> GetReview(int id)
        {
            return await _context.Review.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByProduct(int prodId)
        {
            return await _context.Review.Where(p => p.Purchase.ProductId == prodId).ToListAsync();
        }

        public async Task<double> GetProductAverage(int prodId)
        {
            var avg = 0;
            var purchases = await GetReviewsByProduct(prodId);
            if (purchases.Any())
            {
                foreach (var item in purchases)
                {
                    avg += item.Rating;
                }

                return (double)avg / (double)purchases.Count();
            }

            return -1;
        }

        public void HideReview(int id)
        {
            throw new NotImplementedException();
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
