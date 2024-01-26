using System;
using System.Collections.Generic;
using System.Linq;

namespace Tacmin.Core.Extensions
{
    public static class TypeExtensions
    {
        public class PropInfo
        {
            public string PropertyName { get; set; }
            public string OriginName { get; set; }
            public string DisplayName { get; set; }
            public string TypeName { get; set; }
            public string FwTypeName { get; set; }
            public bool Writable { get; set; }
            public bool IgnoreFilterPanel { get; set; }
            public bool Required { get; set; }
        }

        public static readonly Dictionary<Type, string> TypeAliases = new Dictionary<Type, string> {
            { typeof(int), "int" },
            { typeof(short), "short" },
            { typeof(byte), "byte" },
            { typeof(byte[]), "byte[]" },
            { typeof(long), "long" },
            { typeof(double), "double" },
            { typeof(decimal), "decimal" },
            { typeof(float), "float" },
            { typeof(bool), "bool" },
            { typeof(string), "string" },
            { typeof(TimeSpan), "TimeSpan" }
        };

        public static readonly Type[] PredefinedTypes = {
            typeof(Object),
            typeof(Boolean),
            typeof(Char),
            typeof(String),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert)
        };

        public static bool IsSimpleType(this Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                new Type[]
                {
                    typeof(String),
                    typeof(Decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) || Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static bool IsPredefinedType(this Type type)
        {
            foreach (var t in PredefinedTypes)
            {
                if (t == type)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsEnumType(this Type type)
        {
            return GetNonNullableType(type).IsEnum;
        }

        public static Type GetNonNullableType(this Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }

        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsSubClass(this Type type, Type check)
        {
            return IsSubClass(type, check, out var implementingType);
        }

        public static bool IsSubClass(this Type type, Type check, out Type implementingType)
        {
            return IsSubClassInternal(type, type, check, out implementingType);
        }

        private static bool IsSubClassInternal(Type initialType, Type currentType, Type check, out Type implementingType)
        {
            if (currentType == check)
            {
                implementingType = currentType;
                return true;
            }

            // don't get interfaces for an interface unless the initial type is an interface
            if (check.IsInterface && (initialType.IsInterface || currentType == initialType))
            {
                foreach (var t in currentType.GetInterfaces())
                {
                    if (IsSubClassInternal(initialType, t, check, out implementingType))
                    {
                        // don't return the interface itself, return it's implementor
                        if (check == implementingType)
                            implementingType = currentType;

                        return true;
                    }
                }
            }

            if (currentType.IsGenericType && !currentType.IsGenericTypeDefinition)
            {
                if (IsSubClassInternal(initialType, currentType.GetGenericTypeDefinition(), check, out implementingType))
                {
                    implementingType = currentType;
                    return true;
                }
            }

            if (currentType.BaseType == null)
            {
                implementingType = null;
                return false;
            }

            return IsSubClassInternal(initialType, currentType.BaseType, check, out implementingType);
        }
        public static int ToInt(this object veri)
        {
            if (veri == null)
                veri = 0;
            return int.TryParse(veri.ToString(), out var fer) ? fer : 0;
        }

        public static short ToShort(this object veri)
        {
            if (veri == null)
                veri = 0;
            return short.TryParse(veri.ToString(), out var fer) ? fer : (short)0;
        }
        public static decimal ToDecimal(this object veri)
        {
            return decimal.TryParse(veri.ToString(), out var fer) ? fer : 0;
        }
        public static float ToFloat(this object veri)
        {

            var data = float.TryParse(veri.ToString(), out var fer) ? fer : 0;
            if (float.IsNaN(data) || float.IsInfinity(data))
                data = 0;
            return data;
        }
        public static double ToDouble(this object veri)
        {
            if (veri == null)
                veri = 0;
            var data = double.TryParse(veri.ToString(), out var fer) ? fer : 0;
            if (double.IsNaN(data) || double.IsInfinity(data))
                data = 0;
            return data;
        }
        public static DateTime ToDateTime(this object veri)
        {
            if (veri == null)
                veri = new DateTime();
            return DateTime.TryParse(veri.ToString(), out var fer) ? fer : fer;
        }
        public static bool ToBool(this object veri)
        {
            if (veri == null)
                veri = false;

            return Convert.ToBoolean(veri);
        }
        public static string ToDate(this object veri)
        {
            if (veri == null)
                veri = new DateTime();
            return DateTime.TryParse(veri.ToString(), out var fer) ? fer.ToString("dd.MM.yyyy") : fer.ToString("dd.MM.yyyy");
        }
        public static long ToLong(this object veri)
        {
            if (veri == null)
                veri = 0;
            return long.TryParse(veri.ToString(), out var fer) ? fer : fer;
        }

        public static TimeSpan ToTimeSpan(this object veri)
        {
            return TimeSpan.TryParse(veri.ToString(), out var fer) ? fer : fer;
        }

        public static bool IsNullDate(this string value)
        {
            if (value == new DateTime().Date.ToString("dd.MM.yyyy"))
                value = null;
            else if (value == new DateTime().ToString())
                value = null;
            return (value == null);
        }

        /// <summary>
        /// .ToString(miras aldığı veri,verinin tipi,o tipe göre formatlıyacağı şekil örnek double için "C2")
        /// </summary>
        /// <param name="veri"></param>
        /// <param name="tip"></param>
        /// <param name="formatTipi"></param>
        /// <returns></returns>
        public static string ToString(this object veri, Type tip, string formatTipi)
        {
            var formattype = tip;
            if (formattype == typeof(int) || formattype == typeof(Int32))
            {
                return veri.ToInt().ToString(formatTipi.ToString());
            }
            else if (formattype == typeof(decimal) || formattype == typeof(double))
            {
                return veri.ToDecimal().ToString(formatTipi.ToString());
            }
            else if (formattype == typeof(string))
            {
                return veri.ToString();
            }
            else if (formattype == typeof(DateTime))
            {
                return veri.ToDateTime().ToString(formatTipi.ToString());
            }
            else if (formattype == typeof(float))
            {
                return veri.ToFloat().ToString(formatTipi.ToString());
            }
            else if (formattype == typeof(long))
            {
                return veri.ToLong().ToString(formatTipi.ToString());
            }
            else if (formattype == typeof(TimeSpan))
            {
                return veri.ToTimeSpan().ToString(formatTipi.ToString());
            }
            return veri.ToString();
        }
    }
}
