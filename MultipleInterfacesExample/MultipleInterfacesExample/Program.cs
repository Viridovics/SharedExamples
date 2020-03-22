using System;
using Unity;
using Unity.Lifetime;

namespace MultipleInterfacesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IProductComponentSettings, ProductComponentSettings>(new ContainerControlledLifetimeManager());
            container.RegisterType<IComponentSettings, ProductComponentSettings>(new ContainerControlledLifetimeManager());
            var obj = container.Resolve<IComponentSettings>();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
