using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GoodsLogistics.BLL.Helpers
{
    public static class BadRequestObjectResultFactory
    {
        public static BadRequestObjectResult Create(string key, string errorMessage)
        {
            var errors = new Dictionary<string, string>()
            {
                { key, errorMessage}
            };

            var badResult = new BadRequestObjectResult(errors);
            return badResult;
        }
    }
}
