using Newtonsoft.Json;

namespace CMS.Utilities
{
    // Used to define Extension methods for ISession
    public static class SessionExtensions
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData is null ? default : JsonConvert.DeserializeObject<T>(sessionData);
        }
    }
}