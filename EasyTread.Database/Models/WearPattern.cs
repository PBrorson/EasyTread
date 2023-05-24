using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class WearPattern
{
    [Key]
    public int WearPatternId { get; set; }

    public string Info { get; set; }

    public decimal Value { get; set; }
}