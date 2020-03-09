using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GoodsLogistics.BLL.Helpers
{
    public static class BadRequestObjectResultFactory
    {
        public static BadRequestObjectResult Create(string key, string errorMessage)
        {
            var modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError(key, errorMessage);

            var badResult = new BadRequestObjectResult(modelStateDictionary);
            return badResult;
        }
    }
}
