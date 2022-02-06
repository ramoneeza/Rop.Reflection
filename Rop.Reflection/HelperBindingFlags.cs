using System.Reflection;

// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming

namespace Rop.Reflection
{
    /// <summary>
    ///  Helper class to Binding Flags
    /// </summary>
    public static class BF
    {
        public static readonly BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;
        public static readonly BindingFlags PublicStatic = BindingFlags.Public | BindingFlags.Static;
        public static readonly BindingFlags PrivateInstance = BindingFlags.NonPublic | BindingFlags.Instance;
        public static readonly BindingFlags PrivateStatic = BindingFlags.NonPublic | BindingFlags.Static;
        public static readonly BindingFlags AnyInstance = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        public static readonly BindingFlags AnyStatic = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
    }
}
