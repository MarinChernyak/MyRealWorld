using Authentication.Main;
using Authentication.Models;
using MyRealWorld.Common;
using NuGet.Common;
using SMAuthentication.Factories;
using SMCommonUtilities;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyRealWorld.Models.Authentication
{
    public class LogInModel : MUser
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
                
                var muser = UsersFactoryHelpers.CheckToken(token, Constants.Values.ApplicationId);
                if (muser!=null)
                {
                    Id = muser.Id;
                    UserAccessLevel = muser.UserAccessLevel;
                    UserName = muser.UserName;
                }
            }
        }
        public StrResponse TryLogIn()
        {     
            int err = 0;
            Authenticator auth = new Authenticator(UserName, Password, Constants.Values.ApplicationId);
            StrResponse response =  auth.Authenticate();
            string sid = response.GetValueByName(SMAuthentication.Constants.Values.UserId);
            
            if (!string.IsNullOrEmpty(sid))
            {
                Id = Convert.ToInt32(sid);
                response.AddValue(SMAuthentication.Constants.Values.UserId, sid);
            }
            if (response.ErrCode == SMAuthentication.Constants.ErrorsCodes.ErrorResetPasswordRequired)
            {
                err = response.ErrCode;
                string email = response.GetValueByName(SMAuthentication.Constants.Values.UserEmail);
                if (!string.IsNullOrEmpty(email) && !EmailNewPassword(email))
                {
                    err = Constants.ErrorCodes.Error_SMTP_Problem;
                    auth.DeleteSecurityProtocol(UserName);
                    response.ClearValues();                }
            }
            else if(response.ErrCode == SMAuthentication.Constants.ErrorsCodes.NoError)
            {
                string token = response.GetValueByName(SMAuthentication.Constants.Values.UserToken);
                if(string.IsNullOrEmpty(token) && ShouldRemember )
                {
                    token = UsersFactoryHelpers.SetToken(Id);
                    response.AddValue(SMAuthentication.Constants.Values.UserToken, token);
                }
                string accessLevel = response.GetValueByName(SMAuthentication.Constants.Values.UserLevel);
                response.AddValue(SMAuthentication.Constants.Values.UserLevel, accessLevel);
            }

            return response;

        }
        protected bool EmailNewPassword(string new_pass)
        {
            bool bIsOK = false;
            if (string.IsNullOrEmpty(Email))
            {
                StrResponse strr = UsersFactoryHelpers.GetUserEmail(UserName);
                if (strr!=null && strr.ErrCode== SMAuthentication.Constants.ErrorsCodes.NoError)
                {
                    Email = strr.GetValueByName(SMAuthentication.Constants.Values.UserEmail);
                }
            }
            if (!string.IsNullOrEmpty(new_pass) && !string.IsNullOrEmpty(Email))//new password
            {

                try
                {
                    StringBuilder body = new StringBuilder();
                    body.AppendLine("<h1>MyReal World informs you</h1>");
                    body.AppendLine($"<p>Dear {UserName}!</p>");
                    body.AppendLine($"<p>Currently your password reset and a new one is - <b>{new_pass}<b> </p>");
                    body.AppendLine($"<p>Please change it as soon as you can</p>");
                    using (MailMessage msg = new MailMessage())
                    {

                        msg.From = new MailAddress(Common.Constants.Connections.EmailFrom);
                        msg.To.Add(Email);
                        msg.Subject = "Password Changed";
                        msg.Body = body.ToString(); ;
                        //msg.Priority = MailPriority.High;
                        msg.IsBodyHtml = true;

                        using (SmtpClient client = new SmtpClient())
                        {
                            client.EnableSsl = true;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential(Common.Constants.Connections.EmailFrom, Common.Constants.Connections.PassFrom);
                            client.Host = "smtp.gmail.com";
                            client.Port = 587;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;

                            client.Send(msg);
                        }

                    }
                    bIsOK = true;
                }
                catch (Exception e)
                {
                    LogMaster lm = new LogMaster();
                    lm.SetLog(e.Message);
                }
            }
            return bIsOK;
        }
        public bool ResetPassword()
        {                    
            bool bIsOK = false;

            if (!string.IsNullOrEmpty(UserName))
            {
                Authenticator auth = new Authenticator(UserName, Password, Common.Constants.Values.ApplicationId);
                StrResponse response = auth.ResetPassword();
                string pass = response.GetValueByName(SMAuthentication.Constants.Values.UserPassword);
                bIsOK = EmailNewPassword(pass);
            }            
            return bIsOK;
        }
    }
}
