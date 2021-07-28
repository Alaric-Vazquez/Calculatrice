using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculatrice
{
    class Calculator
    {
        public View view;
        public Model model;
        public Controller controller;

        public Calculator()
        {
            view = new View();
            model = new Model();
            controller = new Controller(ref model);
        }

        static bool isRunning = true;
        public void run()
        {
            ProjectInit();

            while (isRunning)
            {
                try
                {
                    ProjectLoop();
                }
                catch (CalculatorExceptionDoubleSymbol e)
                {
                    view.DisplayLine(e.Message);
                }
                catch (CalculatorExceptionDivisionByZero e)
                {
                    view.DisplayLine(e.Message);
                }
                catch (Exception e)
                {
                    view.DisplayLine(e.Message);
                }
            }
        }

        void ProjectInit()
        {
            view.DisplayLine("Calculatrice :");
        }
        void ProjectLoop()
        {
            view.Display(">");
            string userInput = view.GetUserInput();
            float result = controller.Calculate(userInput);
            view.DisplayLine(result.ToString());
        }
    }
}
