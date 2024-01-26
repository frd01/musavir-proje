using System;
using System.ComponentModel;

namespace Tacmin.Core.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum val)
        {
            return Convert.ToInt32(val);
        }

        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            if (name == null)
                return "";
            var field = type.GetField(name);
            if (field != null)
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute sttr)
                {
                    if (!sttr.Description.IsEmpty())
                    {
                        name = sttr.Description;
                    }
                }
            }
            return name;
        }
    }
}
