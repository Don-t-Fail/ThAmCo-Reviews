using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Reviews.Services;

namespace Reviews.Data.Purchases
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<PurchaseService> _logger;

        public PurchaseService(IHttpClientFactory clientFactory, IConfiguration config, ILogger<PurchaseService> logger)
        {
            _clientFactory = clientFactory;
            _config = config;
            _logger = logger;
        }

        public async Task<List<PurchaseDto>> GetAll()
        {
            var client = _clientFactory.CreateClient("RetryAndBreak");
            client.BaseAddress = new Uri(_config["PurchasesUrl"]);

            _logger.LogInformation("Contacting Purchasing Service");

            var resp = await client.GetAsync("purchases/GetAll");

            if (resp.IsSuccessStatusCode)
            {
                var purchases = await resp.Content.ReadAsAsync<List<PurchaseDto>>();
                return purchases;
            }

            return null;
        }

        public async Task<PurchaseDto> GetPurchase(int id)
        {
            throw new NotImplementedException();
        }
    }
}
