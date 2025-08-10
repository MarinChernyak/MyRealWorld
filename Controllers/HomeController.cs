using Microsoft.AspNetCore.Mvc;
using MyRealWorld.Common;
using MyRealWorld.Helpers;
using MyRealWorld.Models;
using MyRealWorld.ViewModels;
using MyRealWorld.ViewModels.Programming;
using SMAuthentication.Factories;
using System.Diagnostics;

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
            string token = CoockiesHelper.GetCockie(HttpContext, Constants.SessionCoockies.CoockieToken);
            if (!string.IsNullOrEmpty(token))
            {

                var muser = UsersFactoryHelpers.CheckToken(token, Constants.Values.ApplicationId);
                if (muser != null)
                {
                    token = UsersFactoryHelpers.SetToken(muser.Id);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUName, muser.UserName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionULevel, muser.UserAccessLevel.ToString());
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUID, muser.Id.ToString());

                    CoockiesHelper.SetCockie(HttpContext, Constants.SessionCoockies.CoockieToken, token);
                }
                else
                {
                    CoockiesHelper.DeleteCockie(HttpContext, Constants.SessionCoockies.CoockieToken);
                }
            }
            return View(null);
        }
        [HttpPost]
        public IActionResult IndexNext(string clientScreenWidth, string clientScreenHeight)
        {
            MainVM vm = new MainVM(Convert.ToInt32(clientScreenWidth), Convert.ToInt32(clientScreenHeight));
            return View("~/Views/Home/IndexNextView.cshtml", vm);
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
        [HttpPost]
        public IActionResult Reload(int width, int height)
        {
            MainVM vm = new MainVM(Convert.ToInt32(width), Convert.ToInt32(height));
            return View("~/Views/Home/IndexNextView.cshtml", vm);
        }
    }
}
