using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculatrice
{
    class Program
    {
        static bool isRunning = true;
        static bool error;

        static void Main(string[] args)
        {
            ProjectInit();

            while (isRunning)
            {
                ProjectLoop();
            }
        }

        // present app
        static void ProjectInit()
        {   
            Console.WriteLine("Calculatrice :");
        }

        //Main Loop
        static void ProjectLoop()
        {
            string response;
            float result;

            Console.Write(">");
            
            response = Console.ReadLine().Replace('.', ','); // Get response

            List<string> allCalcul = GetCalculs(response); // parse and put response on array
            List<string> endCalculs = PriorityCalculs(allCalcul); //calcul all mult and div -> get only addition and substraction

            if (endCalculs.Count == 1 && !error)
            {
                result = float.Parse(endCalculs.ElementAt(0));
                Console.WriteLine(result);
                return;
            }
            else if (error)
            {
                Console.WriteLine("Erreur syntaxe");
                return;
            }

            result = CalculAll(endCalculs);
            Console.WriteLine(result);
        }

        // General Calculator
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

        // Only calcul multiplication and division ---> return a List of addition and subtraction
        static List<string> PriorityCalculs(List<string> calculs) 
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
                    switch (s)
                {
                    case "+":
                    case "-":
                        if (IsSign(calculs.ElementAt(next)))
                        {
                            error = true;
                            return newCalcul;
                        }
                        newCalcul.Add(s);
                        break;
                    case "*":
                    case "x":
                    case "/":
                        if (i == 0 || IsSign(calculs.ElementAt(next)))
                        {
                            error = true;
                            return newCalcul;
                        }
                        fNbr = float.Parse(calculs.ElementAt(prev));
                        sNbr = float.Parse(calculs.ElementAt(next));
                        if (isAdd)
                        {
                            res = Calcul(fNbr, sNbr, calculs.ElementAt(i));
                            newCalcul.RemoveAt(newCalcul.Count - 1);
                            newCalcul.Add(res.ToString());
                            isAdd = false;
                            i++;
                        }
                        else
                        {
                            fNbr = float.Parse(newCalcul.ElementAt(newCalcul.Count -1));
                            res = Calcul(fNbr, sNbr, calculs.ElementAt(i));
                            newCalcul.RemoveAt(newCalcul.Count - 1);
                            newCalcul.Add(res.ToString());
                            isAdd = false;
                            i++;
                        }
                        break;
                    default:
                        newCalcul.Add(s);
                        isAdd = true;
                        break;
                }
            }
            return newCalcul;
        }

        // FINAL Calcul
        static float CalculAll(List<string> calculs)
        {
            float sNbr;
            float result = 0;

            for (int i = 0; i < calculs.Count-1; i++)
            {
                int next = i + 1;
                if (i == 0 && IsSign(calculs.ElementAt(i)))   // Verif if first index is a sign
                {
                    sNbr = float.Parse(calculs.ElementAt(next));
                    if (calculs.ElementAt(0) == "-")
                    {
                        result -= sNbr;
                    }
                    else if (calculs.ElementAt(0) == "+")
                    {
                        result += sNbr;
                    }
                    else
                    {
                        Console.WriteLine("Erreur syntaxe");
                        return 0;
                    }
                }
                else if(i == 0 && !IsSign(calculs.ElementAt(i)))
                {
                    sNbr = float.Parse(calculs.ElementAt(i));
                    result += sNbr;
                }
                else if (IsSign(calculs.ElementAt(i)))
                {
                    sNbr = float.Parse(calculs.ElementAt(next));

                    float resCal = Calcul(result, sNbr, calculs.ElementAt(i));
                    result = resCal;
                }
            }
            return result;
        }

        //verif if a string is a Sign
        static bool IsSign(string s)
        {
            switch (s)
            {
                case "+":
                case "-":
                case "*":
                case "x":
                case "/":
                    return true;
                default:
                    return false;
            }
        }

        //add a stringNumber on StringList
        static void AppendBuffer(ref string nbr, ref List<string> allCalcul)
        {
            if (!string.IsNullOrWhiteSpace(nbr))
            {
                allCalcul.Add(nbr);
                nbr = string.Empty;
            }
        }


        //Split the string response on a list of numbers ans signs
        static List<string> GetCalculs(string s)
        {
            string nbr = string.Empty;
            List<string> allCalcul = new List<string>();

            foreach (char c in s)
            {
                switch (c)
                {
                    case '+': 
                    case '-':
                    case '*':
                    case 'x':
                    case '/':
                        AppendBuffer(ref nbr, ref allCalcul);
                        allCalcul.Add(c.ToString());
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        nbr += c;
                        break;
                    default:
                        AppendBuffer(ref nbr, ref allCalcul);
                        break;
                }
            }
            AppendBuffer(ref nbr, ref allCalcul);
            return allCalcul;
        }
    }
}
