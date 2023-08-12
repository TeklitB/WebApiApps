using CoinDeskWebApiApp.Models;

namespace CoinDeskWebApiApp.interfaces
{
    public interface IHttpClientFactoryService
    {
        Task<BitCoin> GetBitCoinContent();
        Task<BitCoin> GetBitCoinContentWithUsing();
    }
}
