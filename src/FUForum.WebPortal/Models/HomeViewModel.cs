using FUForum.ViewModels.Contents;

namespace FUForum.WebPortal.Models
{
    public class HomeViewModel
    {
        public List<KnowledgeBaseQuickVM> LatestKnowledgeBases { get; set; }
        public List<KnowledgeBaseQuickVM> PopularKnowledgeBases { get; set; }

        public List<LabelVM> PopularLabels { get; set; }
    }
}
