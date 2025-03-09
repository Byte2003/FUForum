using FUForum.ViewModels.Contents;
using FUForum.ViewModels.Systems;
using FUForum.ViewModels;

namespace FUForum.WebPortal.Services
{
    public interface IUserApiClient
    {
        Task<UserVM> GetById(string id);

        Task<Pagination<KnowledgeBaseQuickVM>> GetKnowledgeBasesByUserId(string userId, int pageIndex, int pageSize);
    }
}
