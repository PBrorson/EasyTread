using EasyTread.Database.Models;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;

namespace EasyTread.Services.Interface.mappers
{
    public interface ICustomerMapper
    {
        CustomerDetails Map(CustomerDetailsRequest customerDetailsRequest);

        CustomerDetailsResponse Map(CustomerDetails customerDetails);
    }
}