using Microsoft.AspNetCore.Mvc;

namespace Task.Web.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
