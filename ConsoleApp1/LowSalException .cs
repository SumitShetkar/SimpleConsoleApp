using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class LowSalException : Exception
    {
        public LowSalException() { }

        public LowSalException(string name)
            : base(String.Format("plase set the basic above 500 for emp ", name))
        {

        }
    }
}
