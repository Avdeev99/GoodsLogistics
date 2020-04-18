using System;
using System.Collections.Generic;
using GoodsLogistics.Models.DTO.Location;
using GoodsLogistics.Models.DTO.UserCompany;
using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.Models.DTO
{
    public class ObjectiveModel
    {
        public string ObjectiveId { get; set; }

        public string SenderCompanyId { get; set; }

        public UserCompanyModel SenderCompany { get; set; }

        public string ReceiverCompanyId { get; set; }

        public UserCompanyModel ReceiverCompany { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreationDate { get; set; }

        public OrderFrequency Frequency { get; set; }

        public GoodModel Good { get; set; }

        public bool IsRemoved { get; set; }

        public LocationModel Location { get; set; }

        public decimal Price { get; set; }

        public List<RuleModel> Rules { get; set; }
    }
}
