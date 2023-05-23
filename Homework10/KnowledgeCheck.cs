using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    /// <summary>
    /// Класс для задания контрольной
    /// </summary>
    public class Task
    {
        public string Question { get; set; }
        public string Answer { get; set; }

        public Task(string question, string answer)
        {
            Question = question;
            Answer = answer;
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
        /// Добавление заданий в контрольную
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(Task task)
        {
            taskBank.Add(task);
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
                    string[] parts = line.Split(';');

                    if (parts.Length == 2)
                    {
                        string question = parts[0];
                        string answer = parts[1];

                        Task task = new Task(question, answer);
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
        /// Механизм подсказок
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public string GetHint(Task task)
        {
            if (task.Answer.Length > 0)
            {
                return task.Answer.Substring(0, 1);
            }

            return "";
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
                            hintsWriter.WriteLine($"Подсказка к вопросу {j + 1}: {GetHint(task)}");
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
