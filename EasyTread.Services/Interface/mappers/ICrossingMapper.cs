using EasyTread.Database.Models;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;

namespace EasyTread.Services.Interface.mappers
{
    public interface ICrossingMapper
    {
        Crossing Map(CrossingRequest crossingRequest, string dealerNumberHeaderValue);

        CrossingResponse Map(Crossing crossing);
    }
}