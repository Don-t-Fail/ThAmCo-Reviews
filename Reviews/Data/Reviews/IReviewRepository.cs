﻿using Reviews.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reviews.Data
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAll();

        Task<Review> GetReview(int id);

        Task InsertReview(Review review);

        void DeleteReview(int id);

        void UpdateReview(Review review);

        Task<List<Review>> GetReviewsByProduct(int prodId);

        Task<List<Review>> GetReviewsByAccount(int accId);

        Task Save();
    }
}