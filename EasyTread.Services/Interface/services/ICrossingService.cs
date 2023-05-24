using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;

namespace EasyTread.Services.Interface.services
{
    public interface ICrossingService
    {
        Task<ICollection<CrossingResponse>> GetAllCrossingVehiclesAsync();

        Task<ICollection<CrossingResponse>> GetAllBadAndMarginalVehiclesAsync();

        Task CreateCrossingAsync(CrossingRequest crossingRequest, string dealerNumberHeaderValue);

        Task<CrossingResponse> ShowLatestCrossingAsync();

        Task<List<CrossingResponse>> ShowANumberOfLatestCrossingsAsync(int count);

        List<CrossingResponse> GetCrossingByLicensePlate(string licensePlate);
    }
}