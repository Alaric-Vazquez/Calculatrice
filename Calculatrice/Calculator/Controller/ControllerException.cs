using System;
using System.Collections.Generic;
using System.Text;

namespace Calculatrice
{
    class ControllerException : Exception
    {
        public ControllerException() { }

        public ControllerException(string s)
            :base(String.Format("Error: {0}", s))
        {

        }
    }
    class ControllerQuitException : ControllerException
    {
        public ControllerQuitException() { }
    }
    //class CalculatorBadInputException : CalculatorException
    //{
    //    public CalculatorBadInputException() { }

    //    public CalculatorBadInputException(string s)
    //        : base(s)
    //    {

    //    }
    //}
}
