using System;

namespace Calculatrice
{
    public class View
    {
        public View() { }
        public void Display(string s)
        {
            Console.Write(s);
        }
        public void DisplayLine(string s)
        {
            Console.WriteLine(s);
        }

        public void DisplayException(Exception e)
        {
            Console.WriteLine(e);
        }

        public string GetUserInput()
        {
            return Console.ReadLine().Replace('.', ',');
        }
    }
}
