using System;
using System.Collections.Generic;
using System.Text;

namespace Calculatrice
{
    public class Model
    {
        public List<string> operationsList = new List<string>();
        public List<string> additionList = new List<string>();
        public char[] additionSymbols = new char[] { '+', '-' };
        public char[] multiplySymbols = new char[] { '*', 'x', '/' };
        public char[] authorizedNumber = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',' };
    }
}
