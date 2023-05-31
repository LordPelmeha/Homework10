using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    public class StartingTheProgram
    {

        /// <summary>
        /// Метод, вызываемый в основной программе, использующий все классы и методы 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void StartKnowledgeCheck()
        {
            Console.WriteLine("Добро пожаловать! Сейчас вы пройдете тест на ваши знания по алгебре и немного геометрии!");
            TestGenerator test = new TestGenerator();
            test.LoadTasksFromFile("questions.txt");
            test.LoadTasksWithAnswersFile("questionsWithAnswers.txt");
            Console.WriteLine("Сколько всего заданий Вы хотите сгенерировать в одном варианте?");
            int numberOfTasks = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Сколько из этих заданий должны быть с вариантами ответа?");
            int numberOfTasksWithAnswers = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Сколько вариантов Вы хотите сгенерировать?");
            int numberOfVariants = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Вводите без пробелов, и если Вам нужно ввести какую-то переменную, то вводите ее на английском\nУдачи!\nЕсли у Вас возникнут проблемы с выполнением задания, напишите слово подсказка");
            Console.WriteLine();
            var testVariants = test.GenerateTestVariants(numberOfTasks, numberOfTasksWithAnswers, numberOfVariants);
            test.SaveVariantsToFile(testVariants);

            if (testVariants != null && testVariants.Count > 0)
            {
                Console.WriteLine("Контрольная работа:");
                Console.WriteLine();
                int correctAnswers = 0;
                int wrongAnswers = 0;

                for (int i = 0; i < testVariants.Count; i++)
                {
                    Console.WriteLine($"Вариант {i + 1}:");
                    Console.WriteLine();
                    var testVariant = testVariants[i];
                    for (int j = 0; j < testVariant.Count; j++)
                    {
                        var task = testVariant[j];
                        Console.WriteLine($"Задание {j + 1}: {task.Question}");
                        Console.Write("Ваш ответ: ");
                        string userAnswer = Console.ReadLine();

                        if (userAnswer.ToLower() == "подсказка")
                        {

                            Console.WriteLine($"Подсказка: {task.Hint}");
                            Console.Write("Ваш ответ: ");
                            userAnswer = Console.ReadLine();
                        }

                        bool isCorrect = CheckAnswer(task, userAnswer);
                        Console.WriteLine(isCorrect ? "Правильно!" : "Неправильно!");
                        Console.WriteLine();
                        if (isCorrect)
                            correctAnswers++;
                        else
                            wrongAnswers++;
                    }

                    Console.WriteLine("Результаты по данному варианту:");
                    Console.WriteLine($"Верных ответов: {correctAnswers}");
                    Console.WriteLine($"Неверных ответов: {wrongAnswers}");
                    Console.WriteLine();

                    Console.WriteLine("Хотите продолжить решать следующий вариант? (Да/Нет)");
                    string continueResponse = Console.ReadLine();

                    if (continueResponse.Trim().ToLower() != "да")
                    {
                        Console.WriteLine("Вы решили завершить тест. До свидания!");
                        return;
                    }
                    correctAnswers = 0;
                    wrongAnswers = 0;
                }
            }
            else
                throw new Exception("Не удалось сгенерировать варианты контрольной работы.");
        }
        /// <summary>
        /// Проверка правильности ответа на вопрос
        /// </summary>
        /// <param name="question"></param>
        /// <param name="userAnswer"></param>
        /// <returns></returns>
        static bool CheckAnswer(Task question, string userAnswer)
        {
            return userAnswer.Equals(question.Answer, StringComparison.OrdinalIgnoreCase);
        }
    }
}

