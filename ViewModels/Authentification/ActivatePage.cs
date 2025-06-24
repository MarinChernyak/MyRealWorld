using Authentication.Models;
using MyRealWorld.Common;
using SMAuthentication.Factories;

namespace MyRealWorld.ViewModels.Authentification
{
    public class ActivatePage : ViewModelBase
    {
        protected string UserName { get; set; }
        protected bool _activate { get; set; }
        public ActivatePage(string username, bool activate)
        {
            UserName = username;
            _activate = activate;
            Activate();
        }
        protected bool Activate()
        {
            bool bActivated = false;
            bActivated=UsersFactoryHelpers.ActivateUser(UserName, _activate);
            if (bActivated)
            {
                Message = "Your acoount has been activated. Please Log In";
                classMsg = "InfoMsg";
            }
            else
            {
                Message = "Your acoount has been NOT activated. Please ask an admion about details";
                classMsg = "ErrMsg";
            }
                return bActivated;
        }
    }
}
