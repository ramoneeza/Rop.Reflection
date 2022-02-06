using System;
using System.Linq;

namespace Rop.Reflection
{
    /// <summary>
    ///  Helper class to types
    /// </summary>
    public static class HelperType
    {
        /// <summary>
        /// Get underlyingtype of nullable type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type SkipNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type)??type;
        }
        /// <summary>
        /// Get standard name of type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
       public static string GetStdName(this Type type)
        {
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
                // ReSharper disable once PossibleNullReferenceException
                return Nullable.GetUnderlyingType(type).Name + "?";
            else
                return type.Name;
        }
        /// <summary>
        /// Check if class if that class o a subclass
        /// </summary>
        /// <param name="derived"></param>
        /// <param name="baseclass"></param>
        /// <returns></returns>
        public static bool IsClassOrSubClass(this Type derived, Type baseclass)
        {
            return derived.IsSubclassOf(baseclass) || derived == baseclass;
        }
        /// <summary>
        /// Check if type is an extension of a interface
        /// </summary>
        /// <param name="thisType"></param>
        /// <param name="potentialSuperType"></param>
        /// <returns></returns>
        public static bool IsExtension(this Type thisType, Type potentialSuperType)
        {
            //
            // protect ya neck
            //
            if (thisType == null || potentialSuperType == null || thisType == potentialSuperType) return false;

            //
            // don't need to traverse inheritance for interface extension, so check/do these first
            //
            if (potentialSuperType.IsInterface)
            {
                foreach (var interfaceType in thisType.GetInterfaces())
                {
                    var tempType = interfaceType.IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType;

                    if (tempType == potentialSuperType)
                    {
                        return true;
                    }
                }
            }

            //
            // do the concrete type checks, iterating up the inheritance chain, as in orignal
            //
            while (thisType != null && thisType != typeof(object))
            {
                var cur = thisType.IsGenericType ? thisType.GetGenericTypeDefinition() : thisType;

                if (potentialSuperType == cur)
                {
                    return true;
                }

                thisType = thisType.BaseType;
            }
            return false;
        }
        /// <summary>
        /// Get generic type of interface or class that implements
        /// </summary>
        /// <param name="thisType"></param>
        /// <param name="genericTypeOrInterface"></param>
        /// <returns></returns>
        public static Type GetGenericTypeOf(this Type thisType, Type genericTypeOrInterface)
        {
            //
            // protect ya neck
            //
            if (thisType == null || genericTypeOrInterface == null || thisType == genericTypeOrInterface || !genericTypeOrInterface.IsGenericType) return null;

            //
            // don't need to traverse inheritance for interface extension, so check/do these first
            //
            if (genericTypeOrInterface.IsInterface)
            {
                var candidate = thisType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericTypeOrInterface);
                if (candidate != null) return candidate.GenericTypeArguments[0];
            }

            //
            // do the concrete type checks, iterating up the inheritance chain, as in orignal
            //
            while (thisType != null && thisType != typeof(object))
            {
                if (thisType.IsGenericType && thisType.GetGenericTypeDefinition()==genericTypeOrInterface)
                {
                    return thisType.GenericTypeArguments[0];
                }
                thisType = thisType.BaseType;
            }
            return null;
        }
        /// <summary>
        /// Check if type is value type or string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValueTypeOrStr(this Type type)
        {
            if (type == typeof(String)) return true;
            return type.IsValueType;
        }
        /// <summary>
        /// Check if type is string or primitive type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitiveTypeOrStr(this Type type)
        {
            if (type == typeof(String)) return true;
            return type.IsValueType && type.IsPrimitive;
        }
    }
}
