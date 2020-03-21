# SharedExamples

Можно рассмотреть несколько сценариев, для которых удобны Shared project.

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

А уже для для каждого отдельного продукта будет определен ``` DifferentStep(); ```. Данный механизм также подходит для того, чтобы определить composition root для конекретного продукта.

[Полный код примера](https://github.com/Viridovics/SharedExamples/tree/master/DifferentStepExample)
