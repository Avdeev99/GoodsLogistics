using System;
using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.Models.DTO.Objective
{
    public class ObjectiveUpdateRequestModel
    {
        public string ReceiverCompanyId { get; set; }

        public DateTime EndDate { get; set; }

        public OrderFrequency Frequency { get; set; }
    }
}
