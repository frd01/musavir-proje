using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Tacmin.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> instance, TKey key)
        {
            instance.TryGetValue(key, out var val);
            return val;
        }

        public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> instance, TKey key, TValue fallback = default(TValue))
        {
            if (key == null)
                return fallback;

            if (instance.TryGetValue(key, out var val))
                fallback = val;

            return fallback;
        }

        public static TValue Get<TValue>(this IDictionary<string, object> instance, string key, TValue fallback = default(TValue))
        {
            instance.TryGetValue(key, out var val);

            return (TValue)(val ?? fallback);
        }

        public static void AttributeMerge(this IDictionary<string, object> instance, object dct)
        {
            instance.AttributeMerge(dct.ToDictionary());
        }

        public static TKey KeyByValue<TKey, TVal>(this Dictionary<TKey, TVal> dict, TVal val)
        {
            var key = default(TKey);
            foreach (var pair in dict)
            {
                if (Comparer<TVal>.Default.Compare(pair.Value, val) == 0)
                {
                    key = pair.Key;
                    break;
                }
            }
            return key;
        }

        /// <summary>
        /// Convert the object properties to a dictionary
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        /// <summary>
        /// Converts the object properties to a dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary<T>(property, source, dictionary);

            return dictionary;
        }

        private static void AddPropertyToDictionary<T>(PropertyDescriptor property, object source, Dictionary<string, T> dictionary)
        {
            var value = property.GetValue(source);
            if (IsOfType<T>(value))
            {
                dictionary.Add(property.Name, (T)value);
            }
            else
            {
                var newValue = (T)Convert.ChangeType(value, typeof(T));
                dictionary.Add(property.Name, newValue);
            }
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}