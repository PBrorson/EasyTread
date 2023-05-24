using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request;

public class MinimumRequest
{
    [JsonProperty("value")]
    public decimal? Value { get; set; }

    [JsonProperty("rating")]
    public string Rating { get; set; }
}