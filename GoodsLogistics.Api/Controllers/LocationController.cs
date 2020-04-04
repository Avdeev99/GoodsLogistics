using GoodsLogistics.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.Api.Controllers
{
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("countries")]
        public IActionResult GetCountries()
        {
            var result = _locationService.GetCountries();
            return result;
        }

        [HttpGet("countries/{countryId}/regions")]
        public IActionResult GetRegionsByCountryId(int countryId)
        {
            var result = _locationService.GetRegionsByCountryId(countryId);
            return result;
        }

        [HttpGet("countries/regions/{regionId}")]
        public IActionResult GetCitiesByRegionId(int regionId)
        {
            var result = _locationService.GetCitiesByRegionId(regionId);
            return result;
        }
    }
}