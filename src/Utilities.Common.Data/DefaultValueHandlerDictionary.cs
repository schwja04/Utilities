using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utilities.Common.Data.Extensions;

namespace Utilities.Common.Data
{
    internal sealed class DefaultValueHandlerDictionary
    {
        private readonly ConcurrentDictionary<Type, Func<object, object>> _typeHandlers;

        public DefaultValueHandlerDictionary()
        {
            _typeHandlers = new()
            {
                [typeof(bool)] = HandleBoolean,
                [typeof(string)] = HandleString
            };

            DefaultValueHandlers = new(_typeHandlers);
        }

        public ReadOnlyDictionary<Type, Func<object, object>> DefaultValueHandlers { get; }

        public void AddOrUpdate(IEnumerable<KeyValuePair<Type, Func<object, object>>> handlers)
        {
            foreach (KeyValuePair<Type, Func<object, object>> handler in handlers)
            {
                AddOrUpdate(handler.Key, handler.Value);
            }
        }

        public void AddOrUpdate(Type type, Func<object, object> handler)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            _typeHandlers.AddOrUpdate(type, handler, (k, v) => handler);
        }

        public bool TryGetValue(Type type, out Func<object, object> handler) =>
            _typeHandlers.TryGetValue(type, out handler);

        private static object HandleBoolean(object value)
        {
            bool boolValue = false;
            return StringExtensions.TryParseBoolean(value.ToString(), ref boolValue)
                && boolValue;
        }

        private static object HandleString(object value)
        {
            return value.ToString().Trim();
        }
    }
}

