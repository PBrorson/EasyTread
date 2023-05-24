using EasyTread.Database.Models.DbEnum;

namespace EasyTread.Domain.Contracts.Response;

public class RegionResponse
{
    public RegionPositionEnum RegionPosition { get; set; }

    public decimal Value { get; set; }

    public string VehicleRating { get; set; }
}