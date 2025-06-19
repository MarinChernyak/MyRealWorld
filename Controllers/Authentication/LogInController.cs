using Authentication.Factories;
using Authentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyRealWorld.Common;
using MyRealWorld.Helpers;
using MyRealWorld.Models.Authentication;
using MyRealWorld.ModelsAuthentication;
using SMAuthentication.Authentication;
using SMAuthentication.Factories;
using System.Security.Cryptography;
using System.Text;
namespace MyRealWorld.Controllers.Authentication
{
    public class LogInController : BaseController
    {

        public IActionResult ReLogIn()
        {
            string token = CoockiesHelper.GetCockie(HttpContext, Constants.SessionCoockies.CoockieToken);
            if (!string.IsNullOrEmpty(token))
            {
                
                var muser = UsersFactoryHelpers.CheckToken(token,Constants.Values.ApplicationId);
                if (muser!=null)
                {
                    token = UsersFactoryHelpers.SetToken(muser);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUName, muser.UserName);
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionULevel, muser.UserAccessLevel.ToString());
                    SessionHelper.SetObjectAsJson(HttpContext.Session, Constants.SessionCoockies.SessionUID, muser.Id.ToString());

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
            int err = model.TryLogIn();
            if(err== SMAuthentication.Constants.ErrorsCodes.ErrorInvalidPassword)
            {
                model.ErrorMessage = "Login failed. Please try again.";
                return View("~/Views/Authentication/LogInStandAlone.cshtml", model);
            }
            else if (err== SMAuthentication.Constants.ErrorsCodes.ErrorSecurityProtocolDoesNotExist)                    
            {
                model.ResetPassword();
                return RedirectToAction("ResetPassword", new {id=model.Id, errCode = err });
            }
            else if (err == SMAuthentication.Constants.ErrorsCodes.NoError)
            {
                if (model.ShouldRemember && !string.IsNullOrEmpty(token))
                {
                    CoockiesHelper.SetCockie(HttpContext, Constants.SessionCoockies.CoockieToken, token);
                    SetSessionVariables(model);
                }
                SetSessionVariables(model);
                return RedirectToAction("Index", "Home");
            }
            else if(err == SMAuthentication.Constants.ErrorsCodes.ErrorResetPasswordRequired)
            {
                LogInModel limodel = new LogInModel();
                limodel.ErrorMessage = "Please log in with a password, which has been sent to you by email";
                return View("~/Views/Authentication/LogInStandAlone.cshtml", limodel);
            }
            else
                return RedirectToAction("ReLogIn");
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

        public ActionResult ResetPassword(int id, int err = SMAuthentication.Constants.ErrorsCodes.NoError)
        {
            MUserVM model = new MUserVM(id, err);

            return View("~/Views/Authentication/ResetPassword.cshtml", model);
        }
        [HttpPost]
        public ActionResult ResetPassword(MUserVM model)
        {
            bool IsOK = model.ResetPassword();
            if (!IsOK)
            {
                model.ErrMessage = "Password was not reset! Please try again.";
                return View("~/Views/Authentication/ResetPassword.cshtml", model);
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult MyAccount()
        {
            string username = SessionHelper.GetObjectFromJson(HttpContext.Session, Constants.SessionCoockies.SessionUName);
            MyAccount model = new MyAccount(username, Constants.Values.ApplicationId);
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
            model.ErrMessage = "Updating data failed. Pleas etry again later.";
            return RedirectToAction("~/Views/Authentication/MyAccount.cshtml", model);
        }
    }
}
