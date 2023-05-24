using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class LicensePlate
{
    [Key]
    public int LicensePlateId { get; set; }

    public string Plate { get; set; }

    public int PlateConfidence { get; set; }

    public string Country { get; set; }

    public int CountryConfidence { get; set; }
}