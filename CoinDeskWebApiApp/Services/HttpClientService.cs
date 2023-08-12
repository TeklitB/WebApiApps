using CoinDeskWebApiApp.interfaces;
using CoinDeskWebApiApp.Models;
using System.Text.Json;

namespace CoinDeskWebApiApp.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly string _baseUri;
        private static HttpClient _httpClient = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(1)
        });

        public HttpClientService(ICoinDeskSettings settings) 
        {
            _baseUri = settings.Url;
        }
        public async Task<BitCoin> GetBitCoinContent()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUri}currentprice.json");

                var btcContent = JsonSerializer.Deserialize<BitCoin>(await response.Content.ReadAsStringAsync());
                return btcContent;
            }
            catch (Exception ex)
            {
                return await Task.FromException<BitCoin>(ex);
            }
        }

        public async Task<BitCoin> GetBitCoinContentWithUsing()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync($"{_baseUri}currentprice.json");

                    var btcContent = JsonSerializer.Deserialize<BitCoin>(await response.Content.ReadAsStringAsync());
                    return btcContent;
                }
            }
            catch (Exception ex)
            {
                return await Task.FromException<BitCoin>(ex);
            }
        }
    }
}
