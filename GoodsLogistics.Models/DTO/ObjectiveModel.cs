using System;

namespace GoodsLogistics.Models.DTO
{
    public class ObjectiveModel
    {
        public string ObjectiveId { get; set; }

        public string SenderCompanyId { get; set; }

        public UserCompanyModel SenderCompany { get; set; }

        public string ReceiverCompanyId { get; set; }

        public UserCompanyModel ReceiverCompany { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsRemoved { get; set; }
    }
}
