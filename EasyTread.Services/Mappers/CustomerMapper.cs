using EasyTread.Database.Models;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;
using EasyTread.Services.Interface.mappers;

namespace EasyTread.Services.Mappers
{
    public class CustomerMapper : ICustomerMapper
    {
        public CustomerDetails Map(CustomerDetailsRequest customerDetailsRequest)
        {
            return new CustomerDetails
            {
                FirstName = customerDetailsRequest.FirstName,
                LastName = customerDetailsRequest.LastName,
                Address = customerDetailsRequest.Address,
                City = customerDetailsRequest.City,
                PhoneNumber = customerDetailsRequest.PhoneNumber,
                EmailAdress = customerDetailsRequest.EmailAdress,
            };
        }

        public CustomerDetailsResponse Map(CustomerDetails customerDetails)
        {
            return new CustomerDetailsResponse
            {
                FirstName = customerDetails.FirstName,
                LastName = customerDetails.LastName,
                Address = customerDetails.Address,
                City = customerDetails.City,
                PhoneNumber = customerDetails.PhoneNumber,
                EmailAdress = customerDetails.EmailAdress,
            };
        }
    }
}