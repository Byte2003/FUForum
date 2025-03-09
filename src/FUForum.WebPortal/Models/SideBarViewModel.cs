using FUForum.ViewModels.Contents;

namespace FUForum.WebPortal.Models
{
    public class SideBarViewModel
    {
        public List<KnowledgeBaseQuickVM> PopularKnowledgeBases { get; set; }

        public List<CategoryVM> Categories { get; set; }

        public List<CommentVM> RecentComments { get; set; }
    }
}
