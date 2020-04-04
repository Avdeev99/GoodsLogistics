using System.Collections.Generic;
using AutoMapper;
using GoodsLogistics.BLL.Services.Interfaces;
using GoodsLogistics.DAL.Repositories.Interfaces;
using GoodsLogistics.Models.DTO.Location;
using GoodsLogistics.Models.Enums;
using GoodsLogistics.ViewModels.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services
{
    public class LocationService : ILocationService
    {
        private readonly IGenericRepository<CountryModel> _countryRepository;
        private readonly IGenericRepository<RegionModel> _regionRepository;
        private readonly IGenericRepository<CityModel> _cityRepository;
        private readonly IMapper _mapper;

        public LocationService(
            IGenericRepository<CountryModel> countryRepository, 
            IMapper mapper, 
            IGenericRepository<RegionModel> regionRepository,
            IGenericRepository<CityModel> cityRepository)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _regionRepository = regionRepository;
            _cityRepository = cityRepository;
        }

        public ObjectResult GetCountries()
        {
            var countries = _countryRepository.GetAll(TrackingState.Disabled);
            var countryViewModels = _mapper.Map<List<CountryViewModel>>(countries);

            var result = new OkObjectResult(countryViewModels);
            return result;
        }

        public ObjectResult GetRegionsByCountryId(int countryId)
        {
            var country = _countryRepository.Get(
                countryModel => countryModel.Id.Equals(countryId),
                TrackingState.Disabled);
            if (country == null)
            {
                var notFoundResult = new NotFoundObjectResult("Country by provided id not found");
                return notFoundResult;
            }

            var regions = _regionRepository.GetMany(
                regionModel => regionModel.CountryId.Equals(countryId), 
                regionModel => regionModel.Name,
                TrackingState.Disabled);
            var regionViewModels = _mapper.Map<List<RegionViewModel>>(regions);

            var result = new OkObjectResult(regionViewModels);
            return result;
        }

        public ObjectResult GetCitiesByRegionId(int regionId)
        {
            var region = _regionRepository.Get(
                regionModel => regionModel.Id.Equals(regionId),
                TrackingState.Disabled);
            if (region == null)
            {
                var notFoundResult = new NotFoundObjectResult("Region by provided id not found");
                return notFoundResult;
            }

            var cities = _cityRepository.GetMany(
                regionModel => regionModel.RegionId.Equals(regionId),
                regionModel => regionModel.Name,
                TrackingState.Disabled);
            var cityViewModels = _mapper.Map<List<CityViewModel>>(cities);

            var result = new OkObjectResult(cityViewModels);
            return result;
        }
    }
}
