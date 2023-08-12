using CoinDeskWebApiApp.Models;

namespace CoinDeskWebApiApp.interfaces
{
    public interface IHttpClientService
    {
        Task<BitCoin> GetBitCoinContent();
        Task<BitCoin> GetBitCoinContentWithUsing();
    }
}
