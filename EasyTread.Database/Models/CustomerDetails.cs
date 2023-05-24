using System.ComponentModel.DataAnnotations;

namespace EasyTread.Database.Models
{
    public class CustomerDetails
    {
        [Key]
        public int CustomerDetailsId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAdress { get; set; }
    }
}