using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    /// <summary>
    /// Класс генерации вариантов контрольной
    /// </summary>
    public class TestGenerator
    {
        private List<Task> taskBank;
        private List<Task> taskWithAnswersBank;
        private Random random;

        public TestGenerator()
        {
            taskBank = new List<Task>();
            taskWithAnswersBank = new List<Task>();
            random = new Random();
        }

        /// <summary>
        /// Генерация вариантов контрольной
        /// </summary>
        /// <param name="numberOfTasks"></param>
        /// <param name="numberOfVariants"></param>
        /// <returns></returns>
        public List<List<Task>> GenerateTestVariants(int numberOfTasks, int numberOfTasksWithAnswers, int numberOfVariants)
        {
            if (taskBank.Count < numberOfTasks || taskWithAnswersBank.Count < numberOfTasksWithAnswers || numberOfTasks < numberOfTasksWithAnswers)
            {
                Console.WriteLine(taskWithAnswersBank.Count);
                throw new ArgumentException("Неверно задано количество заданий!");
            }

            List<List<Task>> testVariants = new List<List<Task>>();
            HashSet<int> selectedIndices = new HashSet<int>();
            HashSet<int> selectedIndicesForAns = new HashSet<int>();
            for (int i = 0; i < numberOfVariants; i++)
            {
                List<Task> variant = new List<Task>();
                int randomIndex;
                while (variant.Count < numberOfTasks - numberOfTasksWithAnswers)
                {
                    randomIndex = random.Next(taskBank.Count);

                    if (!selectedIndices.Contains(randomIndex))
                    {
                        selectedIndices.Add(randomIndex);
                        variant.Add(taskBank[randomIndex]);
                    }
                }
                while (variant.Count < numberOfTasksWithAnswers)
                {
                    randomIndex = random.Next(taskWithAnswersBank.Count);

                    if (!selectedIndicesForAns.Contains(randomIndex))
                    {
                        selectedIndicesForAns.Add(randomIndex);
                        variant.Add(taskWithAnswersBank[randomIndex]);
                    }
                }
                testVariants.Add(Shuffle(variant));
                selectedIndices.Clear();
                selectedIndicesForAns.Clear();
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

                        Task task = new Task(question, answer, hint);
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
        /// Загрузка банка заданий с ответами
        /// </summary>
        /// <param name="filename"></param>
        public void LoadTasksWithAnswersFile(string filename)
        {
            try
            {
                string[] lines = File.ReadAllLines(filename);

                foreach (string line in lines)
                {
                    string[] parts = line.Split('|');

                    if (parts.Length == 3)
                    {
                        List<string> variants = Shuffle(parts[1].Split(' ').ToList());
                        string question = parts[0] + "\n" + GiveNumeration(variants);
                        string answer = (variants.ToList().IndexOf(parts[1].Split(' ')[0]) + 1).ToString();
                        string hint = parts[2].Split(' ')[random.Next(0, 3)];

                        Task task = new Task(question, answer, hint);
                        taskWithAnswersBank.Add(task);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при чтении файла: " + e.Message);
            }
        }
        /// <summary>
        /// Перетасовывает массив по алгоритму Фишера — Йетса
        /// </summary>
        private List<T> Shuffle<T>(List<T> badVariants)
        {
            for (int i = badVariants.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                var temp = badVariants[j];
                badVariants[j] = badVariants[i];
                badVariants[i] = temp;
            }
            return badVariants;
        }
        /// <summary>
        /// Возвращает строку, состоящую из пронумерованных элементов списка
        /// </summary>
        private string GiveNumeration(List<String> ar)
        {
            var variants = new StringBuilder();
            for (int i = 0; i < ar.Count; i++)
                variants.Append($"{i + 1}) {ar[i]}    ");
            return variants.ToString().Trim();
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
