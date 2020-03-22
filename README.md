# SharedExamples

Существует несколько сценариев, для которых использование Shared project уместно.

## Тестовый сценарий имеет шаг, который специфичен для конкретного продукта

Например, это может звучать так:
* Выполните шаг CommonStep1
* Только для второго продукта выполните ExclusiveForProject2Step
* Выполните шаг CommonStep2

В этом случае код тестового метода может выглядеть так:
```
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
```

И только во втором проекте будет определен ExclusiveForProject2Step. Если будет не простален ``` #if Project2 ```, то тогда будет ошибка компиляции для первого проекта.
[Полный код примера](https://github.com/Viridovics/SharedExamples/tree/master/AdditionalStepExample)

## Тестовый сценарий имеет шаг, который отличен у всех продуктов

Например, это может звучать так:
* Выполните шаг CommonStep1
* Для первого продукта выполнить "Step for project 1", а для второго продукта выполнить "Step for project 2"
* Выполните шаг CommonStep2

В этом случае код тестового метода может выглядеть так:
```
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
```

А уже для для каждого отдельного продукта будет определен ``` DifferentStep(); ```. Данный механизм также подходит для того, чтобы определить composition root для конкретного продукта.
[Полный код примера](https://github.com/Viridovics/SharedExamples/tree/master/DifferentStepExample)

## Есть набор компонент и несколько продуктов. Каждый компонент может быть включен в один или несколько продуктов

Пример может выглядеть следующим образом:
| Component | Included in|
|-----------|------------|
| C1        | P1, P2     |
| C2        | P2, P3     |
| C3        | P1         |
| C4        | P1, P2, P3 |

Соответственно, будет достаточно для каждой компоненты сделать свой shared проект с тестами. Для каждого продукта будет сделан обычный (.net framework или .net core) проект. А далее в продуктовые проекты будут подключены shared проекты компонент, которые являются частью продукта. После сборки всех проектов получится три сборки P1.Tests.dll, P2.Tests.dll и P3.Tests.dll, каждая из которых будет содержать набор тестов, который требуется одноименному продукту. 

## Увеличение количества различных интерфейсов/классов из-за различий общих компонент для разных продуктов

Допустим, что есть интерфейс, который отвечает за работу с каким-то компонентом ``` interface IComponentSettings ```. Далее для конкретного продукта интерфейс по работе с этой компонентой может быть расширен ``` interface IProductComponentSettings : IComponentSettings ```. Таким образом, например для двух продуктов и одной компоненты, при обычном подходе (DI и composition root) будет создано 3 интерфейса, а также будет необходимо в 2-ух composition root регистрировать по 2 интерфейса:
```
            container.RegisterType<IProductComponentSettings, ProductComponentSettings>(new ContainerControlledLifetimeManager());
            container.RegisterType<IComponentSettings, ProductComponentSettings>(new ContainerControlledLifetimeManager());
```

При использовании shared project количество интерфейсов может быть сокращено до 2-ух, а количество регистраций в composition root будет одно. Для этого будет необходимо в shared проекте объявить ``` partial interface IComponentSettings ```, а в проекте по работе с конкретным продуктом включить данный shared проект и создать файл с ``` partial interface IComponentSettings ```. Регистрация интерфейса будет выглядеть так:
```
            container.RegisterType<IComponentSettings, ProductComponentSettings>(new ContainerControlledLifetimeManager());
```
Данный подход можно обобщить для классов.
