using Microsoft.AspNetCore.Mvc;

namespace Caching_DynamicUI.Controllers
{
    public class ImagesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("ImageGallery");
        }
    }
}
