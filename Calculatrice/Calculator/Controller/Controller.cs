using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculatrice
{
    public class Controller
    {
        private Model model;
        public Controller(ref Model _model) 
        {
            model = _model;
        }
        public float Calculate(string userInput)
        {
            model.operationsList = SaveStringIntoList(userInput);
            model.additionList = PriorityCalculs(model.operationsList);
            return AdditionCalcul(model.additionList);
        }
        private bool IsAdditionOrSubstraction(string s)
        {
            if(s.Length > 1)
            {
                return false;
            }
            char c = char.Parse(s);
            return model.additionSymbols.Contains(c);
        }
        private bool IsMultiplicationOrDivision(string s)
        {
            if (s.Length > 1)
            {
                return false;
            }
            char c = char.Parse(s);
            return model.multiplySymbols.Contains(c);
        }
        private bool IsNumber(char c)
        {
            return model.authorizedNumber.Contains(c);
        }
        private bool IsStringNumber(string s)
        {
            foreach(char c in s)
            {
                if (!IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        private float Calcul(float fNbr, float sNbr, string s)
        {
            switch (s)
            {
                case "+":
                    return fNbr + sNbr;
                case "-":
                    return fNbr - sNbr;
                case "*":
                    return fNbr * sNbr;
                case "x":
                    return fNbr * sNbr;
                case "/":
                    return fNbr / sNbr;
                default:
                    throw new ControllerException("Forgoten operator.");
            }
        }
        private float AdditionCalcul(List<string> additionList)
        {
            float sNbr = 0;
            float result = 0;
            if (model.additionList.Count < 2)
            {
                return float.Parse(model.additionList.ElementAt(0));
            }

            for (int i = 0; i < additionList.Count - 1; i++)
            {
                int next = i + 1;
                
                if (i == 0 && !IsAdditionOrSubstraction(additionList.ElementAt(0)))
                {
                    sNbr = float.Parse(additionList.ElementAt(i));
                    result += sNbr;
                }
                else if (IsAdditionOrSubstraction(additionList.ElementAt(i)))
                {
                    sNbr = float.Parse(additionList.ElementAt(next));
                    float resCal = Calcul(result, sNbr, additionList.ElementAt(i));
                    result = resCal;
                }
            }
            return result;
        }
        private List<string> PriorityCalculs(List<string> calculs)
        {
            float fNbr = 0;
            float sNbr = 0;
            float res= 0;
            bool isAdd = false;

            List<string> newCalcul = new List<string>();

            for (int i = 0; i < calculs.Count; i++)
            {
                int next = i + 1;
                string s = calculs.ElementAt(i);
                if (IsStringNumber(s))
                {
                    newCalcul.Add(s);
                }
                else if (IsAdditionOrSubstraction(s))
                {
                    newCalcul.Add(s);
                    isAdd = true;
                }
                else if (IsMultiplicationOrDivision(s))
                {
                    if (isAdd) // verify if the last calcul was an addition/substraction or not and put to fNbr the right number
                    {
                        fNbr = float.Parse(calculs.ElementAt(i - 1));
                    }
                    else
                    {
                        fNbr = float.Parse(newCalcul.ElementAt(newCalcul.Count - 1));
                    }

                    sNbr = float.Parse(calculs.ElementAt(next));

                    ValidateDivision(s, sNbr);

                    res = Calcul(fNbr, sNbr, calculs.ElementAt(i));
                    if(newCalcul.Count > 0)
                    {
                        newCalcul.RemoveAt(newCalcul.Count - 1);
                    }
                    newCalcul.Add(res.ToString());
                    isAdd = false;
                    i++;
                }
            }
            return newCalcul;
        }
        private void AppendBuffer(ref string nbr, ref List<string> allCalcul)
        {
            if (!string.IsNullOrWhiteSpace(nbr))
            {
                allCalcul.Add(nbr);
                nbr = string.Empty;
            }
        }
        private List<string> SaveStringIntoList(string s)
        {
            string nbr = string.Empty;
            char prevChar = ' ';
            List<string> operationsList = new List<string>();

            foreach (char c in s)
            {
                char indexChar = c;
                if (model.additionSymbols.Contains(c) || model.multiplySymbols.Contains(c))
                {
                    if (!string.IsNullOrWhiteSpace(prevChar.ToString()))
                    {
                        ValidateSyntax(ref indexChar, ref prevChar, ref operationsList);
                    }
                    AppendBuffer(ref nbr, ref operationsList);
                    operationsList.Add(indexChar.ToString());
                }
                else if (IsNumber(indexChar))
                {
                    nbr += indexChar;
                }
                else
                {
                    throw new ControllerException("Invalid number: " + indexChar);
                }
                prevChar = indexChar;
            }
            AppendBuffer(ref nbr, ref operationsList);
            return operationsList;
        }
        private void ValidateDivision(string s, float sNbr)
        {
            if (s == "/" && sNbr == 0)
                throw new ControllerException("Vous ne pouvez pas Diviser par 0");
        }
        private void ValidateSyntax(ref char indexChar, ref char prevChar, ref List<string> operationsList)
        {
            //verify if there is a double symbol and if the calculation can still be done
            {
                int i = operationsList.Count - 1;
                if (prevChar == '+' && indexChar == '+')
                {
                    operationsList.RemoveAt(i);
                }
                else if(prevChar == '+' && indexChar == '-')
                {
                    operationsList.RemoveAt(i);
                }
                else if (prevChar == '-' && indexChar == '+')
                {
                    operationsList.RemoveAt(i);
                    indexChar = '-';
                }
                else if (prevChar == '-' && indexChar == '-')
                {
                    operationsList.RemoveAt(i);
                    indexChar = '+';
                }
                else if (IsMultiplicationOrDivision(prevChar.ToString()) && IsMultiplicationOrDivision(indexChar.ToString()))
                {
                    throw new ControllerException(prevChar + " " + indexChar + " ");
                }
            }
        }
    }
}