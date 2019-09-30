using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps.Bases;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class PlatoOrderClassMap : DomainClassMap<PlatoOrderClass>
    {
        public override void Map(BsonClassMap<PlatoOrderClass> cm)
        {
            cm.AutoMap();
        }
    }
}
