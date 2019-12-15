using Reviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
