using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class InnerRegion
{
    [Key]
    public int InnerRegionId { get; set; }

    public decimal? Value { get; set; }

    public string VehicleRating { get; set; }
}