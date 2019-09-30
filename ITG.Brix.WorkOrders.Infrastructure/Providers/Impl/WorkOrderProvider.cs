using ITG.Brix.WorkOrders.Domain;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers.Impl
{
    public class WorkOrderProvider : IWorkOrderProvider
    {

        public IDictionary<string, string> GetPropertyTypePairs()
        {
            var allProperties = new Dictionary<string, string>();

            var workOrderProperties = typeof(WorkOrder).GetProperties();

            var orderProperties = typeof(Order).GetProperties();
            var customerProperties = typeof(Customer).GetProperties();
            var transportProperties = typeof(Transport).GetProperties();
            var driverProperties = typeof(Driver).GetProperties();
            var deliveryProperties = typeof(Delivery).GetProperties();
            var loadingProperties = typeof(Loading).GetProperties();

            var operationalProperties = typeof(Operational).GetProperties();

            foreach (var property in workOrderProperties)
            {
                if (property.PropertyType.Namespace == "System")
                {
                    allProperties.Add(property.Name, property.PropertyType.Name.ToLower());
                }
            }

            foreach (var orderProperty in orderProperties)
            {
                if (orderProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Order.{orderProperty.Name}", orderProperty.PropertyType.Name.ToLower());
                }
            }

            foreach (var customerProperty in customerProperties)
            {
                if (customerProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Order.Customer.{customerProperty.Name}", customerProperty.PropertyType.Name.ToLower());
                }
            }

            foreach (var transportProperty in transportProperties)
            {
                if (transportProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Order.Transport.{transportProperty.Name}", transportProperty.PropertyType.Name.ToLower());
                }
            }

            foreach (var driverProperty in driverProperties)
            {
                if (driverProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Order.Transport.Driver.{driverProperty.Name}", driverProperty.PropertyType.Name.ToLower());
                }
            }

            foreach (var deliveryProperty in deliveryProperties)
            {
                if (deliveryProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Order.Transport.Delivery.{deliveryProperty.Name}", deliveryProperty.PropertyType.Name.ToLower());
                }
            }

            foreach (var loadingProperty in loadingProperties)
            {
                if (loadingProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Order.Transport.Loading.{loadingProperty.Name}", loadingProperty.PropertyType.Name.ToLower());
                }
            }

            foreach (var operationalProperty in operationalProperties)
            {
                if (operationalProperty.PropertyType.Namespace == "System")
                {
                    allProperties.Add($"Operational.{operationalProperty.Name}", operationalProperty.PropertyType.Name.ToLower());
                }
            }

            return allProperties;
        }
    }
}
