using MyRealWorld.Models;
using MyRealWorld.Models.Utilities;
using MyRealWorld.ModelsFactories;
using MyRealWorld.Utilities;
using NostralogiaDAL.SMGeneralEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.ModelsAuthentication
{
    public class EncryptDataUpdater :MUserBase
    { 
        public EncryptDataUpdater()
        {

        }
        public bool CheckToken(string token)
        {
            bool bRez = false;
            string newtoken = string.Empty;
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                User user = _context.Users.FirstOrDefault(x => x.Token == token);
                if (user != null)
                {
                    UserLevel = UsersFactory.GetUserLevel(user.Id);
                    UserName =  user.UserName;
                    UserId = user.Id;
                    bRez = true;
                }
            }

            return bRez;
        }
        public string SetToken(int userId)
        {
            
            string token  = Guid.NewGuid().ToString();
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                User user = _context.Users.Where(x => x.Id==userId).First();
                if (user != null)
                {
                    user.Token = token;
                    _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            return token;
        }
        public string EncryptStringVal(int userId, string strtoencrypt)
        {
            string encryptstr = string.Empty;
            try
            {
                using (SMGeneralContext _context = new SMGeneralContext())
                {
                    var secprot = _context.SecurityProtocols.FirstOrDefault(x => x.UserId == userId);
                    if (secprot != null)
                    {
                        string salt = secprot.Salt;
                        string passcode = secprot.Passcode;

                        RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                        encryptstr = encryptor.Encrypt(strtoencrypt);
                    }
                }
            }
            catch
            {

            }

            return encryptstr;
        }
        public string DecryptStringVal(int userId, string encryptedstr)
        {
            string decryptstr = string.Empty;
            try
            {
                using (SMGeneralContext _context = new SMGeneralContext())
                {
                    var secprot = _context.SecurityProtocols.FirstOrDefault(x=>x.UserId==userId);
                    if (secprot != null)
                    {
                        string salt = secprot.Salt;
                        string passcode = secprot.Passcode;

                        RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                        decryptstr = encryptor.Decrypt(encryptedstr);
                    }
                }
            }
            catch
            {

            }
            return decryptstr;
        }
        public Dictionary<string, string> DecryptMapStrings(int userId, Dictionary<string,string> map)
        {
            Dictionary<string, string> mapout = new Dictionary<string, string>();
           
            try
            {
                using (SMGeneralContext _context = new SMGeneralContext())
                {
                    var secprot = _context.SecurityProtocols.FirstOrDefault(x=>x.UserId==userId);
                    if (secprot != null)
                    {
                        string salt = secprot.Salt;
                        string passcode = secprot.Passcode;

                        RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                        foreach(string key in map.Keys)
                        {
                            mapout[key] = encryptor.Decrypt(map[key]);
                        }
                    }
                }
            }
            catch
            {

            }
            return mapout;
        }
        public Dictionary<string, string> EncryptMapStrings(int userId, Dictionary<string, string> map)
        {
            Dictionary<string, string> mapout = new Dictionary<string, string>();

            try
            {
                using (SMGeneralContext _context = new SMGeneralContext())
                {
                    var secprot = _context.SecurityProtocols.FirstOrDefault(x=>x.UserId==userId);
                    if (secprot != null)
                    {
                        string salt = secprot.Salt;
                        string passcode = secprot.Passcode;

                        RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                        foreach (string key in map.Keys)
                        {
                            mapout[key] = encryptor.Encrypt(map[key]);
                        }
                    }
                }
            }
            catch
            {

            }
            return mapout;
        }
        public void UpdateEncryptedData(int userId)
        {
            int SaltLength = 10;
            int PascodeLength = 12;
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                var secprot = _context.SecurityProtocols.FirstOrDefault(x=>x.UserId==userId);
                if (secprot != null)
                {
                    User user = _context.Users.FirstOrDefault(x=>x.Id==userId);
                    string salt = secprot.Salt;
                    string passcode = secprot.Passcode;

                    RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                    string password = encryptor.Decrypt(user.Password);
                    string FName = encryptor.Decrypt(user.FirstName);
                    string email = encryptor.Decrypt(user.Email);
                    string LName = encryptor.Decrypt(user.LastName);
                    string MName = encryptor.Decrypt(user.MidleName);

                    StringGenerator strgen = new StringGenerator(SaltLength);
                    salt = strgen.GenericString;
                    strgen = new StringGenerator(PascodeLength);
                    strgen.Generate();
                    passcode = strgen.GenericString;


                    encryptor = new RijndaelEncryptor(salt, passcode);
                    user.FirstName = encryptor.Encrypt(FName);
                    user.MidleName = encryptor.Encrypt(MName);
                    user.LastName = encryptor.Encrypt(LName);
                    user.Email = encryptor.Encrypt(email);
                    user.Password = encryptor.Encrypt(password);

                    secprot.Passcode = passcode;
                    secprot.Salt = salt;
                    using (var dbContextTransaction = _context.Database.BeginTransaction())
                    {
                        _context.Entry(secprot).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        dbContextTransaction.Commit();
                    }

                }
            }

        }

        public MUser GetDecryptedUser(int userId)
        {
            MUser model = null;
            try
            {
                using (SMGeneralContext context = new SMGeneralContext())
                {
                    var vsecprot = context.SecurityProtocols.FirstOrDefault(x=>x.UserId==userId);
                    if (vsecprot != null)
                    {
                        User user = context.Users.FirstOrDefault(x => x.Id ==userId);
                        string salt = vsecprot.Salt;
                        string passcode = vsecprot.Passcode;

                        RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                        user.Password = encryptor.Decrypt(user.Password);
                        user.FirstName = encryptor.Decrypt(user.FirstName);
                        user.Email = encryptor.Decrypt(user.Email);
                        user.LastName = encryptor.Decrypt(user.LastName);
                        user.MidleName = encryptor.Decrypt(user.MidleName);
                        model = ModelsTransformer.TransferModel<User, MUser>(user);                        
                    }
                }
            }
            catch (Exception e)
            {
                LogMaster lm = new LogMaster();
                lm.SetLog(e.Message);

            }
            return model;
        }
        public bool SetEncryptedUser(MUser muser)
        {
            bool bRez = false;
            string salt = string.Empty;
            string passcode = string.Empty;
            GetSaltPasscode(out salt, out passcode);
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                try
                {
                    RijndaelEncryptor encryptor = new RijndaelEncryptor(salt, passcode);
                    muser.FirstName = encryptor.Encrypt(muser.FirstName);
                    muser.MidleName = encryptor.Encrypt(muser.MidleName);
                    muser.LastName = encryptor.Encrypt(muser.LastName);
                    muser.Email = encryptor.Encrypt(muser.Email);
                    muser.Password = encryptor.Encrypt(muser.Password);

                    User user = ModelsTransformer.TransferModel<MUser, User>(muser);
                    bool bExists = _context.SecurityProtocols.Any(x => x.UserId == muser.UserId);

                    using (var dbContextTransaction = _context.Database.BeginTransaction())
                    {
                        if (!bExists)
                        {
                            SecurityProtocol sp = new SecurityProtocol()
                            {
                                Passcode = passcode,
                                Salt = salt,
                                UserName = muser.UserName,
                                DateCreation = DateTime.Now
                            };
                            _context.SecurityProtocols.Add(sp);
                        }
                        else
                        {
                            var secprot = _context.SecurityProtocols.Where(x => x.UserName == muser.UserName).OrderByDescending(x => x.Id).First();
                            secprot.Salt = salt;
                            secprot.Passcode = passcode;
                            _context.Entry(secprot).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        }

                        _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.Users.Update(user);
                        _context.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    bRez = true;
                }
                catch (Exception e)
                {
                    LogMaster lm = new LogMaster();
                    lm.SetLog(e.Message);
                }
            }

            return bRez;
        }


    }
}
