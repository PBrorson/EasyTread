using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class MiddleRegion
{
    [Key]
    public int MiddleRegionId { get; set; }

    public decimal? Value { get; set; }

    public string VehicleRating { get; set; }
}