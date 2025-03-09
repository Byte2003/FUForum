using FUForum.ViewModels.Contents;
using FUForum.ViewModels;

namespace FUForum.WebPortal.Models
{
    public class SearchKnowledgeBaseViewModel
    {
        public Pagination<KnowledgeBaseQuickVM> Data { set; get; }

        public string Keyword { set; get; }
    }
}
