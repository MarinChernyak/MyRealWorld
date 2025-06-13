using SMAuthentication.Factories;
using SMGeneralEntities;

namespace MyRealWorld.BL.Authentication
{
    public class AuthenticationBL
    {
        public int AccessLevel { get; protected set; }
        public string UserName { get; protected set; } = string.Empty;
        public int  UserId { get; protected set; }
        public bool CheckToken(string token)
        {
            bool bRez = false;
            string newtoken = string.Empty;
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                User user = _context.Users.FirstOrDefault(x => x.Token == token);
                if (user != null)
                {

                    AccessLevel = UsersFactory.GetUserLevel(user.Id);
                    UserName = user.UserName;
                    UserId = user.Id;
                    bRez = true;
                }
            }

            return bRez;
        }
    }
}
