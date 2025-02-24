using Microsoft.AspNetCore.Mvc;

namespace FUForum.BackendServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
