using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class Minimum
{
    [Key]
    public int MinimumId { get; set; }

    public decimal? Value { get; set; }

    public string VehicleRating { get; set; }
}