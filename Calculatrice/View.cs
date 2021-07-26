using System;

namespace Calculatrice
{
    class View
    {
        public static void Display(string s)
        {
            Console.Write(s);
        }
        public static void DisplayLine(string s)
        {
            Console.WriteLine(s);
        }

        public static void DisplayException(Exception e)
        {
            Console.WriteLine(e);
        }

        public static string GetUserInput()
        {
            return Console.ReadLine().Replace('.', ',');
        }
    }
}
