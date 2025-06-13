using MyRealWorld.Common;
using MyRealWorld.Helpers;
using MyRealWorld.ModelsFactories;
using MyRealWorld.Utilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyRealWorld.ModelsAuthentication
{
    public class MUserBase : SMGeneralBaseModel
    {
        public bool IsActive { get; set; }
        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name cannot be empty")]
        [MaxLength(50, ErrorMessage = "The length of a user  name must not exсeed 50 characters")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "The Password cannot be empty")]
        [MaxLength(50, ErrorMessage = "The length of a user  name must not exсeed 50 characters")]
        public string Password { get; set; }

        public int UserLevel { get; set; }
        public int UserId { get; set; }
        public MUserBase()
        {
            IsActive = true;
        }

        protected void GetSaltPasscode(out string salt, out string passcode)
        {
            salt = string.Empty;
            passcode = string.Empty;

            StringGenerator strgen = new StringGenerator(Constants.Values.SaltLength);
            salt = strgen.GenericString;
            strgen = new StringGenerator(Constants.Values.PassCodeLength);
            strgen.Generate();
            passcode = strgen.GenericString;
        }

    }
   
}
