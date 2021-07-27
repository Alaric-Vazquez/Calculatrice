using System;
using System.Collections.Generic;
using System.Text;

namespace Calculatrice
{
    class Model
    {
        public static List<string> operationsList = new List<string>();
        public static List<string> additionList = new List<string>();
        public static string[] additionSymbols = new string[] { "+", "-" };
        public static string[] multiplySymbols = new string[] { "*", "x", "/" };
        public static string[] authorizedNumber = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", };
    }
}
