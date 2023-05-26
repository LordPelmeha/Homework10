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
            Console.WriteLine("Сколько заданий Вы хотите сгенерировать в одном варианте?");
            int numberOfTasks = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Сколько вариантов Вы хотите сгенерировать?");
            int numberOfVariants = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Вводите без пробелов, и если Вам нужно ввести какую-то переменную, то вводите ее на английском\nУдачи!\nЕсли у Вас возникнут проблемы с выполнением задания, напишите слово подсказка");
            Console.WriteLine();
            var testVariants = test.GenerateTestVariants(numberOfTasks, numberOfVariants);
            ///test.SaveVariantsToFile(testVariants);

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
                        Console.WriteLine($"Вопрос {j + 1}: {task.Question}");
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

    /// <summary>
    /// Класс для задания контрольной
    /// </summary>
    public class Task
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Hint { get; set; }

        public Task(string question, string answer, string hint)
        {
            Question = question;
            Answer = answer;
            Hint = hint;
        }
    }

    /// <summary>
    /// Класс генерации вариантов контрольной
    /// </summary>
    public class TestGenerator
    {
        private List<Task> taskBank;
        private Random random;

        public TestGenerator()
        {
            taskBank = new List<Task>();
            random = new Random();
        }

        /// <summary>
        /// Генерация вариантов контрольной
        /// </summary>
        /// <param name="numberOfTasks"></param>
        /// <param name="numberOfVariants"></param>
        /// <returns></returns>
        public List<List<Task>> GenerateTestVariants(int numberOfTasks, int numberOfVariants)
        {
            if (taskBank.Count < numberOfTasks)
            {
                Console.WriteLine("Ошибка: Недостаточно заданий в банке");
                return null;
            }

            List<List<Task>> testVariants = new List<List<Task>>();
            HashSet<int> selectedIndices = new HashSet<int>();

            for (int i = 0; i < numberOfVariants; i++)
            {
                List<Task> variant = new List<Task>();

                while (variant.Count < numberOfTasks)
                {
                    int randomIndex = random.Next(taskBank.Count);

                    if (!selectedIndices.Contains(randomIndex))
                    {
                        selectedIndices.Add(randomIndex);
                        variant.Add(taskBank[randomIndex]);
                    }
                }
                testVariants.Add(variant);
                selectedIndices.Clear();
            }

            return testVariants;
        }

        /// <summary>
        /// Загрузка банка вопросов из файла
        /// </summary>
        /// <param name="filename"></param>
        public void LoadTasksFromFile(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (parts.Length == 3)
                    {
                        string question = parts[0];
                        string answer = parts[1];
                        string hint = parts[2];

                        Task task = new Task(question, answer,hint);
                        taskBank.Add(task);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при чтении файла: " + e.Message);
            }
        }

        /// <summary>
        /// Сохранение вариантов в файлы
        /// </summary>
        /// <param name="testVariants"></param>
        public void SaveVariantsToFile(List<List<Task>> testVariants)
        {
            try
            {
                for (int i = 0; i < testVariants.Count; i++)
                {
                    string variantFileName = $"Variant_{i + 1}.txt";
                    string hintsFileName = $"Hints_{i + 1}.txt";

                    using (StreamWriter variantWriter = new StreamWriter(variantFileName))
                    using (StreamWriter hintsWriter = new StreamWriter(hintsFileName))
                    {
                        List<Task> variant = testVariants[i];

                        for (int j = 0; j < variant.Count; j++)
                        {
                            Task task = variant[j];

                            variantWriter.WriteLine($"Вопрос {j + 1}: {task.Question}");
                            hintsWriter.WriteLine($"Подсказка к вопросу {j + 1}: {task.Hint}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при сохранении файлов: " + e.Message);
            }
        }

    }
}

