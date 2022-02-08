# Rop.Reflection

Features
--------

Helper classes to cheat code with reflection

`Cheat` static classes 
------
```csharp
/// Enumerates all interfaces that matches a filter
public static IEnumerable<Type> FilterInterfaces<K>(this Type t, Func<Type, K, bool> filter, K arg);
/// Check if class implement a interface
public static bool ImplementsInterface(this Type t, Type @interface);
/// Check if class implement a generic interface
public static bool ImplementsGenericInterface(this Type t, Type @interface);
/// Check if member is decorated with any of attributes
public static bool IsDecorated(this MemberInfo m, params string[] attributes);
/// List all implementation of generic interface
public static IEnumerable<Type> GetImplementationsOfGenericInterfaces(this Type t, Type genericinterfacebase);
/// List all arguments of generic interface implementation 
public static Type[] GetArgumentsOfGenericInterface(this Type t, Type genericinterfacebase);
/// List argument type of generic interface implementation
public static Type GetTypeOfGenericInterface(this Type t, Type genericinterfacebase);
/// Get backing field of a fieldname with field not declared
public static FieldInfo GetBackingField(this Type t, PropertyInfo prop);
/// Get private field
public static FieldInfo GetPrivateField(this Type t, string fieldname);
/// Get private method
public static MethodInfo GetPrivateMethod(this Type t, string method);
/// Get private static method
public static MethodInfo GetPrivateStaticMethod(this Type t, string method);
/// Get public static method
public static MethodInfo GetStaticMethod(this Type t, string method);
/// Get private property
public static PropertyInfo GetPrivateProperty(this Type t, string property);
/// Get private field of type
public static FieldInfo GetPrivateField(Type t, string field, Type declaringtype);
/// Get private method of type
public static MethodInfo GetPrivateMethod(Type t, string property, Type declaringtype);
/// Create object with init parameters
public static T NewWithParams<T>(params object[] initparams);
/// Create a derived object with init parameters
public static T NewDerivedWithParams<T>(Type derivedtype, params object[] initparams);
public static IEnumerable<Type> GetNestedClassesOfType(this Type t, Type @base, BindingFlags? flags = null);
public static IEnumerable<Type> GetNestedClassesOfType<T>(Type @base, BindingFlags? flags = null);
public static IEnumerable<Type> GetNestedClassesOfType<T, U>(BindingFlags? flags = null);
public static IEnumerable<Type> GetNestedClassesOfType<T>(BindingFlags? flags = null);
/// Get all properties of type T
public static IEnumerable<PropertyInfo> GetPropertiesOfBaseType<T>(this Type @base);
/// Get hidden constructor
public static ConstructorInfo GetHiddenContructor(Type t, params Type[] paramTypes);
public static ConstructorInfo GetHiddenContructor<T>(params Type[] paramTypes);
/// Get value of attribute of a class
public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,Func<TAttribute, TValue> valueSelector);
/// Create derived class
public static T CreateDerivedInstanceOf<T>(Type t);
```

`DecoratedMembers` static classes 
------
```csharp
/// Check if custom attributedata are in a list of attribute names
public static bool AttributeDataMatch(this CustomAttributeData a, params string[] names);
/// Member filter Anydecorated member
public static readonly MemberFilter DecoratedMemberAny;
/// Member filter one decorate member
public static readonly MemberFilter DecoratedMember1;
/// Get decorated member
public static IEnumerable<MemberInfo> GetDecoratedMemberAny(this Type t,MemberTypes mtype,BindingFlags bindingFlags,params string[] attributes);
public static IEnumerable<MemberInfo> GetDecoratedMember(this Type t,MemberTypes mtype,BindingFlags bindingFlags,string attribute);
/// Get decorated properties
public static IEnumerable<PropertyInfo> GetDecoProperties(this Type t,params string[] attributes);
public static PropertyInfo GetDecoProperty(this Type t,string attribute);
/// Get decorated class
public static CustomAttributeData GetDecoratedAttribute(this Type t,string attribute);
/// Get decorated value
public static T GetDecoratedValue<T>(this Type t,string attribute);
```

 ------
 (C)2022 Ramón Ordiales Plaza
