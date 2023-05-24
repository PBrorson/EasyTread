using EasyTread.Database.Models;
using EasyTread.Database.Models.DbEnum;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;
using EasyTread.Services.Interface.mappers;

namespace EasyTread.Services.Mappers
{
    public class CrossingMapper : ICrossingMapper
    {
        private readonly IVehicleMapper _vehicleMapper;

        public CrossingMapper(IVehicleMapper vehicleMapper)
        {
            _vehicleMapper = vehicleMapper;
        }

        public Crossing Map(CrossingRequest crossingRequest, string dealerNumberHeaderValue)
        {
            var result = crossingRequest.Results;

            var tirePositions = new Dictionary<TireEnum, TireRequest>
            {
                { TireEnum.FrontLeft, result.FrontLeft },
                { TireEnum.FrontRight, result.FrontRight },
                { TireEnum.RearLeft, result.RearLeft },
                { TireEnum.RearRight, result.RearRight }
            };

            var allTires = new List<Tire>();

            foreach (var position in tirePositions)
            {
                var tire = _vehicleMapper.Map(position.Value);
                tire.TirePosition = position.Key;
                allTires.Add(tire);
            }

            var crossing = new Crossing
            {
                DealerNumber = dealerNumberHeaderValue,
                CreatedTime = DateTime.Now,
                Valid = result.Valid,
                VehicleRating = result.VehicleRating,
                ReportLink = result.ReportLink,
                CrossingDirection = result.CrossingDirection,
                LicensePlate = _vehicleMapper.Map(result.LicensePlate),
                Tires = allTires
            };

            return crossing;
        }

        public CrossingResponse Map(Crossing crossing)
        {
            return new CrossingResponse
            {
                DealerNumber = crossing.DealerNumber,
                CreatedTime = crossing.CreatedTime,
                Valid = crossing.Valid,
                VehicleRating = crossing.VehicleRating,
                ReportLink = crossing.ReportLink,
                CrossingDirection = crossing.CrossingDirection,
                LicensePlate = _vehicleMapper.Map(crossing.LicensePlate),
                Tires = crossing.Tires
                    .GroupBy(t => t.TirePosition)
                    .Select(g => _vehicleMapper.Map(g.FirstOrDefault(t => t != null)))
                    .ToList()
            };
        }
    }
}