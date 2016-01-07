using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FlatBuffers
{
    public static class TypeHelpers
    {
        public static Type GetEnumerableElementType(Type enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentException();
            }

            var genericEnumerableInterface = enumerable
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (genericEnumerableInterface == null)
            {
                throw new NotSupportedException();
            }
            var elementType = genericEnumerableInterface.GetGenericArguments()[0];
            return elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(Nullable<>)
                ? elementType.GetGenericArguments()[0]
                : elementType;
        }

        public static T Attribute<T>(this MemberInfo member)
        {
            return (T)member.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        public static T Attribute<T>(this Type type)
        {
            return (T)type.GetCustomAttributes(typeof(T), true).FirstOrDefault();
        }

        public static bool Defined<T>(this MemberInfo member)
        {
            return member.IsDefined(typeof (T), true);
        }

        public static bool Defined<T>(this Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).Any();
        }
    }
}