using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace xUnit.Rop.Reflection
{
    internal class TestClass
    {
        private int afield;
        public int AutoProperty { get; set; }

        private int _prop { get; set; }

        [MyAttr("myvalue")]
        public string MyMethod() => "Hola";

        private int amethod() => 3;
        private static string astmethod() => "Jo";
    }
    [MyAttr("isderivedclass")]
    internal class DerivedClass : TestClass
    {
        public int OtherProperty { get; set; }

        private DerivedClass(int i) : base()
        {
            OtherProperty = i;
        }

        public DerivedClass()
        {
        }
    }

    public class MyAttr:Attribute{
        public string Value { get; }

        public MyAttr(string value)
        {
            Value = value;
        }
    }

    public class StringList:List<string>{
        public StringList(int capacity) : base(capacity)
        {
        }
    }

    public class ComposedClass
    {
        public class Sub1{}
        public class Sub2{}
        private class Sub3{}

    }
}
