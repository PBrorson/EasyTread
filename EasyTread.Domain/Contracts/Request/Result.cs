using Newtonsoft.Json;

namespace EasyTread.Domain.Contracts.Request;

public class Result
{
    [JsonProperty("valid")]
    public bool Valid { get; set; }

    [JsonProperty("vehicleRating")]
    public string VehicleRating { get; set; }

    [JsonProperty("reportLink")]
    public string ReportLink { get; set; }

    [JsonProperty("crossingDirection")]
    public string CrossingDirection { get; set; }

    [JsonProperty("licensePlate")]
    public LicensePlateRequest LicensePlate { get; set; }

    [JsonProperty("frontLeft")]
    public TireRequest FrontLeft { get; set; }

    [JsonProperty("frontRight")]
    public TireRequest FrontRight { get; set; }

    [JsonProperty("rearLeft")]
    public TireRequest RearLeft { get; set; }

    [JsonProperty("rearRight")]
    public TireRequest RearRight { get; set; }
}