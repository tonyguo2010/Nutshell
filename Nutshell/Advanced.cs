using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutshell.Advanced
{
    public delegate void OutputingFunc(string Caller);
    class DelegateExample
    {
        public event OutputingFunc handlers;

        private void Printing1(string Caller)
        {
            Console.WriteLine("Printing1 for {0}", Caller);
        }

        private void Printing2(string Caller)
        {
            Console.WriteLine("Printing2 for {0}", Caller);
        }

        public DelegateExample()
        {
            handlers += Printing1;
            handlers += Printing2;
        }

        public void Work()
        {
            handlers("Caller");
        }
    }
}
