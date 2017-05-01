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
}
