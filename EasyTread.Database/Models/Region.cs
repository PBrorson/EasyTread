using EasyTread.Database.Models.DbEnum;
using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models
{
    public class Region
    {
        [Key]
        public int RegionId { get; set; }

        public RegionPositionEnum RegionPosition { get; set; }

        public decimal Value { get; set; }

        public string VehicleRating { get; set; }
    }
}