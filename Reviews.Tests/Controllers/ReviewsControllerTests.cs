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
using Microsoft.Extensions.Logging;
using Reviews.Data.Purchases;
using Reviews.Services;

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

            public static List<Purchase> Purchases() => new List<Purchase>
            {
                new Purchase { Id = 1, AccountId = 1, ProductId = 1}
            };
        }

        [TestMethod]
        public async Task GetReviewDetailsTest_Success()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var id = 1;

            //Act
            var result = await controller.Details(id);

            //Assert
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as ReviewDetailsDto;
            Assert.IsNotNull(retResult);

            Assert.AreEqual(TestData.Reviews()[id - 1].Id, retResult.Id);
            Assert.AreEqual(TestData.Reviews()[id - 1].PurchaseId, retResult.PurchaseId);
            Assert.AreEqual(TestData.Reviews()[id - 1].IsVisible, retResult.IsVisible);
            Assert.AreEqual(TestData.Reviews()[id - 1].Rating, retResult.Rating);
            Assert.AreEqual(TestData.Reviews()[id - 1].Content, retResult.Content);
        }

        [TestMethod]
        public async Task GetReviewDetails_OutOfBounds()
        {
            // Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var id = -9;

            // Act
            var result = await controller.Details(id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GetReviewDetailsTestAsync_Hidden()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var id = 2;

            //Act
            var result = await controller.Details(id);

            //Assert
            Assert.IsInstanceOfType(result.Result,typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetReviewTestAsync_NotExists()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var id = 42;

            //Act
            var result = await controller.Details(id);

            //Assert
            //Non-Existing review not found
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ProductAverageTest()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = 75;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var res = objResult.Value;

            Assert.AreEqual(Convert.ToDouble(4), res);
        }

        [TestMethod]
        public async Task ProductAverageTest_Hidden()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var res = objResult.Value;

            Assert.AreEqual(2.5, res);
        }

        [TestMethod]
        public async Task ProductAverage_OutOfBounds()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = -9;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task ProductAverage_NotExists()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
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
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = 3;

            //Act
            var result = await controller.ProductAverage(prodId);
            var expected = await controller.GetReviewProduct(prodId);

            //Assert
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value;
            Assert.IsNotNull(retResult);
            double val = TestData.Reviews().FirstOrDefault(r => r.Purchase.Id == prodId).Rating;

            Assert.AreEqual(val, retResult);
        }

        [TestMethod]
        public async Task GetReviewProductTest_Success()
        {
            // Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = 1;

            // Act
            var result = await controller.GetReviewProduct(prodId);

            // Assert
            Assert.IsNotNull(result);
            var objResult = result.Result as OkObjectResult;
            Assert.IsNotNull(objResult);
            var retResult = objResult.Value as List<Review>;
            Assert.IsNotNull(retResult);
            //foreach (Review review in retResult)
            //{
            //    Assert.AreEqual(await repo.GetReview(review.Id), review);
            //}
        }

        [TestMethod]
        public async Task GetReviewProductTest_OutOfBounds()
        {
            // Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = -99;

            // Act
            var result = await controller.GetReviewProduct(prodId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task GetReviewProductTest_Hidden()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = 2;

            //Act
            var result = await controller.GetReviewProduct(prodId);
            var expected = TestData.Reviews().Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToList();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetReviewProductTest_NotExists()
        {
            //Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var prodId = 370;

            //Act
            var result = await controller.GetReviewProduct(prodId);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteReviewTest()
        {
            // Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var reviewId = 1;

            // Act
            var result = await controller.DeleteConfirmed(reviewId);
            var review = await controller.Details(reviewId);

            // Assert
            Assert.IsInstanceOfType(review.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteReview_OutOfBounds()
        {
            // Arrange
            var repo = new FakeReviewRepository(TestData.Reviews());
            var purchaseRepo = new FakePurchaseService(TestData.Purchases());
            var controller = new ReviewsController(repo, purchaseRepo, new NullLogger<ReviewsController>());
            var reviewId = -5;

            // Act
            var result = await controller.DeleteConfirmed(reviewId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task CreateReview_Success()
        {

        }
    }
}