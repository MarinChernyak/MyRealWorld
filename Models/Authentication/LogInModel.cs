using Authentication.Factories;
using MyRealWorld.ModelsAuthentication;
using MyRealWorld.ModelsFactories;
using SMAuthentication.Factories;
using SMGeneralEntities;
using System.ComponentModel;

namespace MyRealWorld.Models.Authentication
{
    public class LogInModel : MUserBase
    {
        [DisplayName("Remember me")]
        public bool ShouldRemember { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;


        public LogInModel()
        {

        }
        public LogInModel(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                EncryptDataUpdater updater = new EncryptDataUpdater();
                bool brez = updater.CheckToken(token);
                if (brez)
                {
                    UserId = updater.UserId;
                    UserLevel = updater.UserLevel;
                    UserName = updater.UserName;
                }
            }
        }
        public bool LogIn()
        {
            bool brez = false;
            //string token = CoockiesHelper.GetCockie(context, Constants.SessionCoockies.CoockieToken);
            //using (SMGeneralContext _context = new SMGeneralContext())
            //{
            //    User user = _context.Users.FirstOrDefault(x => x.Token == token);
            //    if (user != null)
            //    {
            //        UserName = user.UserName;
            //        EncryptDataUpdater datapdater = new EncryptDataUpdater();
            //        token = datapdater.SetToken(user.Id);
            //        CoockiesHelper.SetCockie(context, Constants.SessionCoockies.CoockieToken, token);
            //    }
            //}

            return brez;
        }
        public UserLogIn TryLogIn()
        {           
            return UsersFactory.GetTryLogIn(UserName, Password, ShouldRemember);
        }
    }
}
