using Microsoft.AspNetCore.Mvc;

namespace GoodsLogistics.BLL.Services.Interfaces
{
    public interface ILocationService
    {
        ObjectResult GetCountries();

        ObjectResult GetRegionsByCountryId(int countryId);

        ObjectResult GetCitiesByRegionId(int regionId);
    }
}
