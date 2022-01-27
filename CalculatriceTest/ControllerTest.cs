using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculatrice;

namespace CalculatriceTest
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public void Test()
        {
            Model model = new Model();
            Calculator controller = new Calculator(ref model);
            //PrivateObject obj = new PrivateObject(controller);
            //obj.Invoke("IsAdditionOrSubstraction", "");
        }
    }
}
