using Microsoft.AspNetCore.Mvc;

namespace finalyearproject.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
