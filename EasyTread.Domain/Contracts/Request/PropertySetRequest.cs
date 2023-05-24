using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request;

public class PropertySetRequest
{
    [JsonProperty("deepGrove")]
    public bool DeepGrove { get; set; }

    [JsonProperty("wearPattern")]
    public WearPatternRequest WearPattern { get; set; }

    [JsonProperty("shoulderWear")]
    public ShoulderWearRequest ShoulderWear { get; set; }
}