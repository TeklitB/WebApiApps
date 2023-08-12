using CoinDeskWebApiApp.interfaces;
using CoinDeskWebApiApp.Models;
using System.Text.Json;

namespace CoinDeskWebApiApp.Services
{
    public class HttpClientFactoryService : IHttpClientFactoryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUri;

        public HttpClientFactoryService(ICoinDeskSettings settings, IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
            _baseUri = settings.Url;       
        }

        public async Task<BitCoin> GetBitCoinContent()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var apiUrl = $"{_baseUri}currentprice.json";
                var response = await client.GetAsync(apiUrl);

                var btcContent = JsonSerializer.Deserialize<BitCoin>(await response.Content.ReadAsStringAsync());
                return btcContent;
            }
            catch (Exception ex)
            {
                return await Task.FromException<BitCoin>(ex);
            }
        }

        /// <summary>
        /// It’s also possible to have named clients, you can do that by registering it via DI (you can register how many you want).
        /// This is how you can configure.
        /// </summary>
        /// <returns></returns>
        public async Task<BitCoin> GetBitCoinContentWithUsing()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CoinDeskSettings");

                var response = await client.GetAsync("currentprice.json");

                var btcContent = JsonSerializer.Deserialize<BitCoin>(await response.Content.ReadAsStringAsync());
                return btcContent;
            }
            catch (Exception ex)
            {
                return await Task.FromException<BitCoin>(ex);
            }
        }
    }
}
