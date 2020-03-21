using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DifferentStepExample
{
    [TestClass]
    public partial class CommonComponentTest
    {
        [TestMethod]
        public void ComponentTest()
        {
            CommonStep1();
            DifferentStep();
            CommonStep2();
        }

        private void CommonStep1()
        {
            Console.WriteLine("Common step 1");
        }

        private void CommonStep2()
        {
            Console.WriteLine("Common step 2");
        }
    }
}
