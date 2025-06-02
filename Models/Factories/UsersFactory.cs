using MyRealWorld.Common;
using MyRealWorld.Models;
using MyRealWorld.ModelsAuthentication;
using NostralogiaDAL.SMGeneralEntities;

namespace MyRealWorld.ModelsFactories
{
    public class UsersFactory
    {
        public static List<MUserBase> GetUsersCollection()
        {
            List<User> users = null;
            using (var context = new SMGeneralContext())
            {
                if (context != null)
                    users = context.Users.ToList();
            }

            return ModelsTransformer.TransferModelList<User, MUserBase>(users);
        }
        public static int GetUserRightData(int UserId, int PersonalDataId )
        {
            int Rights=0;
            //User user = null;
            //Person person = null;
            //int? idContributor = 0;
            //int UserLevel = GetUserLevel(UserId);
            //int DataType = 0;
            //try
            //{
            //    using (var context = new SMGeneralContext())
            //    {
            //        if (context != null)
            //            user = context.Users.FirstOrDefault(x=>x.Id== UserId);
            //    }
            //    using (NostradamusContext context = new NostradamusContext())
            //    {
            //        person = context.People.FirstOrDefault(x => x.Id == PersonalDataId);
            //        idContributor = person.IdContributor;
            //        DataType = person.DataType;
            //    }
            //    Rights = CommonFunctionsFactory.GetRights(user.Id, person.IdContributor ?? 0, UserLevel, DataType);

            //}
            //catch (Exception e)
            //{
            //    LogMaster lm = new LogMaster();
            //    lm.SetLog(e.Message);
            //}
            return Rights;
        }
        public static int GetUserLevel(int UserId)
        {
            int level = 0;
            using (SMGeneralContext _context = new SMGeneralContext())
            {
                User user = _context.Users.FirstOrDefault(x => x.Id == UserId);
                var vlevel = _context.UserAppRoles.Join(_context.Roles, uappr => uappr.RoleId, r => r.RoleId,
                    (uappr, r) => new { Uapr = uappr, R = r }).Where(z => z.R.AppId == Constants.Values.ApplicationId && z.Uapr.UserId == user.Id).FirstOrDefault();
                if (vlevel != null)
                {
                    level = vlevel.R.AccessLevel;
                }
            }
            return level;
        }
 
    }
}
