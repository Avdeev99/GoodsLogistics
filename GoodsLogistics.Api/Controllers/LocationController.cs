using GoodsLogistics.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.Api.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public IActionResult GetCountries()
        {
            var result = _locationService.GetCountries();
            return result;
        }

        public IActionResult GetRegionsByCountryId(int countryId)
        {
            var result = _locationService.GetRegionsByCountryId(countryId);
            return result;
        }

        public IActionResult GetCitiesByRegionId(int regionId)
        {
            var result = _locationService.GetCitiesByRegionId(regionId);
            return result;
        }
    }
}