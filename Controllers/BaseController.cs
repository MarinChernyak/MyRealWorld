using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRealWorld.Common;
using MyRealWorld.Helpers;
using MyRealWorld.Models;
using MyRealWorld.Models.Authentication;
using MyRealWorld.ModelsAuthentication;
using MyRealWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MyRealWorld.Controllers
{
    public class BaseController : Controller
    {
        protected IConfiguration _configuration;
        public BaseController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        protected void UpdateUserData(ViewModelBase model)
        {
            string userid = SessionHelper.GetObjectFromJson(HttpContext.Session, Constants.SessionCoockies.SessionUID);
            if (!string.IsNullOrEmpty(userid))
            {
                model.SetUserId(Convert.ToInt32(userid));
            }
            string userlevel = SessionHelper.GetObjectFromJson(HttpContext.Session, Constants.SessionCoockies.SessionULevel);
            if (!string.IsNullOrEmpty(userlevel))
            {
                model.SetUserLevel(Convert.ToInt32(userlevel));
            }
        }
        protected int GetUserLevel()
        {
            int ilevel = 0;
            string userlevel = SessionHelper.GetObjectFromJson(HttpContext.Session, Constants.SessionCoockies.SessionULevel);
            if(!string.IsNullOrEmpty(userlevel))
            {
                ilevel = Convert.ToInt32(userlevel);
            }
            return ilevel;
        }
        protected void SetSessionVariables(LogInModel model)
        {
            HttpContext.Session.SetString(Constants.SessionCoockies.SessionUName, model.UserName);
            HttpContext.Session.SetString(Constants.SessionCoockies.SessionUID, model.Id.ToString());
            HttpContext.Session.SetString(Constants.SessionCoockies.SessionULevel, model.UserAccessLevel.ToString());
           
        }
        protected void DeleteSessionVariables()
        {
            HttpContext.Session.Remove(Constants.SessionCoockies.SessionUName);
            HttpContext.Session.Remove(Constants.SessionCoockies.SessionULevel);
            HttpContext.Session.Remove(Constants.SessionCoockies.SessionUID);
        }
        protected int GetUserId()
        {
            int id = 0;
            string sid = HttpContext.Session.GetString(Constants.SessionCoockies.SessionUID);
            if (!string.IsNullOrEmpty(sid))
            {
                bool rez = int.TryParse(sid, out id);              
            }
            return id;
        }
    }
}
