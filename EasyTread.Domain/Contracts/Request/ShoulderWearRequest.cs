using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request;

public class ShoulderWearRequest
{
    [JsonProperty("info")]
    public string Info { get; set; }

    [JsonProperty("value")]
    public int Value { get; set; }
}