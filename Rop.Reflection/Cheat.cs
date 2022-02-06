using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rop.Reflection
{
    /// <summary>
    /// Helper class for Cheating
    /// </summary>
    public static partial class Cheat
    {
        /// <summary>
        /// Enumerates all interfaces that matches a filter
        /// </summary>
        /// <typeparam name="K">Argument type</typeparam>
        /// <param name="t">class to check</param>
        /// <param name="filter">Filter function</param>
        /// <param name="arg">Argument for filter</param>
        /// <returns></returns>
        public static IEnumerable<Type> FilterInterfaces<K>(this Type t, Func<Type, K, bool> filter, K arg)
        {
            return t.FindInterfaces((i, o) => filter(i, (K)o), arg);
        }
        /// <summary>
        /// Check if class implement a interface
        /// </summary>
        /// <param name="t">Class to check</param>
        /// <param name="interface">Interface to find</param>
        /// <returns>true if class implement interface</returns>
        public static bool ImplementsInterface(this Type t, Type @interface)
        {
            return t.FindInterfaces((i, _) => i == @interface, null).Any();
        }
        /// <summary>
        /// Check if class implement a generic interface
        /// </summary>
        /// <param name="t">Class to check</param>
        /// <param name="interface">Generic interface to find</param>
        /// <returns></returns>
        public static bool ImplementsGenericInterface(this Type t, Type @interface)
        {
            return t.GetImplementationsOfGenericInterfaces(@interface).Any();
        }
        /// <summary>
        /// Check if member is decorated with any of attributes
        /// </summary>
        /// <param name="m">Member to check</param>
        /// <param name="attributes">List of possibles attributes</param>
        /// <returns></returns>
        public static bool IsDecorated(this MemberInfo m, params string[] attributes)
        {
            return DecoratedMembers.DecoratedMemberAny(m, attributes);
        }
        /// <summary>
        /// List all implementation of generic interface
        /// </summary>
        /// <param name="t">Class to check</param>
        /// <param name="genericinterfacebase">Generic interface to find</param>
        /// <returns></returns>
        public static IEnumerable<Type> GetImplementationsOfGenericInterfaces(this Type t, Type genericinterfacebase)
        {
            return t.FilterInterfaces<Type>((i, g) => i.IsGenericType && i.GetGenericTypeDefinition() == g, genericinterfacebase);
        }
        /// <summary>
        /// List all arguments of generic interface implementation 
        /// </summary>
        /// <param name="t">Class to check</param>
        /// <param name="genericinterfacebase">Generic interface to find</param>
        /// <returns>All argument types implemented</returns>
        public static Type[] GetArgumentsOfGenericInterface(this Type t, Type genericinterfacebase)
        {
            return t.GetImplementationsOfGenericInterfaces(genericinterfacebase).FirstOrDefault()?.GetGenericArguments();
        }
        /// <summary>
        /// List argument type of generic interface implementation
        /// </summary>
        /// <param name="t">Class to check</param>
        /// <param name="genericinterfacebase">Generic interface to find</param>
        /// <returns>Argument type of implemented interface</returns>
        public static Type GetTypeOfGenericInterface(this Type t, Type genericinterfacebase)
        {
            return GetArgumentsOfGenericInterface(t, genericinterfacebase)?.FirstOrDefault();
        }
        /// <summary>
        /// Get backing field of a fieldname with field not declared
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="prop">Property to check</param>
        public static FieldInfo GetBackingField(this Type t, PropertyInfo prop)
        {
            return t.GetField($"<{prop.Name}>k__BackingField", BF.PrivateInstance);
        }
        /// <summary>
        /// Get private field
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="fieldname">Private Field name</param>
        public static FieldInfo GetPrivateField(this Type t, string fieldname)
        {
            return t.GetField(fieldname, BF.PrivateInstance);
        }
        /// <summary>
        /// Get private method
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="method">Method to find</param>
        public static MethodInfo GetPrivateMethod(this Type t, string method)
        {
            return t.GetMethod(method, BF.PrivateInstance);
        }
        /// <summary>
        /// Get private static method
        /// </summary>
        /// <param name="t">TYpe to check</param>
        /// <param name="method">Private static method to find</param>
        /// <returns></returns>
        public static MethodInfo GetPrivateStaticMethod(this Type t, string method)
        {
            return t.GetMethod(method, BF.PrivateStatic);
        }
        /// <summary>
        /// Get public static method
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="method">Public static method to find</param>
        /// <returns></returns>
        public static MethodInfo GetStaticMethod(this Type t, string method)
        {
            return t.GetMethod(method, BF.PublicStatic);
        }
        /// <summary>
        /// Get private property
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="property">Private property to find</param>
        /// <returns></returns>
        public static PropertyInfo GetPrivateProperty(this Type t, string property)
        {
            return t.GetProperty(property, BF.PrivateInstance);
        }
        /// <summary>
        /// Get private field of type
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="field">Private field to find</param>
        /// <param name="declaringtype">Type of field</param>
        /// <returns></returns>
        public static FieldInfo GetPrivateField(Type t, string field, Type declaringtype)
        {
            return t.GetFields(BF.PrivateInstance).FirstOrDefault(f => StringComparer.Ordinal.Equals(f.Name, field) && (f.DeclaringType == declaringtype));
        }
        /// <summary>
        /// Get private method of type
        /// </summary>
        /// <param name="t">Type to check</param>
        /// <param name="property">Private method to find</param>
        /// <param name="declaringtype">Type of method</param>
        /// <returns></returns>
        public static MethodInfo GetPrivateMethod(Type t, string property, Type declaringtype)
        {
            return t.GetMethods(BF.PrivateInstance).FirstOrDefault(f => StringComparer.Ordinal.Equals(f.Name, property) && (f.DeclaringType == declaringtype));
        }
        /// <summary>
        /// Create object with init parameters
        /// </summary>
        /// <typeparam name="T">type to create</typeparam>
        /// <param name="initparams">init parameters</param>
        /// <returns></returns>
        public static T NewWithParams<T>(params object[] initparams)
        {
            return (T)Activator.CreateInstance(typeof(T), initparams);
        }
        /// <summary>
        /// Create a derived object with init parameters
        /// </summary>
        /// <typeparam name="T">base type</typeparam>
        /// <param name="derivedtype">derived type to create</param>
        /// <param name="initparams">init parameters</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T NewDerivedWithParams<T>(Type derivedtype, params object[] initparams) where T : class
        {
            if (derivedtype == null) return null;
            if (!derivedtype.IsClassOrSubClass(typeof(T))) throw new Exception("Type Mismatch");
            return (T)Activator.CreateInstance(derivedtype, initparams);
        }

        public static IEnumerable<Type> GetNestedClassesOfType(this Type t, Type @base, BindingFlags? flags = null)
        {
            flags = flags ?? (BindingFlags.NonPublic | BindingFlags.Public);
            return t.GetNestedTypes(flags.Value).Where(c => !c.IsAbstract && !c.IsSubclassOf(@base));
        }

        public static IEnumerable<Type> GetNestedClassesOfType<T>(Type @base, BindingFlags? flags = null)
        {
            return typeof(T).GetNestedClassesOfType(@base, flags);
        }

        public static IEnumerable<Type> GetNestedClassesOfType<T, U>(BindingFlags? flags = null)
        {
            return typeof(T).GetNestedClassesOfType(typeof(U), flags);
        }

        public static IEnumerable<Type> GetNestedClassesOfType<T>(BindingFlags? flags = null)
        {
            return GetNestedClassesOfType(typeof(T), typeof(T), flags);
        }
        /// <summary>
        /// Get all properties of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesOfBaseType<T>(this Type @base)
        {
            // ReSharper disable once PossibleNullReferenceException
            return @base.GetProperties().Where(p => p.DeclaringType.IsAssignableFrom(typeof(T)));
        }
        /// <summary>
        /// Get hidden constructor
        /// </summary>
        /// <param name="t"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public static ConstructorInfo GetHiddenContructor(Type t, params Type[] paramTypes)
        {
            return t.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, paramTypes, null);
        }

        public static ConstructorInfo GetHiddenContructor<T>(params Type[] paramTypes)
        {
            return GetHiddenContructor(typeof(T), paramTypes);
        }
        /// <summary>
        /// Get value of attribute of a class
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="type"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
            Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            return att != null ? valueSelector(att) : default(TValue);
        }
        /// <summary>
        /// Create derived class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T CreateDerivedInstanceOf<T>(Type t) where T : class
        {
            if (!t.IsClassOrSubClass(typeof(T))) throw new Exception("Type Mismatch");
            var obj = Activator.CreateInstance(t);
            return (T)obj;
        }
    }


}