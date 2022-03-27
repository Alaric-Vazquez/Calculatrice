using System;
using System.Collections.Generic;
using System.Text;

namespace Calculatrice
{
    public class Model
    {
        public float result;
        public List<string> operationsList = new List<string>();
        public List<string> additionList = new List<string>();
        public readonly char[] additionSymbols = new char[] { '+', '-' };
        public readonly char[] multiplySymbols = new char[] { '*', 'x', '/' };
        public readonly char[] authorizedNumber = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
    }
}
