using EasyTread.Database.Models;
using EasyTread.Database.Models.DbEnum;
using EasyTread.Domain.Contracts;
using EasyTread.Domain.Contracts.Request;
using EasyTread.Domain.Contracts.Response;
using EasyTread.Services.Interface.mappers;

namespace EasyTread.Services.Mappers
{
    public class VehicleMapper : IVehicleMapper
    {
        public Tire Map(TireRequest tireRequest)
        {
            return new Tire
            {
                Valid = tireRequest.Valid,
                PropertySet = Map(tireRequest.PropertySet),
                Regions = Map(tireRequest.ValueSet.Global,
                            tireRequest.ValueSet.Minimum,
                            tireRequest.ValueSet.InnerRegion,
                            tireRequest.ValueSet.MiddleRegion,
                            tireRequest.ValueSet.OuterRegion),
            };
        }

        private List<Region> Map(
            GlobalRequest globalRequest,
            MinimumRequest minimumRequest,
            InnerRegionRequest innerRegionRequest,
            MiddleRegionRequest middleRegionRequest,
            OuterRegionRequest outerRegionRequest)
        {
            var regions = new List<Region>();
            if (globalRequest != null)
            {
                regions.Add(new Region
                {
                    Value = globalRequest.Value ?? 0,
                    VehicleRating = globalRequest.Rating,
                    RegionPosition = RegionPositionEnum.Global
                });
            }
            if (minimumRequest != null)
            {
                regions.Add(new Region
                {
                    Value = minimumRequest.Value ?? 0,
                    VehicleRating = minimumRequest.Rating,
                    RegionPosition = RegionPositionEnum.Minimum
                });
            }
            if (innerRegionRequest != null)
            {
                regions.Add(new Region
                {
                    Value = innerRegionRequest.Value ?? 0,
                    VehicleRating = innerRegionRequest.Rating,
                    RegionPosition = RegionPositionEnum.InnerRegion
                });
            }
            if (middleRegionRequest != null)
            {
                regions.Add(new Region
                {
                    Value = middleRegionRequest.Value ?? 0,
                    VehicleRating = middleRegionRequest.Rating,
                    RegionPosition = RegionPositionEnum.MiddleRegion
                });
            }
            if (outerRegionRequest != null)
            {
                regions.Add(new Region
                {
                    Value = outerRegionRequest.Value ?? 0,
                    VehicleRating = outerRegionRequest.Rating,
                    RegionPosition = RegionPositionEnum.OuterRegion
                });
            }
            return regions;
        }

        public TireResponse Map(Tire tire)
        {
            if (tire == null)
            {
                return null;
            }

            return new TireResponse
            {
                Valid = tire.Valid,
                PropertySetResponse = Map(tire.PropertySet),
                RegionResponse = tire.Regions.Select(r => Map(r)).ToList()
            };
        }

        private RegionResponse Map(Region region)
        {
            return new RegionResponse
            {
                RegionPosition = region.RegionPosition,
                Value = region.Value,
                VehicleRating = region.VehicleRating,
            };
        }

        private PropertySet Map(PropertySetRequest propertySetRequest)
        {
            return new PropertySet
            {
                DeepGrove = propertySetRequest.DeepGrove,
                WearPattern = Map(propertySetRequest.WearPattern),
                ShoulderWear = Map(propertySetRequest.ShoulderWear),
            };
        }

        private WearPattern Map(WearPatternRequest wearPatternRequest)
        {
            return new WearPattern
            {
                Info = wearPatternRequest.Info,
                Value = wearPatternRequest.Value,
            };
        }

        private ShoulderWear Map(ShoulderWearRequest shoulderWearRequest)
        {
            return new ShoulderWear
            {
                Info = shoulderWearRequest.Info,
                Value = shoulderWearRequest.Value,
            };
        }

        public LicensePlate Map(LicensePlateRequest licensePlateRequests)
        {
            return new LicensePlate
            {
                Plate = licensePlateRequests.Plate,
                PlateConfidence = licensePlateRequests.PlateConfidence,
                Country = licensePlateRequests.Country,
                CountryConfidence = licensePlateRequests.CountryConfidence
            };
        }

        public PropertySetResponse Map(PropertySet propertySet)
        {
            return new PropertySetResponse
            {
                DeepGrove = propertySet.DeepGrove,
                WearPatternResponse = Map(propertySet.WearPattern),
                ShoulderWearResponse = Map(propertySet.ShoulderWear),
            };
        }

        private WearPatternResponse Map(WearPattern wearPattern)
        {
            return new WearPatternResponse
            {
                Info = wearPattern.Info,
                Value = wearPattern.Value,
            };
        }

        private ShoulderWearResponse Map(ShoulderWear shoulderWear)
        {
            return new ShoulderWearResponse
            {
                Info = shoulderWear.Info,
                Value = shoulderWear.Value,
            };
        }

        public LicensePlateResponse Map(LicensePlate licensePlate)
        {
            return new LicensePlateResponse
            {
                Plate = licensePlate.Plate,
                PlateConfidence = licensePlate.PlateConfidence,
                Country = licensePlate.Country,
                CountryConfidence = licensePlate.CountryConfidence,
            };
        }
    }
}