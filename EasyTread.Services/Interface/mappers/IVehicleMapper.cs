using EasyTread.Database.Models;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;

namespace EasyTread.Services.Interface.mappers
{
    public interface IVehicleMapper
    {
        Tire Map(TireRequest tireRequest);

        LicensePlate Map(LicensePlateRequest licensePlateRequests);

        TireResponse Map(Tire tire);

        LicensePlateResponse Map(LicensePlate licensePlate);
    }
}