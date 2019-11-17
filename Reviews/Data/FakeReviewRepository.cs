using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data
{
    public class FakeReviewRepository : IReviewRepository
    {
        private IEnumerable<Review> _reviews;

        public FakeReviewRepository(IEnumerable<Review> reviews)
        {
            _reviews = reviews;
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await Task.FromResult(_reviews.ToList());
        }

        public async Task<Review> GetReview(int id)
        {
            return await Task.FromResult(_reviews.FirstOrDefault(r => r.Id == id));
        }

        public void InsertReview(Review review)
        {
            _reviews.Append(review);
        }

        public void DeleteReview(int id)
        {
            var target = _reviews.FirstOrDefault(r => r.Id == id);
            _reviews.ToList().Remove(target);
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void HideReview(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Review>> GetReviewsByProduct(int prodId)
        {
            return await Task.FromResult(_reviews.Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToList());
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }
    }
}
