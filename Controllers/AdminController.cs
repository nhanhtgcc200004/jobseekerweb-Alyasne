using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Chart()
        {
            return View();
        }

    }
}
