using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyRealWorld.Models;
using MyRealWorld.ViewModels;
using MyRealWorld.ViewModels.Programming;

namespace MyRealWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View(null );
        }
        [HttpPost]
        public IActionResult IndexNext(string clientScreenWidth, string clientScreenHeight)
        {
            MainVM vm = new MainVM(Convert.ToInt32(clientScreenWidth), Convert.ToInt32(clientScreenHeight));
            return View("~/Views/Home/IndexNextView.cshtml",vm);
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
        [HttpPost]
        public async Task<JsonResult> UpdateLine(string sline)
        {
            VertLineVM l = new VertLineVM();
            await l.UpdatLineData(sline);
            return Json(l.LineData);
        }
        public IActionResult Programming()
        {
            ProgrammingVM model = new ProgrammingVM();
            return View("~/Views/Programming/ProgrammingView.cshtml", model);
        }

    }
}
