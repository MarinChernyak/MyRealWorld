using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MyRealWorld.Common;
using MyRealWorld.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MyRealWorld.Models.Factories
{
    public class BaseFactory
    {

        public static void InsertSelectItem(List<SelectListItem> list)
        {
            if (list != null && list.Count == 0)
            {
                SelectListItem sli = new SelectListItem()
                {
                    Value = Constants.Values.ZeroStringComboValue,
                    Text = Constants.Values.ZeroStringComboText,
                    Selected = true
                };
                list.Insert(0, sli);
            }            
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
                            Value = PropHelper.GetProp(ValProperty, obj)
                        });
                       
                    }
                }
            }
            catch (Exception ex)
            {
                LogMaster lm = new LogMaster();
                lm.SetLogException(GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }



            return lst;

        }
    }
}
