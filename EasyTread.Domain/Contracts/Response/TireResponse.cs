namespace EasyTread.Domain.Contracts.Response;

public class TireResponse
{
    public bool Valid { get; set; }

    public List<RegionResponse> RegionResponse { get; set; }

    public PropertySetResponse PropertySetResponse { get; set; }
}