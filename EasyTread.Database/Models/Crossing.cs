using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models
{
    public class Crossing
    {
        [Key]
        public int CrossingId { get; set; }

        public string DealerNumber { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool Valid { get; set; }

        public string VehicleRating { get; set; }

        public string ReportLink { get; set; }

        public string CrossingDirection { get; set; }

        public LicensePlate LicensePlate { get; set; }

        public List<Tire> Tires { get; set; }
    }
}