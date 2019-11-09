using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reviews.Models;

namespace Reviews.Data
{
    public interface IReviewRepository
    {
        Review GetReview(int id);
        void InsertReview(Review review);
        void DeleteReview(int id);
        void UpdateReview(Review review);
        void HideReview(int id);
        IEnumerable<Review> GetReviewsByProduct(int prodId);
    }
}
