using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request;

public class LicensePlateRequest
{
    [JsonProperty("plate")]
    public string Plate { get; set; }

    [JsonProperty("plateConfidence")]
    public int PlateConfidence { get; set; }

    [JsonProperty("country")]
    public string? Country { get; set; }

    [JsonProperty("countryConfidence")]
    public int CountryConfidence { get; set; }
}