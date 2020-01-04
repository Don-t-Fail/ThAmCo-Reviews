using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reviews.Models;
using Reviews.Services;

namespace Reviews.Data.Purchases
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<PurchaseService> _logger;
        private readonly ReviewDbContext _context;

        public HttpClient HttpClient { get; set; }

        public PurchaseService(ReviewDbContext context, IHttpClientFactory clientFactory, IConfiguration config, ILogger<PurchaseService> logger)
        {
            _context = context;
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        public async Task<List<PurchaseDto>> GetAll()
        {
            var client = _clientFactory.CreateClient("RetryAndBreak");
            

            _logger.LogInformation("Contacting Purchasing Service");

            var resp = await client.GetAsync("purchases/GetAll");

            if (resp.IsSuccessStatusCode)
            {
                var purchases = await resp.Content.ReadAsAsync<List<PurchaseDto>>();

                foreach (var purchase in purchases)
                {
                    if (!_context.Purchase.Any(p => p.Id == purchase.Id))
                    {
                        await _context.Purchase.AddAsync(new Purchase {ProductId = purchase.ProductId, AccountId = purchase.AccountId});
                    }
                }

                await _context.SaveChangesAsync();
                return purchases;
            }

            return null;
        }



        public async Task<PurchaseDto> GetPurchase(int id)
        {
            var client = GetHttpClient("RetryAndBreak");

            _logger.LogInformation("Contacting Purchase Service");

            var resp = await client.GetAsync("purchases/details/"+id);

            if (resp.IsSuccessStatusCode)
            {
                var purchase = await resp.Content.ReadAsAsync<PurchaseDto>();

                if (!_context.Purchase.Any(p => p.Id == purchase.Id))
                {
                    await _context.Purchase.AddAsync(new Purchase { ProductId = purchase.ProductId, AccountId = purchase.AccountId });
                }

                await _context.SaveChangesAsync();
                return purchase;
            }

            return null;
        }

        private HttpClient GetHttpClient(string s)
        {
            if (_clientFactory == null && HttpClient != null) return HttpClient;

            var client = _clientFactory.CreateClient(s);
            client.BaseAddress = new Uri(_config["PurchasesUrl"]);

            return client;
        }
    }
}
