using FUForum.ViewModels.Contents;

namespace FUForum.WebPortal.Services
{
    public interface ICategoryApiClient
    {
        Task<List<CategoryVM>> GetCategories();

        Task<CategoryVM> GetCategoryById(int id);
    }
}
