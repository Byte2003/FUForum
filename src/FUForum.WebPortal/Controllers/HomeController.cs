using FUForum.ViewModels.Contents;
using FUForum.WebPortal.Models;
using FUForum.WebPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FUForum.WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IKnowledgeBaseApiClient _knowledgeBaseApiClient;
        private readonly ILabelApiClient _labelApiClient;

        public HomeController(ILogger<HomeController> logger, IKnowledgeBaseApiClient knowledgeBaseApiClient, ILabelApiClient labelApiClient)
        {
            _logger = logger;
            _knowledgeBaseApiClient = knowledgeBaseApiClient;
            _labelApiClient = labelApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var latestKnowledgeBases = await _knowledgeBaseApiClient.GetLatestKnowledgeBases(5);
            var popularKnowledgeBases = await _knowledgeBaseApiClient.GetPopularKnowledgeBases(5);
            var popularLabels = await _labelApiClient.GetPopularLabels(5);

            var viewModel = new HomeViewModel
            {
                LatestKnowledgeBases = latestKnowledgeBases,
                PopularKnowledgeBases = popularKnowledgeBases,
                PopularLabels = popularLabels
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
