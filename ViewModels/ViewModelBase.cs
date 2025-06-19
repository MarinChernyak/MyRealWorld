using Microsoft.AspNetCore.Mvc.Rendering;
using MyRealWorld.Common;
using MyRealWorld.Models;
using MyRealWorld.Models.Utilities;
using System.Reflection;
using SMUtilities;

namespace MyRealWorld.ViewModels
{
    public class ViewModelBase : BaseModel
    {
        public string ErrorMessage { get; protected set; } = string.Empty;
        public ViewModelBase()
        {

        }
        public int GetUID()
        {
            int uid = 0;
            string suid = _session.GetString(Constants.SessionCoockies.SessionUID);
            if (!string.IsNullOrEmpty(suid))
            {
                uid = Convert.ToInt32(suid);
            }
            return uid;
        }
        public void SetUserId(int UID)
        {
            _session.SetString(Constants.SessionCoockies.SessionULevel, UID.ToString());
        }
        public int GetUserLevel()
        {
            int level = 0;
            if (_session != null)
            {
                string slevel = _session.GetString(Constants.SessionCoockies.SessionULevel);
                if (!string.IsNullOrEmpty(slevel))
                {
                    level = Convert.ToInt32(slevel);
                }
            }
            return level;
        }
        public int GetUserLevel(ISession session)
        {
            _session = session;
            return GetUserLevel();
        }        
        public void SetUserLevel(int level)
        {
            _session.SetString(Constants.SessionCoockies.SessionULevel, level.ToString());
        }
        protected List<SelectListItem> FromLstObjectsToDropDownFeed<T>(List<T> lstObj, string TxtProperty, string ValProperty)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem()
            {
                Text = Constants.Values.ZeroStringComboText,
                Value = Constants.Values.ZeroStringComboValue,
                Selected = true
            });
            try
            {
                if (lstObj != null)
                {
                    foreach (T obj in lstObj)
                    {
                        lst.Add(new SelectListItem()
                        {
                            Text = PropHelper.GetProp(TxtProperty,obj),
                            Value = PropHelper.GetProp(ValProperty, obj),
                        });                        
                    }
                }
            }
            catch (Exception ex)
            {
                LogMaster lm = new LogMaster();
                var curmethod = MethodBase.GetCurrentMethod();
                lm.SetLogException(GetType().Name, curmethod==null? string.Empty: MethodBase.GetCurrentMethod().Name, ex.Message);

            }
            return lst;
        }
    }        
}
