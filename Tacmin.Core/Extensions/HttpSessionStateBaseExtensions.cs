using System.Web;
using System.Web.SessionState;

namespace Tacmin.Core.Extensions
{
    public static class HttpSessionStateBaseExtensions
    {
        public static bool TryGet<T>(this HttpSessionStateBase session, string key, out T value)
        {
            value = default(T);

            var obj = session[key];

            if (obj != null)
            {
                value = (T)obj;
                return true;
            }

            return false;
        }

        public static T Get<T>(this HttpSessionStateBase session, string key)
        {
            session.TryGet(key, out T value);

            return value;
        }

        public static void Set(this HttpSessionStateBase session, string key, object value)
        {
            session[key] = value;
        }

        public static bool TryGet<T>(this HttpSessionState session, string key, out T value)
        {
            value = default(T);

            var obj = session[key];

            if (obj != null)
            {
                value = (T)obj;
                return true;
            }

            return false;
        }

        public static T Get<T>(this HttpSessionState session, string key)
        {
            session.TryGet(key, out T value);

            return value;
        }

        public static void Set(this HttpSessionState session, string key, object value)
        {
            session[key] = value;
        }
    }
}
