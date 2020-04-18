using System.Reflection.Metadata;

namespace GoodsLogistics.Models.DTO
{
    public class RuleModel
    {
        public string RuleId { get; set; }

        public string Content { get; set; }

        public string ObjectiveId { get; set; }

        public ObjectiveModel Objective { get; set; }
    }
}
