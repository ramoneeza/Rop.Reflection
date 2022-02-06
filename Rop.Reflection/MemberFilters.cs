using System;
using System.Reflection;

namespace Rop.Reflection
{
    /// <summary>
    /// Helper class for memberfilters
    /// </summary>
    public static class MemberFilters
    {
        public static readonly MemberFilter FieldOfType=new MemberFilter((m,o)=>(m is FieldInfo fi)&& fi.DeclaringType==(o as Type));
        public static readonly MemberFilter PropertyOfType = new MemberFilter((m, o) => (m is PropertyInfo fi) && fi.PropertyType == (o as Type));
        public static readonly MemberFilter MethodOfType = new MemberFilter((m, o) => (m is MethodInfo fi) && fi.ReturnType == (o as Type));

        public static readonly MemberFilter FieldOfSubType = new MemberFilter((m, o) => (m is FieldInfo fi) && ((o as Type)?.IsAssignableFrom(fi.DeclaringType)??false));
        public static readonly MemberFilter PropertyOfSubType = new MemberFilter((m, o) => (m is PropertyInfo fi) && ((o as Type)?.IsAssignableFrom(fi.DeclaringType) ?? false));
        public static readonly MemberFilter MethodOfSubType = new MemberFilter((m, o) => (m is MethodInfo fi) && ((o as Type)?.IsAssignableFrom(fi.DeclaringType) ?? false));

        public static readonly MemberFilter FieldDecorated = new MemberFilter((m, o) => (m is FieldInfo fi) && fi.IsDecorated(o as string[]));
        public static readonly MemberFilter PropertyDecorated = new MemberFilter((m, o) => (m is PropertyInfo fi) && fi.IsDecorated(o as string[]));
        public static readonly MemberFilter MethodDecorated = new MemberFilter((m, o) => (m is MethodInfo fi) && fi.IsDecorated(o as string[]));
    }
}
