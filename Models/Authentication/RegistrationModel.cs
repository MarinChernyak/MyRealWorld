using MyRealWorld.Models.Utilities;
using MyRealWorld.Utilities;
using NostralogiaDAL.SMGeneralEntities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;

namespace MyRealWorld.ModelsAuthentication
{
    public class RegistrationModel : MUser
    {
        [DisplayName("Confirmation Email")]
        [Compare("Email", ErrorMessage = "The email and confirmation do not match.")]
        public string ConfirmEmail { get; set; }

        public bool SaveNewUser()
        {
            bool brez = false;
            StringGenerator strgen = new StringGenerator();
            string salt = strgen.GenericString;
            strgen.Generate();
            string pass = strgen.GenericString;
            strgen = new StringGenerator();
            string vector = strgen.GenericString;


            User user = new User();
            //SMRijndaelEncryption ecryptor = new SMRijndaelEncryption(salt, pass, vector);
            RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, pass);
            try
            {
                UpdateDates();
                user.UserName = UserName;
                user.Password = encryptor.Encrypt(Password);
                user.Email = encryptor.Encrypt(Email);
                user.IsActive = true;
                user.ActivationDate = ActivationDate;
                user.CountryId = CountryId;
                user.StateId = StateId;
                user.PlaceId = PlaceId;
                user.FirstName = encryptor.Encrypt(StrWraper(FirstName));
                user.LastName = encryptor.Encrypt(StrWraper(LastName));
                user.MidleName = encryptor.Encrypt(StrWraper(MidleName));
                user.Dob = Dob;
                user.Sex = (byte?)Sex;
                    
                //Save salt etc...
                SecurityProtocol prot = new SecurityProtocol();
                prot.Passcode = pass;
                prot.Salt = salt;
                prot.UserName = UserName;
                prot.DateCreation = DateTime.Now;
                using (SMGeneralContext context = new SMGeneralContext())
                {
                    using (var dbContextTransaction = context.Database.BeginTransaction())
                    {
                        context.Users.Add(user);
                        context.SecurityProtocols.Add(prot);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                }
                brez = true;
            }
            catch(Exception e)
            {
                LogMaster Log = new LogMaster();
                Log.SetLogException("RegistrationModel", "SaveNewUser", e.Message);
                ErrorMessage = "Registration has failed! Please check log files."; 
            }
            return brez;
        }

        public bool UserNameExists()
        {
            bool bRez = false;
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                bRez= _context.Users.Any(x => x.UserName == UserName);
            }
            return bRez;
        }
        public bool EmailExists()
        {
            bool bRez = false;
            using (SMGeneralContext _context = new SMGeneralContext())
            {

                bRez= _context.Users.Any(x => x.Email == Email);
            }
            return bRez;
        }
    }
}
