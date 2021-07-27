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
                ProjectLoop();
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
            try
            {
                Model.operationsList = Controller.SaveStringIntoList(response);
                Model.additionList = Controller.PriorityCalculs(Model.operationsList);
            }
            catch (CalculatorExceptionDoubleSymbol e)
            {
                View.DisplayLine(e.Message);
                return;
            }
            catch(CalculatorExceptionDivisionByZero e)
            {
                View.DisplayLine(e.Message);
                return;
            }
            catch(Exception e)
            {
                View.DisplayLine(e.Message);
                return;
            }

            if (Model.additionList.Count < 2)
            {
                if (Controller.IsNumber(Model.additionList.ElementAt(0)))
                {
                    Controller.result = float.Parse(Model.operationsList.ElementAt(0));
                    View.DisplayLine(Controller.result.ToString());
                }
                return;
            }

            Controller.result = Controller.AdditionCalcul(Model.additionList);
            View.DisplayLine(Controller.result.ToString());
        }
    }
}
