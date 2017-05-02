using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Generic
{
    partial class PartialEntity
    {
        partial void OutputSomething()
        {
            StackTrace st = new StackTrace(true);

            foreach(StackFrame sf in st.GetFrames())
            {
                Console.WriteLine("I am at {0} of {1}", 
                    sf.GetMethod().ToString(), sf.GetFileName());
            }

        }
    }

    class InheritBase
    {
        public int iValue { get; set; }
        public virtual void Output()
        {
            Console.WriteLine("The internal value is {0}", iValue);
        }
    }

    class InheritSub : InheritBase
    {
        public new int iValue { get; set; }
        public new void Output()
        {
            Console.WriteLine("The new output is {0}", iValue);
        }
    }
    
    class InheritSeal : InheritBase
    {
        public sealed override void Output()
        {
            Console.WriteLine("Sealed function now.");
            base.Output();
        }
    }

    interface InterBase1
    {
        void Output();
    }

    interface InterBase2
    {
        void Output();
    }

    class ImplInstance : InterBase1, InterBase2
    {
        public void Output()
        {
            Console.WriteLine("Inter1");
        }

        void InterBase2.Output()
        {
            Console.WriteLine("Inter2");
        }
    }

    class NestOut
    {
        public class NestInside
        {
            public static void Output()
            {
                NestOut.Output();
            }
        }

        static void Output()
        {
            Console.WriteLine("World inside");
        }
    }

    partial class Test
    {
        public static void TestNewSubMember()
        {
            InheritBase TheBase = new InheritBase { iValue = 10 };
            InheritSub TheSub = new InheritSub { iValue = 12 };

            Console.WriteLine("The value is {0} and {1}", TheBase.iValue, TheSub.iValue);

            InheritBase TheFakeBase = TheSub as InheritBase;
            TheFakeBase.Output();
            TheSub.Output();

            Console.WriteLine("************************************");
            TheFakeBase.iValue = 5;
            TheFakeBase.Output();
            TheSub.Output();

            Console.WriteLine("************************************");
            InheritSeal TheSeal = new InheritSeal { iValue = 9 };

            TheSeal.Output();
            (TheSeal as InheritBase).Output();

            Console.WriteLine("************************************");
            ImplInstance inst = new ImplInstance();

            inst.Output();
            (inst as InterBase2).Output();

            Console.WriteLine("************************************");
            NestOut.NestInside.Output();
        }
    }
}
