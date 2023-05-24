﻿namespace EasyTread.Domain.Contracts.Request
{
    public class CustomerDetailsRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAdress { get; set; }
    }
}