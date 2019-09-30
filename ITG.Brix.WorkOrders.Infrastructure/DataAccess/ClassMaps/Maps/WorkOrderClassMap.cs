using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps.Bases;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using MongoDB.Bson.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps.Maps
{
    public class WorkOrderClassMap : DomainClassMap<WorkOrderClass>
    {
        public override void Map(BsonClassMap<WorkOrderClass> cm)
        {
            cm.AutoMap();
        }
    }
}
