# SharedExamples

Можно рассмотреть несколько сценариев, для которых удобны Shared project.

## Тестовый сценарий имеет один шаг, который специфичен для конкретного продукта

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

И только во втором проекте определяем ExclusiveForProject2Step. Если даже забудем проставить ``` #if Project2 ```, то тогда получим ошибку компиляции для первого проекта.
[Полный код примера](https://github.com/Viridovics/SharedExamples/tree/master/AdditionalStepExample)
