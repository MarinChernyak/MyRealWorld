using SMGeneralEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRealWorld.ModelsAuthentication
{
    public class SMGeneralBaseModel
    {
        public SMGeneralBaseModel()
        {
        }
        protected string StrWraper(string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : str;
        }
    }
}
