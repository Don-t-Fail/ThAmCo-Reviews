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

        public void DeleteReview(int id)
        {
            throw new NotImplementedException();
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
