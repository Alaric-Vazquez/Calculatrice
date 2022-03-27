using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculatrice
{
    public class Calculator
    {
        private Model model;
        public Calculator(ref Model _model) 
        {
            model = _model;
        }
        public float Calculate(string userInput)
        {
            model.operationsList = SaveUserInputIntoList(userInput);
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
            model.result = result;
            return result;
        }
        private List<string> PriorityCalculs(List<string> calculs)
        {
            float fNbr = 0;
            float sNbr = 0;
            float res= 0;
            bool isAdd = false;
            int count = calculs.Count;

            List<string> newCalcul = new List<string>();

            for (int i = 0; i < count; i++)
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
                    CheckIfSecondNumberExist(next, count - 1, newCalcul.ElementAt(newCalcul.Count - 1), calculs.ElementAt(i));

                    fNbr = InitFirstNumber(isAdd, calculs, newCalcul, i);
                    sNbr = float.Parse(calculs.ElementAt(next));

                    CheckDivision(s, sNbr);

                    res = Calcul(fNbr, sNbr, calculs.ElementAt(i));
                    ReplaceLastIndexByResult(ref newCalcul, res);
                    isAdd = false;
                    i++;
                }
            }
            return newCalcul;
        }
        private void ReplaceLastIndexByResult(ref List<string> newCalcul, float res)
        {
            newCalcul.RemoveAt(newCalcul.Count - 1);
            newCalcul.Add(res.ToString());
        }
        private float InitFirstNumber(bool isAdd, List<string> calculs, List<string> newCalcul, int i)
        {
            if (isAdd) // verify if the last calcul was an addition/substraction or not and put to fNbr the right number
            {
                return float.Parse(calculs.ElementAt(i - 1));
            }
            return float.Parse(newCalcul.ElementAt(newCalcul.Count - 1));
        }
        private void AppendBuffer(ref string nbr, ref List<string> allCalcul)
        {
            if (!string.IsNullOrWhiteSpace(nbr))
            {
                allCalcul.Add(nbr);
                nbr = string.Empty;
            }
        }
        private List<string> SaveUserInputIntoList(string s)
        {
            string nbr = string.Empty;
            char prevChar = ' ';
            List<string> operationsList = new List<string>();
            if (model.result == 0)
            {
                CheckFirstIndex(s);
            }

            for (int i = 0; i < s.Length ; i++)
            {
                char indexChar = s.ElementAt(i);
                if (i == 0 && IsAdditionOrSubstraction(indexChar.ToString()) || i == 0 && IsMultiplicationOrDivision(indexChar.ToString()))
                {
                    operationsList.Insert(0, model.result.ToString());
                }
                if (model.additionSymbols.Contains(indexChar) || model.multiplySymbols.Contains(indexChar))
                {
                    if (!string.IsNullOrWhiteSpace(prevChar.ToString()))
                    {
                        CheckSyntax(ref indexChar, ref prevChar, ref operationsList);
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
                    throw new ControllerException("Invalid number --> " + indexChar);
                }
                prevChar = indexChar;
            }
            AppendBuffer(ref nbr, ref operationsList);
            return operationsList;
        }
        private void CheckIfSecondNumberExist(int next, int count, string nbr, string symbol)
        {
            if(next > count)
            {
                throw new ControllerException("Invalid syntax --> " + nbr + symbol);
            }
        }
        private void CheckFirstIndex(string s)
        {
            if (IsMultiplicationOrDivision(s.ElementAt(0).ToString()) && model.result == 0)
            {
                throw new ControllerException("You can't begin with a multiplication or a division.");
            }
        }
        private void CheckDivision(string s, float sNbr)
        {
            if (s == "/" && sNbr == 0)
                throw new ControllerException("You can't divide by 0.");
        }
        private void CheckSyntax(ref char indexChar, ref char prevChar, ref List<string> operationsList)
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
                    throw new ControllerException("Invalid syntax --> " + prevChar + " " + indexChar);
                }
            }
        }
    }
}