using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reviews.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
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
                new Review { Id = 2, Content = "Review No. 2", IsVisible = true, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
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
        public async Task GetReviewTestAsync_Hidden()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = false, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
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
        public async Task GetReviewTestAsync_NotExists()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1},
                new Review { Id = 2, Content = "Review No. 2", IsVisible = false, PurchaseId = 4, Rating = 5},
                new Review { Id = 3, Content = "Review No. 3", IsVisible = true, PurchaseId = 12, Rating = 3}
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(3, result.Value);
        }

        [TestMethod]
        public async Task ProductAverageTest_Hidden()
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(2, result.Value);
        }

        [TestMethod]
        public async Task ProductAverage_NotExists()
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 73;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ProducAverageTest_OneReview()
        {
            //Arrange
            var reviews = new List<Review>
            {
                new Review
                {
                    Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1, Purchase =
                        new Purchase
                        {
                            AccountId = 1, Id = 1, ProductId = 1
                        }
                },
                new Review
                {
                    Id = 2, Content = "Review No. 2", IsVisible = false, PurchaseId = 4, Rating = 5, Purchase =
                        new Purchase
                        {
                            AccountId = 2, Id = 4, ProductId = 2
                        }
                }
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 1;

            //Act
            var result = await controller.ProductAverage(prodId);

            //Assert
            Assert.AreEqual(1,result.Value);
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var review = new Review { Id = 4, Content = "Review 4", IsVisible = true, PurchaseId = 2, Rating = 5 };

            //Act
            var result = await controller.PutReview(5, review);

            //Assert
            Assert.IsInstanceOfType(result,typeof(BadRequestResult));
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var review = new Review { Id = 4, Content = "Review 4", IsVisible = true, PurchaseId = 2, Rating = 5 };

            //Act
            // TODO - Fix NullPointerException
            var result = await controller.PutReview(4, review);

            //Assert
            Assert.IsInstanceOfType(result,typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostReviewTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public async Task GetReviewProductTest()
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 1;

            //Act
            var result = await controller.GetReviewProduct(prodId);

            //Assert
            Assert.AreEqual(reviews.Where(r => r.Purchase.ProductId == prodId && r.IsVisible),result.Value);
        }

        [TestMethod]
        public async Task GetReviewProductTest_Hidden()
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
                new Review { Id = 4, Content = "Review No. 4", IsVisible = false, PurchaseId = 2, Rating = 3, Purchase = new Purchase
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 1;

            //Act
            var result = await controller.GetReviewProduct(prodId);

            //Assert
            Assert.AreEqual(reviews.Where(r => r.Purchase.ProductId == prodId && r.IsVisible).ToList(), result);
        }

        [TestMethod]
        public async Task GetReviewProductTest_NotExists()
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
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var prodId = 37;

            //Act
            var result = await controller.GetReviewProduct(prodId);

            //Assert
            Assert.IsInstanceOfType(result.Result,typeof(NotFoundResult));
        }


        [TestMethod]
        public void DeleteReviewTest()
        {
            var reviews = new List<Review>
            {
                //Arrange
                new Review
                {
                    Id = 1, Content = "Review No. 1", IsVisible = true, PurchaseId = 1, Rating = 1
                },
                new Review
                {
                    Id = 2, Content = "Review No. 2", IsVisible = true, PurchaseId = 4, Rating = 5
                }
            };
            var repo = new FakeReviewRepository(reviews);
            var controller = new ReviewsController(repo, new NullLogger<ReviewsController>());
            var reviewId = 1;

            //Act
            controller.DeleteReview(reviewId);

            //Assert
            Assert.IsFalse(reviews.Find(r => r.Id == 1).IsVisible);
        }
    }
}