using SMUtilities;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MyRealWorld.Models
{
    public class ModelsTransformer
    {
        protected static object ChangeType(object value, Type conversion)
        {
            var t = conversion;
            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                t = Nullable.GetUnderlyingType(t);
            }
            return Convert.ChangeType(value, t);
        }
        protected static void SetValue<T>(T obj, string propertyName, object value)
        {
            // these should be cached if possible
            Type type = typeof(T);
            PropertyInfo pi = type.GetProperty(propertyName);
            if (pi != null)
            {
                Type pt = pi.PropertyType;
                //pi.SetValue(obj, Convert.ChangeType(value, pi.PropertyType), null);
                pi.SetValue(obj, ChangeType(value, pt), null);
            }
        }
        public static TTo TransferModel<TFrom, TTo>(TFrom modelfrom)
        {
            TTo modelTo = (TTo)Activator.CreateInstance(typeof(TTo));
            try
            {
                Type type = modelfrom.GetType();
                PropertyInfo[] propertiesFrom = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo p in propertiesFrom)
                {
                    if (!p.CanRead || !p.CanWrite) continue;
                    if (p.PropertyType.Name.Contains("ICollection") || p.PropertyType.Name.Contains("HashSet")) continue;
                    var value = p.GetValue(modelfrom);
                    if (value == null) continue;

                    SetValue(modelTo, p.Name, value);
                }
            }
            catch(Exception e)
            {
                LogMaster lm = new LogMaster();
                lm.SetLog(e.Message);
            }
            return modelTo;
        }
        public static List<TTo> TransferModelList<TFrom, TTo>(List<TFrom> lstfrom, bool IgnoreNestedTypes = false)
        {
            List<TTo> lstTo = (List<TTo>)Activator.CreateInstance(typeof(List<TTo>));
            foreach (TFrom f in lstfrom)
            {
                TTo model = TransferModel<TFrom, TTo>(f);
                lstTo.Add(model);
            }
            return lstTo;
        }
        public static HashSet<TTo> TransferModelList<TFrom, TTo>(HashSet<TFrom> lstfrom, bool IgnoreNestedTypes = false)
        {
            HashSet<TTo> lstTo = (HashSet<TTo>)Activator.CreateInstance(typeof(HashSet<TTo>));
            foreach (TFrom f in lstfrom)
            {
                TTo model = TransferModel<TFrom, TTo>(f);
                lstTo.Add(model);
            }
            return lstTo;
        }
        public static void TransferModelList<TFrom, TTo>(List<TFrom> lstfrom, ObservableCollection<TTo> modelto)
        {
            foreach (TFrom f in lstfrom)
            {
                TTo model = TransferModel<TFrom, TTo>(f);
                modelto.Add(model);
            }
        }        
    }
}
