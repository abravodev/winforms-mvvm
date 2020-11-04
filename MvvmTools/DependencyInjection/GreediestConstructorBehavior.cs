using SimpleInjector.Advanced;
using System;
using System.Linq;
using System.Reflection;

namespace MvvmTools.DependencyInjection
{
    /// <summary>
    /// <see href="https://simpleinjector.readthedocs.io/en/latest/extensibility.html#overriding-constructor-resolution-behavior"/>
    /// </summary>
    public class GreediestConstructorBehavior : IConstructorResolutionBehavior
    {
        public ConstructorInfo TryGetConstructor(
            Type implementationType, out string errorMessage)
        {
            errorMessage = null;
            return implementationType
                .GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .FirstOrDefault();
        }
    }
}
