using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rop.Reflection
{
    public static class DecoratedMembers
    {
        /// <summary>
        /// Check if custom attributedata are in a list of attribute names
        /// </summary>
        /// <param name="a"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static bool AttributeDataMatch(this CustomAttributeData a, params string[] names)
        {
            var name = a.AttributeType.Name;
            if (name.EndsWith("Attribute")) name = name.Substring(0, name.Length - 9);
            return names?.Contains(name) ?? false;
        }
        /// <summary>
        /// Member filter Anydecorated member
        /// </summary>
        public static readonly MemberFilter DecoratedMemberAny = (m, o) =>
            m.GetCustomAttributesData().Any(a =>a.AttributeDataMatch(o as string[]) );
        /// <summary>
        /// Member filter one decorate member
        /// </summary>
        public static readonly MemberFilter DecoratedMember1 = (m, o) => m.GetCustomAttributesData().Any(a =>a.AttributeDataMatch(o as string));
        /// <summary>
        /// Get decorated member
        /// </summary>
        /// <param name="t"></param>
        /// <param name="mtype"></param>
        /// <param name="bindingFlags"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetDecoratedMemberAny(this Type t,MemberTypes mtype,BindingFlags bindingFlags,params string[] attributes)
        {
            return t.FindMembers(mtype,bindingFlags, DecoratedMemberAny,attributes);
        }
        public static IEnumerable<MemberInfo> GetDecoratedMember(this Type t,MemberTypes mtype,BindingFlags bindingFlags,string attribute)
        {
            return t.FindMembers(mtype,bindingFlags, DecoratedMember1,attribute);
        }
        /// <summary>
        /// Get decorated properties
        /// </summary>
        /// <param name="t"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetDecoProperties(this Type t,params string[] attributes)
        {
            return t.GetDecoratedMemberAny(MemberTypes.Property,BF.PublicInstance,attributes).Cast<PropertyInfo>();
        }
        public static PropertyInfo GetDecoProperty(this Type t,string attribute)
        {
            return t.GetDecoratedMember(MemberTypes.Property,BF.PublicInstance,attribute).FirstOrDefault() as PropertyInfo;
        }
        /// <summary>
        /// Get decorated class
        /// </summary>
        /// <param name="t"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static CustomAttributeData GetDecoratedAttribute(this Type t,string attribute)
        {
            return t.GetCustomAttributesData().FirstOrDefault(a => a.AttributeType.Name == attribute);
        }
        /// <summary>
        /// Get decorated value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static T GetDecoratedValue<T>(this Type t,string attribute)
        {
            var a = GetDecoratedAttribute(t, attribute);
            if (a == null) return default;
            if (a.ConstructorArguments.Count == 0) return default;
            return (T)a.ConstructorArguments[0].Value;
        }


    }
}