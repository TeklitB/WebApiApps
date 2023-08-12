using System.Text.Json.Serialization;

namespace CoinDeskWebApiApp.Models
{
    public class BitCoin
    {
        [JsonPropertyName("bpi")]
        public Bpi Bpi { get; set; }
    }

    public class Bpi
    {
        [JsonPropertyName("EUR")]
        public Eur Eur { get; set; }
    }

    public class Eur
    {
        [JsonPropertyName("rate")]
        public string Rate { get; set; }
    }
}
