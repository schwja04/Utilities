using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities.Common.Data
{
    public static class Convert
    {
        private static DefaultValueHandlerDictionary _defaultValueHandlers;

        public static ReadOnlyDictionary<Type, Func<object, object>> DefaultValueHandlers =>
            _defaultValueHandlers?.DefaultValueHandlers;

        public static void AddOrUpdateHandler(Type type, Func<object, object> handler)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            InitializeHandlers();

            _defaultValueHandlers.AddOrUpdate(type, handler);
        }

        public static void AddOrUpdateHandlers(IEnumerable<KeyValuePair<Type, Func<object, object>>> handlers)
        {
            foreach (KeyValuePair<Type, Func<object, object>> handler in handlers)
            {
                AddOrUpdateHandler(handler.Key, handler.Value);
            }
        }

        public static T Cast<T>(object value)
        {
            InitializeHandlers();

            Type baseType = typeof(T);
            Type unlyingType = Nullable.GetUnderlyingType(baseType);
            Type destinationType = unlyingType ?? baseType;

            object resultValue = value;

            if (_defaultValueHandlers.TryGetValue(destinationType, out Func<object, object> handler))
            {
                resultValue = handler.Invoke(resultValue);
            }

            return (resultValue is T || destinationType.IsEnum || (value is null && unlyingType is not null))
                ? (T)resultValue
                : (T)System.Convert.ChangeType(resultValue, destinationType);
        }

        private static void InitializeHandlers()
        {
            _defaultValueHandlers ??= new();
        }
    }
}
