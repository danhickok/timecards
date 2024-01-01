using System.ComponentModel;
using System.Reflection;

namespace TimecardsCore.ExtensionMethods
{
    public static class EnumExtensionMethods
    {
        /// <summary>
        /// This method returns the Description attribute of the given enum value
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Description as defined by enum's attribute</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }

            return value.ToString();
        }
    }
}
