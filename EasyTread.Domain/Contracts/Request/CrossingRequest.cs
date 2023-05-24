using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request;

public class CrossingRequest
{
    public string name { get; set; }

    public DateTime dateTime { get; set; }

    [JsonProperty("results")]
    public Result Results { get; set; }
}