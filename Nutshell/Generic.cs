using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Generic
{
    class PassByReference
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    struct PassByValue
    {
        public int X;
        public int Y;
    }

    class Test
    {
        public static void TestPassByReference()
        {
            PassByReference class1 = new PassByReference();

            class1.X = 30;
            class1.Y = 40;

            PassByReference class2 = class1;

            Console.WriteLine("Before: \n\tclass1->({0}, {1}) \n\tclass2->({2}, {3})\n", 
                class1.X, class1.Y,
                class2.X, class2.Y);

            class2.Y = 50;

            Console.WriteLine("After: \n\tclass1->({0}, {1}) \n\tclass2->({2}, {3})\n",
                class1.X, class1.Y,
                class2.X, class2.Y);
        }

        public static void TestPassByValue()
        {
            PassByValue struct1 = new PassByValue();

            struct1.X = 30;
            struct1.Y = 40;

            PassByValue struct2 = struct1;

            Console.WriteLine("Before: \n\tstruct1->({0}, {1}) \n\tstruct2->({2}, {3})\n",
                struct1.X, struct1.Y,
                struct2.X, struct2.Y);

            struct2.Y = 50;

            Console.WriteLine("After: \n\tstruct1->({0}, {1}) \n\tstruct2->({2}, {3})\n",
                struct1.X, struct1.Y,
                struct2.X, struct2.Y);
        }
    }
}
