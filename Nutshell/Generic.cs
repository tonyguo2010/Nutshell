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

    class InitialEntity
    {
        public int iAge { get; set; }
        public string sName { get; set; }
        public string sDescription { get; set; }
        public override string ToString()
        {
            return string.Format("iAge : {0}\nsName : {1}\niReadOnly : {2}", iAge, sName, iReadOnly);
        }
        public InitialEntity() { iReadOnly = 11; }
        public InitialEntity(int iAge, string sName, int iReadOnly = 11)
        {
            this.iAge = iAge;
            this.sName = sName;
            this.iReadOnly = iReadOnly;
        }
        public string this [int indexer]
        {
            get
            {
                string[] NameContent = sDescription.Split();
                return NameContent[indexer];
            }

            set
            {
                string[] Content = sDescription.Split();
                Content[indexer] = value;
                StringBuilder Tmp = new StringBuilder();
                foreach (string Unit in Content)
                {
                    Tmp.Append(Unit).Append(" ");
                }
                sDescription = Tmp.ToString();
            }
            
        }
        private readonly int iReadOnly;
        private const int iConst = 10;
        ~InitialEntity()
        {
            Console.WriteLine("end...");
        }
    }

    partial class PartialEntity
    {
        public override string ToString()
        {
            OutputSomething();
            return "Type is " + this.GetType().ToString() + " and " + typeof(PartialEntity).ToString();
        }

        partial void OutputSomething();
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

        public static void TestInitialization()
        {
            InitialEntity cEntity1 = new InitialEntity { iAge = 9, sName = "Cici", sDescription="Cici was borned in China" };
            Console.WriteLine(cEntity1.ToString());

            InitialEntity cEntity2 = new InitialEntity(iAge: 3, sName: "Kiki");
            Console.WriteLine(cEntity2.ToString());

            for (int iLoop = 0; iLoop < cEntity1.sDescription.Split().Length; iLoop++)
            {
                Console.WriteLine(cEntity1[iLoop]);
            }

            cEntity1[3] = "at";
            Console.WriteLine(cEntity1.sDescription);

            Console.WriteLine("*******\nTry to convert {0} \nand assert {1}\n******",
                cEntity1 as InitialEntity, cEntity2 is object);

        }

        public static void TestPartial()
        {
            PartialEntity Entity = new PartialEntity();

            Console.WriteLine(Entity.ToString());
        }
    }
}
