using System.Collections.Generic;
using System;
using Utilities.Common.Data.Extensions;

namespace Utilities.Common.Data
{
    public static partial class Conversions
    {
        private static Dictionary<Type, Func<object, object>> _defaultValueHandlerDictionary;

        static partial void UpdateDefaultValueHandlers(Dictionary<Type, Func<object, object>> handlerDictionary);

        public static T Cast<T>(object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            InitializeHandlers();

            Type destinationType = typeof(T);
            object resultValue = value;

            if (_defaultValueHandlerDictionary.TryGetValue(destinationType, out Func<object, object> handler))
            {
                resultValue = handler.Invoke(resultValue);
            }

            return (resultValue is T || destinationType.IsEnum)
                ? (T)resultValue
                : (T)Convert.ChangeType(resultValue, destinationType);
        }

        private static void InitializeHandlers()
        {
            if (_defaultValueHandlerDictionary is null)
            {
                _defaultValueHandlerDictionary = new Dictionary<Type, Func<object, object>>(2)
                {
                    [typeof(bool)] = HandleBoolean,
                    [typeof(string)] = HandleString
                };
                UpdateDefaultValueHandlers(_defaultValueHandlerDictionary);
            }

            static object HandleBoolean(object value)
            {
                bool boolValue = false;
                return StringExtensions.TryParseBoolean(value.ToString(), ref boolValue)
                    && boolValue;
            }

            static object HandleString(object value)
            {
                return value.ToString().Trim();
            }
        }
    }
}
