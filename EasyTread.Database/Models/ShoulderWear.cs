using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class ShoulderWear
{
    [Key]
    public int ShoulderWearId { get; set; }

    public string Info { get; set; }

    public int Value { get; set; }
}