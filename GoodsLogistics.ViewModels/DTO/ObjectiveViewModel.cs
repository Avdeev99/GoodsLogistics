using System;
using GoodsLogistics.Models.DTO.UserCompany;

namespace GoodsLogistics.ViewModels.DTO
{
    public class ObjectiveViewModel
    {
        public string ObjectiveId { get; set; }

        public string SenderCompanyId { get; set; }

        public UserCompanyModel SenderCompany { get; set; }

        public string ReceiverCompanyId { get; set; }

        public UserCompanyModel ReceiverCompany { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
