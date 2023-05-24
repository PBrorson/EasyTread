using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts;

public class MiddleRegionRequest
{
    [JsonProperty("value")]
    public decimal? Value { get; set; }

    [JsonProperty("rating")]
    public string Rating { get; set; }
}