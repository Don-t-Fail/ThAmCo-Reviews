using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Reviews.Models;
using Reviews.Services;

namespace Reviews.Data.Purchases.Tests
{
    [TestClass]
    public class PurchaseServiceTests
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

            public static List<PurchaseDto> Purchases() => new List<PurchaseDto>
            {
                new PurchaseDto { Id = 1, AccountId = 1, ProductId = 1, AddressId = 1, Qty = 1},
                new PurchaseDto { Id = 2, AccountId = 1, ProductId = 1, AddressId = 1, Qty = 1},
                new PurchaseDto { Id = 3, AccountId = 2, ProductId = 1, AddressId = 2, Qty = 1}
            };
        }

        private Mock<HttpMessageHandler> CreateHttpMock(HttpResponseMessage expected)
        {
            var mock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expected)
                .Verifiable();
            return mock;
        }
        private HttpClient SetupMock_ListPurchaseDto()
        {
            var expectedHttpResp = TestData.Purchases().Select
            (
                p => new PurchaseDto
                {
                    Id = p.Id,
                    AccountId = p.AccountId,
                    ProductId = p.ProductId
                }
            );
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        private HttpClient SetupMock_PurchaseDto(int id)
        {
            var expectedHttpResp = TestData.Purchases().Select
            (
                p => new PurchaseDto
                {
                    Id = p.Id,
                    AccountId = p.AccountId,
                    ProductId = p.ProductId
                }
            ).FirstOrDefault(p => p.Id == id);
            var expectedJson = JsonConvert.SerializeObject(expectedHttpResp);
            var expResult = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson, Encoding.UTF8, "application/json")
            };
            return new HttpClient(CreateHttpMock(expResult).Object);
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            // Arrange
            var client = SetupMock_ListPurchaseDto();
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("http://localhost:12324/");
            config.SetupGet(s => s["PurchasesUrl"]).Returns("http:localhost:12324/");
            var purchaseRepo = new FakePurchaseRepository(TestData.Purchases().Select(p => new Purchase
            {
                AccountId = p.AccountId,
                Id = p.Id,
                ProductId = p.ProductId
            }).ToList());
            var service = new PurchaseService(null, config.Object, new NullLogger<PurchaseService>(), purchaseRepo) {HttpClient = client};

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            foreach (var purchase in result)
            {
                var testItem = TestData.Purchases().FirstOrDefault(p => p.Id == purchase.PurchaseRef);
                Assert.AreEqual(testItem.Id,purchase.PurchaseRef);
                Assert.AreEqual(testItem.AccountId, purchase.AccountId);
            }
        }

        [TestMethod]
        public async Task Get_Success()
        {
            // Arrange
            var id = 1;
            var client = SetupMock_PurchaseDto(id);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("http://localhost:12324/");
            config.SetupGet(s => s["PurchasesUrl"]).Returns("http:localhost:12324/");
            var purchaseRepo = new FakePurchaseRepository(TestData.Purchases().Select(p => new Purchase
            {
                AccountId = p.AccountId,
                Id = p.Id,
                ProductId = p.ProductId
            }).ToList());
            var service = new PurchaseService(null, config.Object, new NullLogger<PurchaseService>(), purchaseRepo) { HttpClient = client };

            // Act
            var result = await service.GetPurchase(id);

            // Assert
            Assert.IsNotNull(result);

            var testItem = TestData.Purchases().FirstOrDefault(p => p.Id == id);
            Assert.AreEqual(testItem.Id, result.PurchaseRef);
            Assert.AreEqual(testItem.AccountId, result.AccountId);
        }

        [TestMethod]
        public async Task Get_NotExist()
        {
            // Arrange
            var id = 75;
            var client = SetupMock_PurchaseDto(id);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("http://localhost:12324/");
            config.SetupGet(s => s["PurchasesUrl"]).Returns("http:localhost:12324/");
            var purchaseRepo = new FakePurchaseRepository(TestData.Purchases().Select(p => new Purchase
            {
                AccountId = p.AccountId,
                Id = p.Id,
                ProductId = p.ProductId
            }).ToList());
            var service = new PurchaseService(null, config.Object, new NullLogger<PurchaseService>(), purchaseRepo) { HttpClient = client };

            // Act
            var result = await service.GetPurchase(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Get_OutOfBounds()
        {
            // Arrange
            var id = 6;
            var client = SetupMock_PurchaseDto(id);
            var config = new Mock<IConfiguration>();
            client.BaseAddress = new Uri("http://localhost:12324/");
            config.SetupGet(s => s["PurchasesUrl"]).Returns("http:localhost:12324/");
            var purchaseRepo = new FakePurchaseRepository(TestData.Purchases().Select(p => new Purchase
            {
                AccountId = p.AccountId,
                Id = p.Id,
                ProductId = p.ProductId
            }).ToList());
            var service = new PurchaseService(null, config.Object, new NullLogger<PurchaseService>(), purchaseRepo) { HttpClient = client };

            // Act
            var result = await service.GetPurchase(id);

            // Assert
            Assert.IsNull(result);
        }
    }
}