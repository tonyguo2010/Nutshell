using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nutshell.Advanced;

namespace Nutshell
{
    class Program
    {
        static void Main(string[] args)
        {
            //            Generic.Test.TestPassByReference();
            //            Generic.Test.TestPassByValue();
            // Streams.StandardStream.Test();
            // Generic.Test.TestNewSubMember();
            // Streams.StreamSamples.Test();
            DelegateExample Tester = new DelegateExample();
            Tester.Work();
        }
    }
}
