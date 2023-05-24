using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class OuterRegion
{
    [Key]
    public int OuterRegionId { get; set; }

    public decimal? Value { get; set; }

    public string VehicleRating { get; set; }
}