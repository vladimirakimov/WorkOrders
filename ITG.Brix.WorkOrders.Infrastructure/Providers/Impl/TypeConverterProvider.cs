using System;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers.Impl
{
    public class TypeConverterProvider : ITypeConverterProvider
    {
        public int ToInt(string value, int defaultValue)
        {
            if (!int.TryParse(value, out int result))
            {
                result = defaultValue;
            }

            return result;
        }

        public int ToInt(string value)
        {
            var result = int.Parse(value);

            return result;
        }

        public float ToFloat(string value)
        {
            var result = float.Parse(value);

            return result;
        }

        public bool CanConvertToFloat(string value)
        {
            var result = float.TryParse(value, out float doubleResult);

            return result;
        }

        public bool IsInt(float value)
        {
            float tolerance = 0.001F;
            float abs = Math.Abs(value);
            var result = Math.Abs(abs - Math.Round(abs)) <= tolerance;

            return result;
        }
    }
}
