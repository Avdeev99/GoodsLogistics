using System;
using GoodsLogistics.Models.DTO.Enum;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.Models.DTO
{
    public class RequestModel
    {
        public string RequestId { get; set; }

        public ObjectiveModel Objective { get; set; }

        public string ObjectiveId { get; set; }

        public string CompanyId { get; set; }

        public UserCompanyModel UserCompany { get; set; }

        public RequestStatus Status { get; set; }

        public bool IsRemoved { get; set; }
    }
}
