using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.Common
{
    public class Constants
    {
        public class SessionCoockies
        {
            public static string SessionID { get { return "MRW_SESSION_ID"; } }
            public static string SessionUName { get { return "MRW_SESSION_UserName"; } }
            public static string SessionUID { get { return "MRW_SESSION_UserID"; } }
            public static string SessionULevel { get { return "MRW_SESSION_UserLevel"; } }

            public static string CoockieToken { get { return "MRW_COCKIE_Token"; } }
            public static string CoockieUserLevel { get { return "MRW_COCKIE_ULevel"; } }

        }
        public class Connections
        {
            public const string ConnectionMain = "ConnectionMain";
            public const string ConnectionGeo = "ConnectionGeo";
            public const string ConnectionSMGeneral = "ConnectionSMGeneral";

            public const string EmailFrom = "nostralogia@gmail.com";
            public const string PassFrom = "Shofar8385";


        }
        public class Values
        {
            public const int ApplicationId = 16;
            public const int PassCodeLength = 12;
            public const int SaltLength = 10;
            public const string ItemValue = "Value";
            public const string ItemText = "Text";

            public const int CAN_VIEW = 1;
            public const int CAN_EDIT = 2; 
            public const int CAN_ADD = 4;
            public const int CAN_DEACTIVATE = 8;
            public const int CAN_DELETE = 16;

            public const int VOLUNTIER_RIGHTS = 1;
            public const int RESEARCHER_RIGHTS = 2;
            public const int TEAMLEAD_RIGHTS = 3;
            public const int ADMIN_RIGHTS = 4;
            public static DateTime DummyDate { get { return new DateTime(1800, 1, 1); } }
            public const string ZeroStringComboText = "Select...";
            public const string ZeroStringComboValue = "-1";
            protected static int _counter = 0;
            public static int Counter { get { return _counter++; } }
            public static string MarkerEdit { get { return "[EDIT]"; } }            
            public static string MarkerDelete { get { return "[DELETE]"; } }
            public static string MarkerDeactivate { get { return "[DEACTIVATE]"; } }
        }
        public class Paths
        {
            public static string ImageRepository { get { return "~/images/ProjectsImages";  } }
            public static string ImageRepositoryRoot { get { return "wwwroot\\images\\ProjectsImages"; } }
        }
        public class DataTypes
        {
            public const int DataPublic = 1;
        }

    }
}
