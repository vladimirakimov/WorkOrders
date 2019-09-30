namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface ITypeConverterProvider
    {
        int ToInt(string value, int defaultValue);
        int ToInt(string value);
        float ToFloat(string value);
        bool CanConvertToFloat(string value);
        bool IsInt(float value);
    }
}
