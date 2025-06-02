using MyRealWorld.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.ModelsFactories
{
    public class CommonFunctionsFactory
    {
        public static int GetRights(int UserId, int idContributor, int UserLevel, int DataType)
        {
            int Rights = 0;
            if (UserLevel >= DataType || UserId == idContributor || DataType==Constants.DataTypes.DataPublic)
            {
                Rights = Constants.Values.CAN_VIEW;
            }
            if (UserId == idContributor || UserLevel >= Constants.Values.RESEARCHER_RIGHTS)
            {
                Rights += Constants.Values.CAN_ADD;
            }
            if (UserId == idContributor || UserLevel >= Constants.Values.TEAMLEAD_RIGHTS)
            {
                Rights += Constants.Values.CAN_EDIT + Constants.Values.CAN_DEACTIVATE;
            }
            if (UserLevel == Constants.Values.ADMIN_RIGHTS)
            {
                Rights += Constants.Values.CAN_DELETE;
            }
            return Rights;
        }
        public static bool IsReadOnly(int UserId, int idContributor, int UserLevel, int DataType)
        {
            int rights = GetRights(UserId, idContributor, UserLevel, DataType);
            return !((rights & Constants.Values.CAN_EDIT) > 0);
        }
    }
}
