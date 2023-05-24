using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models;

public class PropertySet
{
    [Key]
    public int PropertySetId { get; set; }

    public bool DeepGrove { get; set; }

    public WearPattern WearPattern { get; set; }

    public ShoulderWear ShoulderWear { get; set; }
}