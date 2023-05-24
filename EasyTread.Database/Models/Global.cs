using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class Global
{
    [Key]
    public int GlobalId { get; set; }

    public decimal? Value { get; set; }

    public string VehicleRating { get; set; }
}