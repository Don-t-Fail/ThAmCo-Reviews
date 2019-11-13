using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reviews.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Assert.AreEqual(id,result.Value.Id);
            Assert.AreEqual(reviews[id - 1].Id, result.Value.Id);
            Assert.AreEqual(reviews[id - 1].Content, result.Value.Content);
            Assert.AreEqual(reviews[id - 1].IsVisible, result.Value.IsVisible);
            Assert.AreEqual(reviews[id - 1].PurchaseId, result.Value.PurchaseId);
            Assert.AreEqual(reviews[id - 1].Rating, result.Value.Rating);
        }

        [TestMethod]
        public void ProductAverageTest()
        {
            Assert.Fail();
        }
    }
}