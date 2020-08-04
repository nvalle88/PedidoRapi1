using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Saludsa.Utilidades.Extensores
{
    public static class ObjectExtension
    {
        public static object Serializar(this object str)
        {
            return str == null ? string.Empty : JsonConvert.SerializeObject(str);
        }

        public static string ConvertirQuery(this NameValueCollection nvc)
        {
            if (nvc == null)
                return "?";
            var cadena = (from key in nvc.AllKeys
                          from value in nvc.GetValues(key)
                          select $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlPathEncode(value)}")
                .ToArray();

            return $"?{string.Join("&", cadena)}";
        }
    }
}