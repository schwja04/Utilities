using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Utilities.Common.Data
{
    public interface IConvert
    {
        ReadOnlyDictionary<Type, Func<object, object>> DefaultValueHandlers { get; }

        void AddOrUpdateHandler(Type type, Func<object, object> handler);

        void AddOrUpdateHandlers(IEnumerable<KeyValuePair<Type, Func<object, object>>> handlers);
        
        T Cast<T>(object value);
    }
}