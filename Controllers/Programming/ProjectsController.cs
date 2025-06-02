using Microsoft.AspNetCore.Mvc;

namespace MyRealWorld.Controllers.Programming
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
