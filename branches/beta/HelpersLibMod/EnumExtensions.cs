using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;

namespace HelpersLibMod
{
    public class EnumExtensions
    {
        public static string[] GetEnumDescriptions<T>()
        {
            List<string> descriptions = new List<string>();
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("T must be of type System.Enum");
            }

            foreach (int value in Enum.GetValues(enumType))
            {
                descriptions.Add(((Enum)Enum.ToObject(enumType, value)).GetDescription());
            }

            return descriptions.ToArray();
        }
    }
}