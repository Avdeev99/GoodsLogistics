using System;
using GoodsLogistics.Models.Enums;

namespace GoodsLogistics.ViewModels.DTO
{
    public class ObjectiveUpdateRequestViewModel
    {
        public string ReceiverCompanyId { get; set; }

        public DateTime EndDate { get; set; }

        public OrderFrequency Frequency { get; set; }
    }
}
