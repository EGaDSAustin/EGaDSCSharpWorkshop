using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace EGaDSTutorial
{
    public class Tests : MonoBehaviour
    {
        #region Inheritance1

        public class A
        {
            private readonly int _secret = 1;
            protected readonly int _family = 2;
            private readonly int Everyone = 3;
            
            // NOTE: Unity does not support constructors for its object types
            public A()
            {
                print("Construct an A");
            }

            public void foo()
            {
                print("A foo");
            }

            public virtual void bar()
            {
                print("A bar");
            }
            
            protected virtual void uhhh()
            {
                print("A uhhh");
            }
        }
        
        public class B : A
        {
            public B() : base() // this is done implicitly for default constructors, but this is how you call the parents constructor
            {
                print("Construct a B");
            }
            
            public void foo()
            {
                print("B foo");
            }

            public override void bar()
            {
                print("B bar");
            }
            
            protected override void uhhh()
            {
                base.uhhh(); // How to call your parents uhhh
                print("B uhhh");
            }
        }
        
        void TestInheritance1()
        {
            A a1 = new A();
            a1.foo();
            a1.bar();
            // a1.uhh(); cannot uhh due to protection level

            B b1 = new B(); // 
            b1.foo();
            b1.bar();
            
            // B b1 = new A();
            
            A a2 = new B();
            a2.foo();
            a2.bar();
        }

        #endregion

        #region Inheritance2

        public interface IC
        {
            void foo();
            void bar();
        }
        
        public abstract class C : IC
        {
            public abstract int Value { get; }

            public abstract void foo();
            public void bar()
            {
                print("C bar");
            }
        }

        public class D : C
        {
            public override int Value => 5;
            public override void foo()
            {
                print("D foo");
            }
        }

        void TestInheritance2()
        {
            // C c1 = C() // cannot construct a C as it is abstract
            D d1 = new D();
            d1.foo();
            d1.bar();
            
            C c1 = new D();
            c1.foo();
            c1.bar();

            IC c2 = new D();
            c2.foo();
            c2.bar();
        }
        
        #endregion

        #region Generics 
        
        class AG<A>
        {
            public A Value;
        }
        
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
        class BG<T,U,V> 
            where T : IC
            where U : struct 
            where V : new() // must have a default constructor
        {
            BG(T t)
            {
                t.foo();
                var f = new V();
            }
        }

        class DoSomethinger<T>
        {
            private T i;
            DoSomethinger(T I)
            {
                
            }
             
            public T GetI() {return i; }

            public void SetI(T v)
            {
                i = v;
                DoSomething(i);
            }
            private void DoSomething(T v) { print(v.ToString()); }
        }
        
        
        #endregion

        #region StructVClass

        public struct Srt
        {
            public string Name;
            public int Value;

            public Srt(string n, int v)
            {
                Name = n;
                Value = v;
            }
            
            public override string ToString()
            {
                return $"STRUCT: {Name}: {Value}";
            }
        }

        public class Cls
        {
            public string Name;
            public int Value;

            public Cls(string n, int v)
            {
                Name = n;
                Value = v;
            }

            public override string ToString()
            {
                return $"CLASS: {Name}: {Value}";
            }
        }

        void TextStructVClass()
        {
            var st1 = new Srt("Struct", 0);
            var cl1 = new Cls("Class", 0);
            
            print(st1);
            print(cl1);
            
            var st2 = st1;
            var cl2 = cl1;

            cl2.Value = 1;
            st2.Value = 1;
            
            print(st1 + " ?= " + st2);
            print(cl1 + " ?= " + cl2);
            
            // Passing structs into fucntions makes a copy, which is inherently more expensive
        }

        #endregion
        
        #region RefAndOut

        private int _genCount = 0;
        void Generate(out Srt s, out string id)
        {
            id = "new_name " + _genCount;
            s = new Srt(id, _genCount++);
        }
        
        void Modify(ref Srt s, ref int val)
        {
            s.Value = val;
            val = -1;
        }
        
        private void TestRefAndOut()
        {
            string id;
            Srt newSrt;
            Generate(out newSrt, out id);
            print(id + " :  " + newSrt);

            int newVal = -100;
            Modify(ref newSrt, ref newVal);
            print(newSrt + " :  newVal new value:" + newVal);
        }
        
        #endregion
        
        #region Properties

        public int Prop1 { set; get; }

        public int Prop2 { private set; get; }

        
        private int _prop3 = -1;
        public int Prop3
        {
            set
            {
                _prop3 = value;
                print("SET PROP3 to " + value);
            }
            get
            {
                print("GET PROP3 with value " + _prop3);
                return _prop3;
            }
        }
            
        public int Prop4 => _prop3;
        private void TestProperties()
        {
            Prop1 = 2;
            
            print(Prop1);

            Prop2 = 3; // We can do this because we are in the right permisions
            print(Prop2);

            Prop3 = 4;
            print(Prop3);

            // Prop4 = 5; // cant has no set
            print(Prop4);
            _prop3 = 5;
            print(Prop4);
        }

        
        #endregion
        
        #region Delegates

        public delegate int MyDelegate(bool b, byte by, string s);

        int MyDelegateExample(bool b, byte by, string s)
        {
                if (b) print(s + "MYDELLEGATE EXAMPLE");
            return -(int) by;
        }
        
        int MyDelegateExample2(bool b, byte by, string s)
        {
            if (b) print(s + "MYDELLEGATE2 EXAMPLE");
            return (int) by;
        }
        // Events only invocable in original context
        public event MyDelegate Md;
        
        private void TestDelegates()
        {
            Md = MyDelegateExample;
            Md(true, 1, "hi");
            
            Md = MyDelegateExample2;
            Md(true, 1, "hi");

            Md += MyDelegateExample;
            Md(true, 1, "hi");
            //
            Md -= MyDelegateExample2;
            Md(true, 1, "hi");
            
            Md += MyDelegateExample;

            MyDelegate d = (a, b, c) =>
            {
                if (a)
                {
                    print(c);
                }

                return (int) b;
            };
            
            
            print(d(true, 1, "d1 example string"));

            Md = d;

            print(Md(true, 2, "d1 example string event"));

            Md += (bool a, byte b, string c) =>
            {
                print(c);
                int ret = (int) b;
                if (a) ret *= -1;

                return ret;
            };

            print(Md(true, 3, "d2 example string"));

            Md += MyDelegateExample;
            print(Md(false, 4, "d3 example string"));

            Md -= d;
            print(Md(true, 5, "d4 example string"));

            Action<int> t = (i) => { };
            
            Func<bool, byte, string, int> f;
            f = (a, b, c) =>
            {
                if (a)
                {
                    print(c);
                }

                return (int) b;
            };
        }
        
        #endregion
        
        void Start()
        {
            TestInheritance1();

            TestInheritance2();

            TextStructVClass();

            TestRefAndOut();

            TestProperties();

            TestDelegates();
        }
    }
}
