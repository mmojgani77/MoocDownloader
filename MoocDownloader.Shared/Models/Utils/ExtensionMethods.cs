using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MoocDownloader.Shared.Models.Utils
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Type> InheritedTypes(this Type baseType)
        {
            var assembly = Assembly.GetAssembly(baseType);
            return assembly.GetTypes().Where(t => t != baseType &&
                                                  baseType.IsAssignableFrom(t));
        }

        public static IEnumerable<Type> WithAttribute<T>(this IEnumerable<Type> types)
        {
            return types.Where(x => x.CustomAttributes.Any(x => x.AttributeType == typeof(T)));
        }
    }
}
