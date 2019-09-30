using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Extensions
{
    public static class IDictionaryExtensions
    {
        public static Optional<T> GetOptional<T>(this IDictionary<string, object> dictionary, string key)
        {
            Optional<T> result = new Optional<T>();

            Optional<object> optionalObject = dictionary.ContainsKey(key) ? new Optional<object>(dictionary[key]) : new Optional<object>();
            if (optionalObject.HasValue)
            {
                if (typeof(T) == typeof(string))
                {
                    var value = optionalObject.Value;
                    result = new Optional<T>((T)value);
                }
                else if (typeof(T) == typeof(IEnumerable<HandledUnitDto>))
                {
                    var enumerable = JsonConvert.DeserializeObject<IEnumerable<HandledUnitDto>>(optionalObject.Value.ToString());
                    result = new Optional<T>((T)enumerable);
                }
                else if (typeof(T) == typeof(IEnumerable<RemarkDto>))
                {
                    var enumerable = JsonConvert.DeserializeObject<IEnumerable<RemarkDto>>(optionalObject.Value.ToString());
                    result = new Optional<T>((T)enumerable);
                }
                else if (typeof(T) == typeof(IEnumerable<PictureDto>))
                {
                    var enumerable = JsonConvert.DeserializeObject<IEnumerable<PictureDto>>(optionalObject.Value.ToString());
                    result = new Optional<T>((T)enumerable);
                }
            }

            return result;
        }
    }
}
