using System;
using System.Linq;
using System.Linq.Expressions;
using WinformsTools.Common.Extensions;

namespace WinformsTools.MVVM.Bindings
{
    public static class ReflectionUtils
    {
        public static string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> action)
        {
            var body = (MemberExpression)action.Body;
            return body.Member.Name;
        }

        public static string GetFullPath<TSource, TProperty>(Expression<Func<TSource, TProperty>> action)
        {
            return action.Body.ToString().Split('.').Skip(1).Joined(".");
        }

        public static void SetValue<TSource, TProperty>(TSource item, Expression<Func<TSource, TProperty>> member, TProperty value)
        {
            var property = typeof(TSource).GetProperty(GetPropertyName(member));
            property.SetValue(item, value);
        }

        public static TProperty GetValue<TSource, TProperty>(TSource item, Expression<Func<TSource, TProperty>> member)
        {
            var property = typeof(TSource).GetProperty(GetPropertyName(member));
            return (TProperty) property.GetValue(item);
        }
    }
}
