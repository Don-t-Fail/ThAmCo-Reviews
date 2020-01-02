using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reviews.Controllers;
using Reviews.Data;
using Reviews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reviews.Tests.Controllers
{
    [TestClass]
    public class ReviewsControllerTests
    {
        private static class TestData
        {
            public static List<Review> Reviews() => new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 2, Purchase = new Purchase
                {
                    AccountId = 1, Id = 1, ProductId = 1
                }},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = false, PurchaseId = 4, Rating = 5, Purchase = new Purchase
                {
                    AccountId = 2, Id = 4, ProductId = 1
                }},
                new Review { Id = 4, Content = "Review No. 4", IsVisible = true, PurchaseId = 2, Rating = 3, Purchase = new Purchase
                {
                    AccountId = 1, Id = 2, ProductId = 1
                }},
                new Review { Id = 5, Content = "Review No. 5", IsVisible = true, PurchaseId = 3, Rating = 2, Purchase = new Purchase
                {
                    AccountId = 2, Id = 3, ProductId = 1
                }},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 13, Rating = 5, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 75, Id =  13
                }},
                new Review { Id = 6, Content = "Review No. 6", IsVisible = true, PurchaseId = 14, Rating = 5, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 75, Id =  14
                }},
                new Review { Id = 7, Content = "Review No. 7", IsVisible = true, PurchaseId = 15, Rating = 2, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 75, Id =  15
                }},
                new Review { Id = 8, Content = "Review No. 8", IsVisible = true, PurchaseId = 16, Rating = 2, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 3, Id =  16
                }}
            };
        }

        [TestMethod]
        public async Task GetReviewTestAsync()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var id = 2;

            //Act
            var result = await controller.GetReview(id);

            //Assert
            Assert.AreEqual(id, result.Value.Id);
            Assert.AreEqual(TestData.Reviews()[id - 1].Id, result.Value.Id);
            Assert.AreEqual(TestData.Reviews()[id - 1].Content, result.Value.Content);
            Assert.AreEqual(TestData.Reviews()[id - 1].IsVisible, result.Value.IsVisible);
            Assert.AreEqual(TestData.Reviews()[id - 1].PurchaseId, result.Value.PurchaseId);
            Assert.AreEqual(TestData.Reviews()[id - 1].Rating, result.Value.Rating);
        }

        [TestMethod]
        public async Task GetReviewTestAsync_Hidden()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var id = 2;

            //Act
            var result = await controller.GetReview(id);

            //Assert
            Assert.AreEqual(id, result.Value.Id);
            Assert.AreEqual(TestData.Reviews()[id - 1].Id, result.Value.Id);
            Assert.AreEqual(TestData.Reviews()[id - 1].Content, result.Value.Content);
            Assert.AreEqual(TestData.Reviews()[id - 1].IsVisible, result.Value.IsVisible);
            Assert.AreEqual(TestData.Reviews()[id - 1].PurchaseId, result.Value.PurchaseId);
            Assert.AreEqual(TestData.Reviews()[id - 1].Rating, result.Value.Rating);
        }

        [TestMethod]
        public async Task GetReviewTestAsync_NotExists()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var id = 42;

            //Act
            var result = await controller.GetReview(id);

            //Assert
            //Non-Existing review not found
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ProductAverageTest()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 75;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(4, result.Value);
        }

        [TestMethod]
        public async Task ProductAverageTest_Hidden()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(2.5, result.Value);
        }

        [TestMethod]
        public async Task ProductAverage_NotExists()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 7000;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ProductAverageTest_OneReview()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 3;

            //Act
            var result = await controller.ProductAverage(prodId);
            var expected = await controller.GetReviewProduct(prodId);
            var review = expected.FirstOrDefault();

            //Assert
            Assert.AreEqual(review.Rating, result.Value);
        }

        [TestMethod]
        public async Task PutReviewTest()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = true, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var review = new Review { Id = 4, Content = "Review 4", IsVisible = true, PurchaseId = 2, Rating = 5 };

            //Act
            // TODO - Fix NullPointerException
            var result = await controller.PutReview(4, review);

            //Assert
            Assert.AreEqual(reviews.FirstOrDefault(r => r.Id == 4), review);
        }

        [TestMethod]
        public async Task PutReviewTest_NoIdMatch()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = true, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var review = new Review { Id = 4, Content = "Review 4", IsVisible = true, PurchaseId = 2, Rating = 5 };

            //Act
            var result = await controller.PutReview(5, review);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task PutReviewTest_ReviewExists()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = true, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var review = new Review { Id = 4, Content = "Review 4", IsVisible = true, PurchaseId = 2, Rating = 5 };

            //Act
            // TODO - Fix NullPointerException
            var result = await controller.PutReview(4, review);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task PostReviewTest_CreateSuccess()
        {
            var reviews = new List<Review>
            {
                new Review {Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var newReview = new Review
            { Id = 2, IsVisible = true, Content = "This is a new review", PurchaseId = 2, Rating = 3 };

            // Act
            await controller.PostReview(newReview);
            var expected = await controller.GetReview(2);

            //Assert
            Assert.AreEqual(expected.Value, reviews.Find(r => r.Id == newReview.Id));
        }

        [TestMethod]
        public async Task GetReviewProductTest()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 75;

            //Act
            var result = await controller.GetReviewProduct(prodId);
            var expected = TestData.Reviews().Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToList();

            //Assert
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public async Task GetReviewProductTest_Hidden()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 1;

            //Act
            var result = await controller.GetReviewProduct(prodId);
            var expected = TestData.Reviews().Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToList();

            //Assert
            Assert.IsTrue(result.SequenceEqual(expected));
        }

        [TestMethod]
        public async Task GetReviewProductTest_NotExists()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var prodId = 370;

            //Act
            var result = await controller.GetReviewProduct(prodId);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task DeleteReviewTest()
        {
            // Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var controller = new ReviewsApiController(repo, new NullLogger<ReviewsApiController>());
            var reviewId = 1;

            // Act
            await controller.DeleteReview(reviewId);
            var review = await controller.GetReview(reviewId);

            // Assert
            Assert.IsFalse(review.Value.IsVisible);
        }
    }
}