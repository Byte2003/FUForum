using FUForum.ViewModels.Contents;
using FUForum.ViewModels;

namespace FUForum.WebPortal.Models
{
    public class ListByTagIdViewModel
    {
        public Pagination<KnowledgeBaseQuickVM> Data { set; get; }

        public LabelVM LabelVm { set; get; }
    }
}
