using System;

namespace MultipleInterfacesExample
{
    class ProductComponentSettings : IProductComponentSettings
    {
        public void CommonMethod()
        {
            Console.WriteLine("It's a common method");
        }

        public void ParticularMethod()
        {
            Console.WriteLine("It's a particular method");
        }
    }
}
