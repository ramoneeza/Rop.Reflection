using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json.Serialization.Metadata;
using Rop.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace xUnit.Rop.Reflection
{
    public class CheatTest
    {
        [Fact]
        public void FilterInterfacesTest()
        {
            var tipo = typeof(ArrayList);
            var expected = new Type[] {typeof(IList),typeof(ICollection)};
            var findinterfaces = tipo.FilterInterfaces((i, lista) => lista.Contains(i), new List<Type>(){typeof(IList),typeof(ICollection),typeof(IDisposable)});
            Assert.Equal(expected,findinterfaces);
        }
        [Fact]
        public void ImplementsInterfacesTest()
        {
            var tipo = typeof(ArrayList);
            var findinterface = tipo.ImplementsInterface(typeof(IList));
            Assert.True(findinterface);
        }
        [Fact]
        public void ImplementsGenericInterfaceTest()
        {
            var tipo = typeof(List<int>);
            var findinterface = tipo.ImplementsGenericInterface(typeof(IList<>));
            Assert.True(findinterface);
        }
        
        [Fact]
        public void IsDecoratedTest()
        {
            var tipo = typeof(TestClass).GetMethod(nameof(TestClass.MyMethod));
            var isdecorated = tipo.IsDecorated("MyAttr");
            Assert.True(isdecorated);
        }
        [Fact]
        public void GetImplementationsOfGenericInterfacesTest()
        {
            var tipo = typeof(List<int>);
            var gentype = tipo.GetImplementationsOfGenericInterfaces(typeof(IList<>)).ToArray();
            var expected = new[] {typeof(IList<int>)};
            Assert.Equal(expected,gentype);
        }
        [Fact]
        public void GetArgumentsOfGenericInterfaceTest()
        {
            var tipo = typeof(List<int>);
            var gentype = tipo.GetArgumentsOfGenericInterface(typeof(IList<>)).ToArray();
            var expected = new[] { typeof(int) };
            Assert.Equal(expected, gentype);
        }
        [Fact]
        public void GetTypeOfGenericInterfaceTest()
        {
            var tipo = typeof(List<int>);
            var gentype = tipo.GetTypeOfGenericInterface(typeof(IList<>));
            var expected = typeof(int);
            Assert.Equal(expected, gentype);
        }
        [Fact]
        public void GetBackingFieldTest()
        {
            var tipo = typeof(TestClass);
            var property = tipo.GetProperty(nameof(TestClass.AutoProperty));
            var backfield = tipo.GetBackingField(property);
            Assert.NotNull(backfield);
        }
        [Fact]
        public void GetPrivateFieldTest()
        {
            var f = typeof(TestClass).GetPrivateField("afield");
            Assert.NotNull(f);
        }
        [Fact]
        public void GetPrivateMethodTest()
        {
            var m = typeof(TestClass).GetPrivateMethod("amethod");
            Assert.NotNull(m);
        }
        [Fact]
        public void GetPrivateStaticMethodTest()
        {
            var m= typeof(TestClass).GetPrivateStaticMethod("astmethod");
            Assert.NotNull(m);
        }
        [Fact]
        public void GetPrivatePropertyTest()
        {
            var p = typeof(TestClass).GetPrivateProperty("_prop");
            Assert.NotNull(p);
        }
        
        [Fact]
        public void NewWithParamsTest()
        {
            var l = Cheat.NewWithParams<List<int>>(4);
            Assert.NotNull(l);
        }
        [Fact]
        public void NewDerivedWithParamsTest()
        {
            var l = Cheat.NewDerivedWithParams<List<string>>(typeof(StringList),3);
            Assert.NotNull(l);
        }
        [Fact]
        public void GetNestedClassesOfTypeTest()
        {
            var t = typeof(ComposedClass);
            var l = t.GetNestedClassesOfType(t);
            var expected = new[] {"Sub1","Sub2","Sub3" };
            var names = l.Select(l => l.Name).ToArray();
            Assert.Equal(expected,names);
        }
        [Fact]
        public void GetPropertiesOfBaseTypeTest()
        {
            var t = typeof(DerivedClass);
            var p = t.GetPropertiesOfBaseType<TestClass>().ToArray();
            var expected = t.GetProperty(nameof(TestClass.AutoProperty));
            Assert.Equal(new []{expected},p);
        }
        [Fact]
        public void GetHiddenConstructorTest()
        {
            var t = typeof(DerivedClass);
            var h = Cheat.GetHiddenContructor(t,typeof(int));
            Assert.NotNull(h);
        }
        [Fact]
        public void GetAttributeValueTest()
        {
            var t = typeof(DerivedClass);
            var v = t.GetAttributeValue<MyAttr, string>(a => a.Value);
            Assert.Equal("isderivedclass",v);
        }
        [Fact]
        public void CreateDerivedInstanceOfTest()
        {
            var t = typeof(TestClass);
            var v = Cheat.CreateDerivedInstanceOf<TestClass>(typeof(DerivedClass));
            Assert.NotNull(v);
        }
       

    }
}