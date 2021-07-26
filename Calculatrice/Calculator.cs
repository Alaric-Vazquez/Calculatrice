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

            Model.operationsList = Controler.SaveStringIntoList(response);
            Model.additionList = Controler.PriorityCalculs(Model.operationsList);

            if (Model.operationsList.Count < 2)
            {
                Controler.result = float.Parse(Model.operationsList.ElementAt(0));
                View.DisplayLine(Controler.result.ToString());
                return;
            }

            Controler.result = Controler.AdditionCalcul(Model.additionList);
            View.DisplayLine(Controler.result.ToString());
        }
    }
}
