using System;

namespace GoodsLogistics.ViewModels.DTO
{
    public class ObjectiveUpdateRequestViewModel
    {
        public string ReceiverCompanyId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
