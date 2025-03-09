using FUForum.ViewModels.Contents;
using FUForum.ViewModels;

namespace FUForum.WebPortal.Models
{
    public class ListByCategoryIdViewModel
    {
        public Pagination<KnowledgeBaseQuickVM> Data { set; get; }

        public CategoryVM Category { set; get; }
    }
}
