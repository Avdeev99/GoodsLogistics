using System;
using System.Collections.Generic;
using System.Text;
using GoodsLogistics.Models.DTO;
using GoodsLogistics.Models.DTO.Enum;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.ViewModels.DTO
{
    public class RequestViewModel
    {
        public string RequestId { get; set; }

        public ObjectiveModel Objective { get; set; }

        public string ObjectiveId { get; set; }

        public string CompanyId { get; set; }

        public UserCompanyModel UserCompany { get; set; }

        public RequestStatus Status { get; set; }
    }
}
