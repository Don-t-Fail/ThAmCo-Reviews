using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reviews.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Reviews.Models;

namespace Reviews.Data.Purchases
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<PurchaseService> _logger;
        private readonly IPurchaseRepository _purchaseRepo;

        public HttpClient HttpClient { get; set; }

        public PurchaseService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<PurchaseService> logger, IPurchaseRepository purchaseRepo)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
            _purchaseRepo = purchaseRepo;
        }

        public async Task<List<PurchaseDto>> GetAll()
        {
            var client = GetHttpClient("RetryAndBreak");

            _logger.LogInformation("Contacting Purchasing Service");

            var resp = await client.GetAsync("purchases/GetAll");

            if (resp.IsSuccessStatusCode)
            {
                var purchases = await resp.Content.ReadAsAsync<List<PurchaseDto>>();
                foreach (var purchase in purchases)
                {
                    purchase.PurchaseRef = purchase.Id;
                    purchase.Id = 0;
                }
                return purchases;
            }

            return null;
        }

        public async Task<PurchaseDto> GetPurchase(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var client = GetHttpClient("RetryAndBreak");

            _logger.LogInformation("Contacting Purchase Service");

            var resp = await client.GetAsync("purchases/details/" + id);

            if (resp.IsSuccessStatusCode)
            {
                var purchase = await resp.Content.ReadAsAsync<PurchaseDto>();
                if (purchase != null)
                {
                    purchase.PurchaseRef = purchase.Id;
                    purchase.Id = 0;
                    var newPurchase = new Purchase
                    {
                        AccountId = purchase.AccountId,
                        ProductId = purchase.ProductId,
                        PurchaseRef = purchase.PurchaseRef
                    };

                    await _purchaseRepo.InsertPurchase(newPurchase);
                    await _purchaseRepo.Save();

                    return purchase;
                }
            }

            return null;
        }

        private HttpClient GetHttpClient(string s)
        {
            if (_clientFactory == null && HttpClient != null)
            {
                return HttpClient;
            }

            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["PurchasesUrl"]);

            return client;
        }
    }
}