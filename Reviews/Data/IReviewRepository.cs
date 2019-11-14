using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAll();
        Task<Review> GetReview(int id);
        void InsertReview(Review review);
        void DeleteReview(int id);
        void UpdateReview(Review review);
        void HideReview(int id);
        Task<IEnumerable<Review>> GetReviewsByProduct(int prodId);
        Task<double> GetProductAverage(int prodId);
        Task Save();
    }
}
