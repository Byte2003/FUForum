using FUForum.ViewModels.Contents;
using FUForum.ViewModels.Systems;

namespace FUForum.WebPortal.Models
{
    public class KnowledgeBaseDetailViewModel
    {
        public CategoryVM Category { set; get; }
        public KnowledgeBaseVM Detail { get; set; }

        public List<LabelVM> Labels { get; set; }

        public UserVM CurrentUser { get; set; }
    }
}
