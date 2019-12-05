using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data
{
    public class FakeReviewRepository : IReviewRepository
    {
        private List<Review> _reviews;

        public FakeReviewRepository(List<Review> reviews)
        {
            _reviews = reviews;
        }

        public async Task<List<Review>> GetAll()
        {
            return await Task.FromResult(_reviews.ToList());
        }

        public async Task<Review> GetReview(int id)
        {
            return await Task.FromResult(_reviews.FirstOrDefault(r => r.Id == id));
        }

        public void InsertReview(Review review)
        {
            _reviews.Add(review);
        }

        public void DeleteReview(int id)
        {
            _reviews.Where(r => r.Id == id).ToList().ForEach(r => r.IsVisible = false);
        }

        public void UpdateReview(Review review)
        {
            throw new NotImplementedException();
        }

        public void HideReview(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Review>> GetReviewsByProduct(int prodId)
        {
            return await Task.FromResult(_reviews.Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToList());
        }

        public Task Save()
        {
            return null;
        }
    }
}
