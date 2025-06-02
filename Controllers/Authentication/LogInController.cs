using Microsoft.AspNetCore.Mvc;
using MyRealWorld.Common;
using MyRealWorld.Helpers;
using MyRealWorld.ModelsAuthentication;

namespace MyRealWorld.Controllers.Authentication
{
    public class LogInController : BaseController
    {
        public IActionResult ReLogIn()
        {
            string token = CoockiesHelper.GetCockie(HttpContext, Constants.SessionCoockies.CoockieToken);
            if (!string.IsNullOrEmpty(token))
            {
                EncryptDataUpdater updater = new EncryptDataUpdater();
                bool bRez = updater.CheckToken(token);
                if (bRez)
                {
                    token = updater.SetToken(updater.UserId);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUName, updater.UserName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionULevel, updater.UserLevel.ToString());
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUID, updater.Id.ToString());

                    CoockiesHelper.SetCockie(HttpContext, Constants.SessionCoockies.CoockieToken, token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    CoockiesHelper.DeleteCockie(HttpContext, Constants.SessionCoockies.CoockieToken);
                }
            }
            LogInModel model = new LogInModel();
            model.ErrorMessage = "Login failed. Please try again.";
            return View("~/Views/Authentication/LogInStandAlone.cshtml", model);
        }
        public ActionResult LogIn()
        {
            LogInModel model = new LogInModel();
            return View("~/Views/Authentication/LogInStandAlone.cshtml", model);
        }
        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            string token = string.Empty;
            bool brez = model.TryLogIn(out token);
            if (!brez)
                return RedirectToAction("ReLogIn");
            else
            {
                if (model.ShouldRemember && !string.IsNullOrEmpty(token))
                {
                    CoockiesHelper.SetCockie(HttpContext, Constants.SessionCoockies.CoockieToken, token);
                    SetSessionVariables(model);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUName, model.UserName);
                SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionULevel, model.UserLevel.ToString());
                SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUID, model.Id.ToString());

                return RedirectToAction("HomePage", "Home");
            }
        }
        public ActionResult Registration()
        {
            RegistrationModel model = new RegistrationModel();
            return View("~/Views/Authentication/RegistrationForm.cshtml", model);
        }
        [HttpPost]
        public ActionResult Registration(RegistrationModel model)
        {

            if (ModelState.IsValid)
            {
                bool UserNameExists = model.UserNameExists();
                bool EmailExists = model.EmailExists();
                if (UserNameExists)
                {
                    ModelState.AddModelError("UserName", "This User name already exists");
                }
                if (EmailExists)
                {
                    ModelState.AddModelError("Email", "This email already exists");
                }

                if (!UserNameExists && !EmailExists)
                {
                    bool brez = model.SaveNewUser();
                    if (brez)
                    {
                        return View("~/Views/Authentication/LogInStandAlone.cshtml", new LogInModel());
                    }
                }

            }

            return View("~/Views/Authentication/RegistrationForm.cshtml", model);
        }
        public ActionResult LogOut()
        {
            CoockiesHelper.DeleteCockie(HttpContext, Constants.SessionCoockies.CoockieToken);
            DeleteSessionVariables();

            return RedirectToAction("HomePage", "Home");
        }

        public ActionResult ForgotPassword()
        {
            MUser model = new MUser();

            return View("~/Views/Authentication/ForgotPassword.cshtml", model);
        }
        [HttpPost]
        public ActionResult ForgotPassword(MUser model)
        {
            bool IsOK = model.ResetPassword();
            if (!IsOK)
            {
                model.ErrorMessage = "Password was not reset! Please try again.";
                return View("~/Views/Authentication/ForgotPassword.cshtml", model);
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult MyAccount()
        {
            string username = SessionHelper.GetObjectFromJson(HttpContext.Session, Constants.SessionCoockies.SessionUName);
            MyAccount model = new MyAccount(username);
            return View("~/Views/Authentication/MyAccount.cshtml", model);
        }
        [HttpPost]
        public ActionResult MyAccount(MyAccount model)
        {
            if (model != null)
            {
                bool bRez = model.SaveData();
                //EncryptDataUpdater updater = new EncryptDataUpdater();
                //bool bRez= updater.SetEncryptedUser(model);
                if (bRez)
                {
                    return RedirectToAction("HomePage", "Home");
                }
            }
            model.ErrorMessage = "Updating data failed. Pleas etry again later.";
            return RedirectToAction("~/Views/Authentication/MyAccount.cshtml", model);
        }
    }
}
