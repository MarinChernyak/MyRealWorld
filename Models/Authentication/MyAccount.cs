using MyRealWorld.Common;
using MyRealWorld.Models.Geo;
using SMGeneralEntities;
using System.ComponentModel;

namespace MyRealWorld.ModelsAuthentication
{
    public class MyAccount : MUser
    {
        [DisplayName("My points")]   
        public int NumPoints { get; set; }

        [DisplayName("My Role")]
        public string Role { get; set; }
         public int RoleId { get; set; }

       
        public MyAccount()
        {

        }
        public MyAccount(string username)
        {
            UserName = username;
            InitData();
        }
        protected void InitData()
        {
            EncryptDataUpdater updater = new EncryptDataUpdater();
            MUser muser = updater.GetDecryptedUser(UserId);
            FirstName = muser.FirstName;
            MidleName = muser.MidleName;
            Password = muser.Password;
            LastName = muser.LastName;
            Email = muser.Email;
            UserId = muser.UserId;
            IsActive = muser.IsActive;
            Sex = muser.Sex;
            Dob = muser.Dob;
            ActivationDate = muser.ActivationDate;
           
            using (SMGeneralContext context = new SMGeneralContext())
            {
                var uar = context.UserAppRoles.Join(context.Roles, uappr => uappr.RoleId, r => r.RoleId,
                (uappr, r) => new { Uapr = uappr, R = r }).Where(z => z.R.AppId == Constants.Values.ApplicationId && z.Uapr.UserId == UserId).FirstOrDefault();
                if (uar != null)
                {
                    RoleId = uar.R.RoleId;
                    Role = uar.R.RoleName;
                }
            }
        }    
        public bool SaveData()
        {
            bool bRez = false;
            EncryptDataUpdater updater = new EncryptDataUpdater();
            MUser muser = updater.GetDecryptedUser(UserId);
            muser.FirstName= FirstName;
            muser.MidleName = MidleName;
            muser.LastName= LastName;
            muser.Email= Email;
            muser.Password = Password;
            muser.UserId = UserId;
            muser.IsActive= IsActive;
            muser.Sex= Sex;
            muser.Dob= Dob;
            muser.ActivationDate= ActivationDate;
            muser.CountryId = CountryId;
            muser.StateId = StateId;
            muser.PlaceId = PlaceId;
            bRez = updater.SetEncryptedUser(muser);

            return bRez;
        }

    }
}
