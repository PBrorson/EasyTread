using EasyTread.Database.Models.DbEnum;
using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models
{
    public class Tire
    {
        [Key]
        public int TireId { get; set; }

        public bool Valid { get; set; }

        public TireEnum TirePosition { get; set; }

        public List<Region> Regions { get; set; }

        public PropertySet PropertySet { get; set; }
    }
}