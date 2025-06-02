using System.Reflection;
namespace MyRealWorld.Models.Utilities
{
    public class PropHelper
    {
        public PropHelper() { }
        public static string GetProp<T>(string prop_name, T obj)
        {
            string prop_txt = string.Empty;
            object propval = null;
            PropertyInfo propobj = typeof(T).GetProperty(prop_name);
            if (propobj != null)
            {
                propval = propobj.GetValue(obj, null);
            }
            if (propval != null)
            {
                prop_txt = propval.ToString();
            }
            return prop_txt;
        }
    }
}
