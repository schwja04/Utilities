using System;
using System.Collections.Generic;
using Utilities.Common.Data.Abstractions;

namespace Utilities.Common.Data
{
    public sealed class Convert : IConvert
    {
        private readonly DefaultValueHandlerDictionary _handlers;

        public Convert()
        {
            _handlers = new();
        }

        public Convert(IEnumerable<KeyValuePair<Type, Func<object, object>>> defaultValueHandlers)
        {
            _handlers = new(defaultValueHandlers);
        }

        public T Cast<T>(object value)
        {
            Type baseType = typeof(T);
            Type underlyingType = Nullable.GetUnderlyingType(baseType); // baseType.Equals(typeof(Nullable<>))
            Type destinationType = underlyingType ?? baseType;

            object resultValue = value;

            if (_handlers.TryGetValue(destinationType, out Func<object, object> handler))
            {
                resultValue = handler.Invoke(resultValue);
            }

            return resultValue is T 
                || destinationType.IsEnum 
                || (value is null && (underlyingType is not null || !baseType.IsValueType))
                    ? (T)resultValue
                    : (T)System.Convert.ChangeType(resultValue, destinationType);
        }
    }
}
