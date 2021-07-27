using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculatrice
{
    class Calculator
    {
        static bool isRunning = true;
        public static void run()
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
                    View.DisplayLine(e.Message);
                }
                catch (CalculatorExceptionDivisionByZero e)
                {
                    View.DisplayLine(e.Message);
                }
                catch (Exception e)
                {
                    View.DisplayLine(e.Message);
                }
            }
        }

        static void ProjectInit()
        {
            View.DisplayLine("Calculatrice :");
        }
        static void ProjectLoop()
        {
            string response;

            View.Display(">");

            response = View.GetUserInput();

            Model.operationsList = Controller.SaveStringIntoList(response);
            Model.additionList = Controller.PriorityCalculs(Model.operationsList);

            if (Model.additionList.Count < 2)
            {
                if (Controller.IsStringNumber(Model.additionList.ElementAt(0)))
                {
                    Controller.result = float.Parse(Model.additionList.ElementAt(0));
                    View.DisplayLine(Controller.result.ToString());
                }
                return;
            }

            Controller.result = Controller.AdditionCalcul(Model.additionList);
            View.DisplayLine(Controller.result.ToString());
        }
    }
}
