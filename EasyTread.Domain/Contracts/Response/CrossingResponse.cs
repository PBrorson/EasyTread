namespace EasyTread.Domain.Contracts.Response;

public class CrossingResponse
{
    public string DealerNumber { get; set; }
    public DateTime CreatedTime { get; set; }
    public bool Valid { get; set; }

    public string VehicleRating { get; set; }

    public string ReportLink { get; set; }

    public string CrossingDirection { get; set; }

    public LicensePlateResponse LicensePlate { get; set; }

    public List<TireResponse> Tires { get; set; }

}