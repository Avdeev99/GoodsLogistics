using System;
using System.Collections.Generic;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.ViewModels.DTO
{
    public class ObjectiveUpdateRequestViewModel
    {
        public string SenderCompanyId { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderFrequency Frequency { get; set; }

        public List<RuleModel> Rules { get; set; }
    }
}
