using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities.Common.Data
{
    internal sealed class DefaultValueHandlerDictionary
    {
        private readonly ConcurrentDictionary<Type, Func<object, object>> _typeHandlers;

        public DefaultValueHandlerDictionary()
        {
            _typeHandlers = new();
            DefaultValueHandlers = new(_typeHandlers);
        }

        public DefaultValueHandlerDictionary(IEnumerable<KeyValuePair<Type, Func<object, object>>> collection)
        {
            if (collection is null) throw new ArgumentNullException(nameof(collection));

            _typeHandlers = new();
            DefaultValueHandlers = new(_typeHandlers);

            AddOrUpdateMany(collection);
        }

        public ReadOnlyDictionary<Type, Func<object, object>> DefaultValueHandlers { get; }

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

        public void AddOrUpdateMany(IEnumerable<KeyValuePair<Type, Func<object, object>>> handlers)
        {
            foreach (KeyValuePair<Type, Func<object, object>> handler in handlers)
            {
                AddOrUpdate(handler.Key, handler.Value);
            }
        }

        public void Clear() => _typeHandlers.Clear();

        public bool TryGetValue(Type type, out Func<object, object> handler) =>
            _typeHandlers.TryGetValue(type, out handler);

        //private static object HandleBoolean(object value)
        //{
        //    bool boolValue = false;
        //    return StringExtensions.TryParseBoolean(value.ToString(), ref boolValue)
        //        && boolValue;
        //}

        //private static object HandleString(object value)
        //{
        //    return value.ToString().Trim();
        //}
    }
}

