using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.Helpers
{
    public class AccessHelper
    {
        public enum eDataType
        {
            PUBLIC=1,
            CONFIDNTIAL,
            SECRET,
            TOP_SECRET
        };
        public static bool CanViewRecord(int UserLevel, int DataType)
        {
            bool canView = false;
            if (DataType == (int)eDataType.PUBLIC)
            {
                canView = true;
            }
            else if(DataType== (int)eDataType.CONFIDNTIAL)
            {

            }

            return canView;
        }
    }
}
