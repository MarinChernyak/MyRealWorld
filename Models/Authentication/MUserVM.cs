using Authentication.Factories;
using Authentication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyRealWorld.Common;
using SMCommonUtilities;
using SMGeneralEntities;
using SMUtilities;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MyRealWorld.ModelsAuthentication
{
    public class MUserVM : MUser
    {
        public string ErrMessage { get; set; } = string.Empty;
        public string HeaderForgotPass { get; set; } = "Please enter a user name and email";
        public List<SelectListItem> SexCollection { get; protected set; }=new List<SelectListItem>();

        public MUserVM()
        {
        }
        public MUserVM(int id)
        {
            Id= id;
            UpdateDates();
        }
        public MUserVM(int id, int err)
        {
            Id = id;
            if (err==SMAuthentication.Constants.ErrorsCodes.ErrorSecurityProtocolDoesNotExist)
            {
                HeaderForgotPass = "Your password must be reset! Please enter your user name and email.";
            }
            UpdateDates();
        }

        public bool ResetPassword()
        {
            bool bIsOK = false;

            if (!string.IsNullOrEmpty(UserName))
            {
                using (SMGeneralContext _context = new SMGeneralContext())
                {
                    SecurityProtocol prot = _context.SecurityProtocols.FirstOrDefault(x => x.UserId == Id);
                    
                    MUser muser = EncryptionHelper.GetDecryptedUser(Id,AppId);
                    if (!string.IsNullOrEmpty(muser.Email))
                    {
                        StringGenerator gen = new StringGenerator(8, true, false, true, true);
                        string newPass = gen.GenericString;
                        muser.Password = newPass;

                        try
                        {
                            StringBuilder body = new StringBuilder();
                            body.AppendLine("<h1>MyReal World informs you</h1>");
                            body.AppendLine($"<p>Currently your password is - {newPass} </p>");
                            using (MailMessage msg = new MailMessage())
                            {

                                msg.From = new MailAddress(Constants.Connections.EmailFrom);
                                msg.To.Add(muser.Email);
                                msg.Subject = "Password Changed";
                                msg.Body = body.ToString(); ;
                                //msg.Priority = MailPriority.High;
                                msg.IsBodyHtml = true;

                                using (SmtpClient client = new SmtpClient())
                                {
                                    client.EnableSsl = true;
                                    client.UseDefaultCredentials = false;
                                    client.Credentials = new NetworkCredential(Constants.Connections.EmailFrom, Constants.Connections.PassFrom);
                                    client.Host = "smtp.gmail.com";
                                    client.Port = 587;
                                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                                    client.Send(msg);
                                }

                            }
                            bIsOK = EncryptionHelper.EncryptUser(muser);
                        }
                        catch (Exception e)
                        {
                            LogMaster lm = new LogMaster();
                            lm.SetLog(e.Message);
                        }
                    }
                }
                
            }
            else
            {
                ErrMessage = "User with this user name and/or email was not found!";
            }

            return bIsOK;
        }
        protected void UpdateDates()
        {
            Dob = Constants.Values.DummyDate;
            ActivationDate = DateTime.Now;
        }       
    }
}
