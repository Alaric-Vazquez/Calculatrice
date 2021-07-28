using System;

namespace Calculatrice
{
    class Calculator
    {
        private View view;
        private Model model;
        private Controller controller;
        private bool isRunning = true;

        public Calculator()
        {
            view = new View();
            model = new Model();
            controller = new Controller(ref model);
        }

        public void run()
        {
            ProjectInit();

            while (isRunning)
            {
                try
                {
                    ProjectLoop();
                }
                catch(ControllerQuitException e)
                {
                    isRunning = false;
                }
                catch (ControllerException e)
                {
                    view.DisplayLine(e.Message);
                }
                catch (Exception e)
                {
                    view.DisplayLine(e.Message);
                    Environment.Exit(1);
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
            VerifyIfUserQuits(userInput);
            
            float result = controller.Calculate(userInput);
            view.DisplayLine(result.ToString());
        }
        void VerifyIfUserQuits(string userInput)
        {
            if (string.Compare(userInput, "q") == 0)
            {
                throw new ControllerQuitException();
            }
        }
    }
}
