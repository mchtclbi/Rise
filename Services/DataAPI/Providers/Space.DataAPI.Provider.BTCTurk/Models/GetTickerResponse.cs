using System.Text.Json.Serialization;

namespace Space.DataAPI.Provider.BTCTurk.Models
{
    public class GetTickerResponse
    {
        [JsonPropertyName("data")]
        public List<TickerData> Data { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public object Message { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    public class TickerData
    {
        [JsonPropertyName("pair")]
        public string Pair { get; set; }

        [JsonPropertyName("pairNormalized")]
        public string PairNormalized { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("last")]
        public int Last { get; set; }

        [JsonPropertyName("high")]
        public int High { get; set; }

        [JsonPropertyName("low")]
        public int Low { get; set; }

        [JsonPropertyName("bid")]
        public int Bid { get; set; }

        [JsonPropertyName("ask")]
        public int Ask { get; set; }

        [JsonPropertyName("open")]
        public int Open { get; set; }

        [JsonPropertyName("volume")]
        public double Volume { get; set; }

        [JsonPropertyName("average")]
        public int Average { get; set; }

        [JsonPropertyName("daily")]
        public int Daily { get; set; }

        [JsonPropertyName("dailyPercent")]
        public double DailyPercent { get; set; }

        [JsonPropertyName("denominatorSymbol")]
        public string DenominatorSymbol { get; set; }

        [JsonPropertyName("numeratorSymbol")]
        public string NumeratorSymbol { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }
    }
}