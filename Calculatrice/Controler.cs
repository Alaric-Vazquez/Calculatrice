using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculatrice
{
    class Controler
    {
        public static float result;

        static bool IsAddition(string s)
        {
            if (Model.additionSymbols.Contains(s)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static bool IsMultiplication(string s)
        {
            if (Model.multiplySymbols.Contains(s)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static float Calcul(float fNbr, float sNbr, string s)
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
                    return 0;
            }
        }

        public static float AdditionCalcul(List<string> additionList)
        {
            float sNbr;
            float result = 0;

            for (int i = 0; i < additionList.Count - 1; i++)
            {
                int next = i + 1;

                try
                {
                    if (i == 0 && !IsAddition(additionList.ElementAt(0)))
                    {
                        sNbr = float.Parse(additionList.ElementAt(i));
                        result += sNbr;
                    }
                    else if (IsAddition(additionList[i]) && additionList.ElementAt(i) == "+")
                    {
                        sNbr = float.Parse(additionList.ElementAt(next));
                        float resCal = Calcul(result, sNbr, additionList.ElementAt(i));
                        result = resCal;
                    }
                    else if (IsAddition(additionList[i]) && additionList.ElementAt(i) == "+")
                    {
                        sNbr = float.Parse(additionList.ElementAt(next));
                        float resCal = Calcul(result, sNbr, additionList.ElementAt(i));
                        result = resCal;
                    }
                }
                catch (Exception e)
                {
                    View.DisplayException(e);
                }
            }
            return result;
        }

        public static List<string> PriorityCalculs(List<string> calculs)
        {
            float fNbr;
            float sNbr;
            float res;
            bool isAdd = false;

            List<string> newCalcul = new List<string>();

            for (int i = 0; i < calculs.Count; i++)
            {
                int prev = i - 1;
                int next = i + 1;
                string s = calculs.ElementAt(i);
                try
                {
                    if (!IsAddition(calculs[i]))
                    {
                        newCalcul.Add(s);
                    } 
                    else if (IsAddition(calculs[i]))
                    {
                        newCalcul.Add(s);
                        isAdd = true;
                    }
                    else if (IsMultiplication(calculs[i]))
                    {
                        if (isAdd) // verify if the last calcul was an addition/substraction or not and put to fNbr the right number
                        {
                            fNbr = float.Parse(calculs.ElementAt(prev));
                        }
                        else
                        {
                            fNbr = float.Parse(newCalcul.ElementAt(newCalcul.Count - 1));
                        }

                        sNbr = float.Parse(calculs.ElementAt(next));
                        res = Calcul(fNbr, sNbr, calculs.ElementAt(i));
                        newCalcul.RemoveAt(newCalcul.Count - 1);
                        newCalcul.Add(res.ToString());
                        isAdd = false;
                        i++;
                    }
                }
                catch(Exception e)
                {
                    View.DisplayException(e);
                }
            }
            return newCalcul;
        }
        static void AppendBuffer(ref string nbr, ref List<string> allCalcul)
        {
            if (!string.IsNullOrWhiteSpace(nbr))
            {
                allCalcul.Add(nbr);
                nbr = string.Empty;
            }
        }
        public static List<string> SaveStringIntoList(string s)
        {
            string nbr = string.Empty;
            List<string> operationsList = new List<string>();

            foreach (char c in s)
            {
                if(Model.additionSymbols.Contains(c.ToString()) || Model.multiplySymbols.Contains(c.ToString()))
                {
                    AppendBuffer(ref nbr, ref operationsList);
                    operationsList.Add(c.ToString());
                }
                else if (Model.isNumber.Contains(c.ToString()))
                {
                    nbr += c;
                }
            }
            return operationsList;
        }
    }
}
