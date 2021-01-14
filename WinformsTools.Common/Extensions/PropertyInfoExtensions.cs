using System;
using System.Reflection;

namespace WinformsTools.Common.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool HasCustomAttribute<TCustomAttribute>(this PropertyInfo propertyInfo)
            where TCustomAttribute : Attribute
        {
            return propertyInfo.GetCustomAttribute<TCustomAttribute>() != null;
        }
    }
}
