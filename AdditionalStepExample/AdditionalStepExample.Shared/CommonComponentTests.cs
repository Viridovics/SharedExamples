using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdditionalStepExample.Shared
{
    [TestClass]
    public partial class CommonComponentTests
    {
        [TestMethod]
        public void ComponentTest()
        {
            CommonStep1();
#if Project2
            ExclusiveForProject2Step();
#endif
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
