using System;
using System.Collections.Generic;
using System.Text;

namespace Calculatrice
{
    class CalculatorException : Exception
    {
        public CalculatorException() { }

        public CalculatorException(string s)
            :base(String.Format("Syntax error: {0}", s))
        {

        }
    }
    class CalculatorExceptionDivisionByZero : CalculatorException
    {
        public CalculatorExceptionDivisionByZero() { }

        public CalculatorExceptionDivisionByZero(string s)
            : base(s)
        {

        }
    }
    class CalculatorExceptionDoubleSymbol : CalculatorException
    {
        public CalculatorExceptionDoubleSymbol() { }

        public CalculatorExceptionDoubleSymbol(string s)
            : base(s)
        {

        }
    }
}
