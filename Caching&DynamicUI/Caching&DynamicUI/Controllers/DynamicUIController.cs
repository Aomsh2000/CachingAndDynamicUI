using Microsoft.AspNetCore.Mvc;

namespace Caching_DynamicUI.Controllers
{
    public class DynamicUIController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("DynamicTabs");
        }
    }
}
