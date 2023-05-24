using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts;

public class WearPatternRequest
{
    [JsonProperty("info")]
    public string Info { get; set; }

    [JsonProperty("value")]
    public decimal Value { get; set; }
}