using EasyTread.Database;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;
using EasyTread.Services.Interface.mappers;
using Microsoft.EntityFrameworkCore;

namespace EasyTread.Services.Interface.services
{
    public class CrossingService : ICrossingService
    {
        private readonly EasyTreadContext _dbContext;
        private readonly ICrossingMapper _crossingMapper;

        public CrossingService(EasyTreadContext dbContext, ICrossingMapper crossingMapper)
        {
            _dbContext = dbContext;
            _crossingMapper = crossingMapper;
        }

        public async Task CreateCrossingAsync(CrossingRequest crossingRequest, string dealerNumberHeaderValue)
        {
            var newCrossing = _crossingMapper.Map(crossingRequest, dealerNumberHeaderValue);

            await _dbContext.AddAsync(newCrossing);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<CrossingResponse>> GetAllCrossingVehiclesAsync()
        {
            var crossing = await _dbContext
                .Crossing
                .Include(c => c.LicensePlate)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.WearPattern)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.ShoulderWear)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.Regions)
                .AsNoTracking()
                .ToListAsync();

            return crossing
                .Select(_ => _crossingMapper.Map(_))
                .ToList();
        }

        public async Task<ICollection<CrossingResponse>> GetAllBadAndMarginalVehiclesAsync()
        {
            var crossing = await _dbContext
                .Crossing
                .Include(c => c.LicensePlate)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.WearPattern)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.ShoulderWear)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.Regions)
                .Where(c => c.VehicleRating != "GOOD")
                .AsNoTracking()
                .ToListAsync();

            return crossing
                .Select(_ => _crossingMapper.Map(_))
                .ToList();
        }

        public async Task<CrossingResponse> ShowLatestCrossingAsync()
        {
            var latestCrossing = await _dbContext.Crossing
                .Include(c => c.LicensePlate)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.WearPattern)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.ShoulderWear)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.Regions)
                .OrderByDescending(c => c.CrossingId)
                .FirstOrDefaultAsync();

            if (latestCrossing == null)
            {
                return null;
            }

            return _crossingMapper.Map(latestCrossing);
        }

        public async Task<List<CrossingResponse>> ShowANumberOfLatestCrossingsAsync(int count)
        {
            var latestCrossings = await _dbContext.Crossing
                .Include(c => c.LicensePlate)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.WearPattern)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.ShoulderWear)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.Regions)
                .OrderByDescending(c => c.CrossingId)
                .Take(count)
                .ToListAsync();

            return latestCrossings.Select(c => _crossingMapper.Map(c)).ToList();
        }

        public List<CrossingResponse> GetCrossingByLicensePlate(string licensePlate)
        {
            var vehicle = _dbContext.Crossing
                .Include(c => c.LicensePlate)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.WearPattern)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.PropertySet)
                        .ThenInclude(t => t.ShoulderWear)
                .Include(c => c.Tires)
                    .ThenInclude(t => t.Regions)
                .Where(c => c.LicensePlate.Plate == licensePlate)
                .ToList();

            if (vehicle == null)
            {
                return null;
            }

            var response = vehicle.Select(c => _crossingMapper.Map(c)).ToList();

            return response;
        }
    }
}