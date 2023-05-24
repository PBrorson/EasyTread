namespace EasyTread.Domain.Contracts.Response;

public class LicensePlateResponse
{
    public string Plate { get; set; }

    public int PlateConfidence { get; set; }

    public string Country { get; set; }

    public int CountryConfidence { get; set; }
}