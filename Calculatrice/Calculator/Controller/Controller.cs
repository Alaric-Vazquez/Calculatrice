using System;

namespace Calculatrice
{
    public class Controller
    {
        private View view;
        private Model model;
        private Calculator controller;
        private bool isRunning = true;

        public Controller()
        {
            view = new View();
            model = new Model();
            controller = new Calculator(ref model);
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
                catch (ControllerClearException e)
                {
                    Console.Clear();
                    ProjectInit();
                    model.result = 0;
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
            VerifyIfUserQuitsOrClear(userInput);

            float result = controller.Calculate(userInput);
            model.result = result;
            view.DisplayLine(result.ToString());
        }
        void VerifyIfUserQuitsOrClear(string userInput)
        {
            if (string.Compare(userInput, "q") == 0)
            {
                throw new ControllerQuitException();
            } 
            else if(string.Compare(userInput, "c") == 0)
            {
                throw new ControllerClearException();
            }
        }
    }
}
