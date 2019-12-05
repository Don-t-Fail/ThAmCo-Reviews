using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAll();
        Task<Review> GetReview(int id);
        void InsertReview(Review review);
        void DeleteReview(int id);
        void UpdateReview(Review review);
        Task<List<Review>> GetReviewsByProduct(int prodId);
        Task Save();
    }
}
