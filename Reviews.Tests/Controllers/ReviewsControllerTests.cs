using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reviews.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reviews.Data;
using Reviews.Models;

namespace Reviews.Controllers.Tests
{
    [TestClass]
    public class ReviewsControllerTests
    {
        [TestMethod]
        public async Task GetReviewTestAsync()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = false, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo);
            var id = 2;

            //Act
            var result = await controller.GetReview(id);

            //Assert
            Assert.AreEqual(id, result.Value.Id);
            Assert.AreEqual(reviews[id - 1].Id, result.Value.Id);
            Assert.AreEqual(reviews[id - 1].Content, result.Value.Content);
            Assert.AreEqual(reviews[id - 1].IsVisible, result.Value.IsVisible);
            Assert.AreEqual(reviews[id - 1].PurchaseId, result.Value.PurchaseId);
            Assert.AreEqual(reviews[id - 1].Rating, result.Value.Rating);
        }

        [TestMethod]
        public async Task ProductAverageTestAsync()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1, Purchase = new Purchase
                {
                    AccountId = 1, Id = 1, ProductId = 1
                }},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = true, PurchaseId = 4, Rating = 5, Purchase = new Purchase
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
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 4, Id =  12
                }}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo);
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(2.75, result.Value);
        }

        [TestMethod]
        public async Task ProductAverageTestAsync_Hidden()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1, Purchase = new Purchase
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
                new Review { Id = 5, Content = "Review No. 5", IsVisible = false, PurchaseId = 3, Rating = 2, Purchase = new Purchase
                {
                    AccountId = 2, Id = 3, ProductId = 1
                }},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 4, Id =  12
                }}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo);
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public void ProductAverage_NotExists()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1, Purchase = new Purchase
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
                new Review { Id = 5, Content = "Review No. 5", IsVisible = false, PurchaseId = 3, Rating = 2, Purchase = new Purchase
                {
                    AccountId = 2, Id = 3, ProductId = 1
                }},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3, Purchase = new Purchase
                {
                    AccountId = 3, ProductId = 4, Id =  12
                }}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo);
            var prodId = 73;

            //Act
            var result = controller.ProductAverage(prodId);

            //Assert
            Assert.IsInstanceOfType(result.Result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PutReviewTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PostReviewTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void DeleteReviewTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetReviewProductTest()
        {
            Assert.Fail();
        }
    }
}