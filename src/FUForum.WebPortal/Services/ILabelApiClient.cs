using FUForum.ViewModels.Contents;

namespace FUForum.WebPortal.Services
{
    public interface ILabelApiClient
    {
        Task<List<LabelVM>> GetPopularLabels(int take);

        Task<LabelVM> GetLabelById(string labelId);
    }
}
