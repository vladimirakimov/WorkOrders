using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace ITG.Brix.WorkOrders.API.Context.Providers.Impl
{
    public class JsonProvider : IJsonProvider
    {
        public IDictionary<string, object> ToDictionary(string json)
        {
            JObject jObject;
            using (var reader = new JsonTextReader(new StringReader(json)) { DateParseHandling = DateParseHandling.None })
            {
                jObject = JObject.Load(reader);
            }
            var result = jObject.ToObject<Dictionary<string, object>>();
            return result;
        }
    }
}
