using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utilities.Common.Data.Extensions;

namespace Utilities.Common.Data
{
    public sealed class Convert2 : IConvert
    {
        private readonly DefaultValueHandlerDictionary _defaultValueHandlers;

        public Convert2()
        {
            _defaultValueHandlers = new();
        }

        public ReadOnlyDictionary<Type, Func<object, object>> DefaultValueHandlers
        {
            get => _defaultValueHandlers.DefaultValueHandlers;
        }

        public void AddOrUpdateHandlers(IEnumerable<KeyValuePair<Type, Func<object, object>>> handlers)
        {
            _defaultValueHandlers.AddOrUpdate(handlers);
        }

        public void AddOrUpdateHandler(Type type, Func<object, object> handler)
        {
            _defaultValueHandlers.AddOrUpdate(type, handler);
        }

        public T Cast<T>(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Type destinationType = typeof(T);
            object resultValue = value;

            if (_defaultValueHandlers.TryGetValue(destinationType, out Func<object, object> handler))
            {
                resultValue = handler.Invoke(resultValue);
            }

            return (resultValue is T || destinationType.IsEnum)
                ? (T)resultValue
                : (T)System.Convert.ChangeType(resultValue, destinationType);
        }
    }
}

