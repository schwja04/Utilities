using System;

namespace Utilities.Common.Data.Extensions
{
    internal static class GenericExtensions
    {
        private static readonly object DEFAULT_STRING_OBJECT = string.Empty;

        public static T GetDefaultValue<T>()
        {
            Type type = typeof(T);
            return type switch
            {
                Type when type.Equals(typeof(string)) => (T)DEFAULT_STRING_OBJECT,
                _ => default
            };
        }
    }
}
