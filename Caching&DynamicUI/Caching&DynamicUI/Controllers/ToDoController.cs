using Microsoft.AspNetCore.Mvc;

namespace Caching_DynamicUI.Controllers
{
    public class ToDoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
