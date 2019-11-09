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

        public Review GetReview(int id)
        {
            return _context.Review.Find(id);
        }

        public IEnumerable<Review> GetReviewsByProduct(int prodId)
        {
            return _context.Review.Where(p => p.Purchase.ProductId == prodId).ToList();
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
