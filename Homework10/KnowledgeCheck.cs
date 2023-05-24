using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathTutor
{
    public void StartKnowledgeCheck()
    {
        Console.WriteLine("Добро пожаловать! Сейчас вы пройдете тест на ваши знания по выбранному Вами предмету!");
        TestGenerator test = new TestGenerator();
        Console.WriteLine("Хорошо, сейчас Вы пройдете тест по алгебре\nСколько заданий Вы хотите в тесте?");
        test.LoadTasksFromFile("questions.txt");

        Console.WriteLine("Сколько заданий Вы хотите сгенерировать в одном варианте?");
        int numberOfTasks = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Сколько вариантов Вы хотите сгенерировать?");
        int numberOfVariants = Convert.ToInt32(Console.ReadLine());
        var testVariants = test.GenerateTestVariants(numberOfTasks, numberOfVariants);

        if (testVariants != null && testVariants.Count > 0)
        {
            Console.WriteLine("Контрольная работа:");
            Console.WriteLine();
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
                    bool isCorrect = CheckAnswer(task, userAnswer);
                    Console.WriteLine(isCorrect ? "Правильно!" : "Неправильно!");
                    Console.WriteLine();
                }
            }
        }
        else
        {
            Console.WriteLine("Не удалось сгенерировать варианты контрольной работы.");
        }
    }

    public static bool CheckAnswer(Task task, string userAnswer)
    {
        return userAnswer.Equals(task.Answer, StringComparison.OrdinalIgnoreCase);
    }
}



static bool CheckAnswer(TestQuestion question, string userAnswer)
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
