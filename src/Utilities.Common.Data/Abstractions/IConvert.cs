namespace Utilities.Common.Data.Abstractions
{
    public interface IConvert
    {
        T Cast<T>(object value);
    }
}
