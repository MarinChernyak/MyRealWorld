using Authentication.Main;
using Authentication.Models;
using SMAuthentication.Authentication;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Text;
using SMUtilities;
using Authentication.Factories;
using MyRealWorld.Common;
using SMAuthentication.Factories;

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
        public int TryLogIn()
        {     
            int err = 0;
            Authenticator auth = new Authenticator(UserName, Password, Common.Constants.Values.ApplicationId);
            StrResponse response =  auth.Authenticate();
            if (response.ErrCode == SMAuthentication.Constants.ErrorsCodes.ErrorResetPasswordRequired)
            {
                err = response.ErrCode;

                if (!EmailNewPassword(response.ResponseValue))
                {

                    err = Common.Constants.ErrorCodes.Error_SMTP_Problem;
                    auth.DeleteSecurityProtocol(UserName);
                }

            }
            return err;

        }
        protected bool EmailNewPassword(string new_pass)
        {
            bool bIsOK = false;
            if (string.IsNullOrEmpty(Email))
            {
                StrResponse strr = SMAuthentication.Factories.UsersFactoryHelpers.GetUserEmail(UserName);
                if (strr!=null && strr.ErrCode== SMAuthentication.Constants.ErrorsCodes.NoError)
                {
                    Email = strr.ResponseValue;
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
                bIsOK = EmailNewPassword(response.ResponseValue);
            }            
            return bIsOK;
        }
    }
}
