using ITG.Brix.WorkOrders.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers.Impl
{
    public class PlatoOrderProvider : IPlatoOrderProvider
    {
        public PlatoOrderOverview GetPlatoOrderOverview(string jsonPlatoOrderOverview)
        {
            JObject jObject;
            using (var reader = new JsonTextReader(new StringReader(jsonPlatoOrderOverview)) { DateParseHandling = DateParseHandling.None })
            {
                jObject = JObject.Load(reader);
            }

            JToken transport = jObject["Transport"];

            var platoOrderOverview = transport.ToObject<PlatoOrderOverview>();
            platoOrderOverview.Source = jObject["Source"].ToObject<string>();

            return platoOrderOverview;
        }

        public PlatoOrderFull GetPlatoOrderFull(string jsonPlatoOrderFull)
        {
            JObject jObject;
            using (var reader = new JsonTextReader(new StringReader(jsonPlatoOrderFull)) { DateParseHandling = DateParseHandling.None })
            {
                jObject = JObject.Load(reader);
            }

            var platoOrderFull = jObject.ToObject<PlatoOrderFull>();

            return platoOrderFull;
        }
    }
}
