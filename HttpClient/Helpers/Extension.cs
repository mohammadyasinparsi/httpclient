using System.Collections.Generic;

namespace HttpClient.Helpers
{
    public static class Extension
    {
        public static List<KeyValuePair<string, string>> GetProperties(this object me)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            foreach (var property in me.GetType().GetProperties())
            {
                result.Add(new KeyValuePair<string, string>(property.Name, property.GetValue(me).ToString()));
            }
            return result;
        }
         

    }
}
