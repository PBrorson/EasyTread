using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts;

public class OuterRegionRequest
{
    [JsonProperty("value")]
    public decimal? Value { get; set; }

    [JsonProperty("rating")]
    public string Rating { get; set; }
}