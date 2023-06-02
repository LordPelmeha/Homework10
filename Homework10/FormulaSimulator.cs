using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework10
{
    public class FormulaSimulator
    {
        public Dictionary<string, List<Formula>> _formulabank { get; set; }
        public Dictionary<string, List<Theorem>> _theorembank { get; set; }
        public int _rightcnt;
        public int _wrongcnt;
        public int _traincnt;

        public FormulaSimulator()
        {
            _formulabank = new Dictionary<string, List<Formula>>();
            _theorembank = new Dictionary<string, List<Theorem>>();
            _rightcnt = 0;
            _wrongcnt = 0;
        }

        public void StartTraining()
        {
            try
            {
                LoadFormulas("formulas.txt");
                LoadTheorems("theorems.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке формул и теорем: {ex.Message}");
                return;
            }

            Console.WriteLine("Выберите режим: 1 - Тренировка, 2 - Доказательство");
            var mode = Console.ReadLine();
            while (mode != "1" && mode != "2")
            {
                Console.WriteLine("Неверный режим. Повторите выбор: 1 - Тренировка, 2 - Доказательство");
                mode = Console.ReadLine();
            }

            if (mode == "1")
            {
                Training();
            }
            else if (mode == "2")
            {
                TheoremProving();
            }
        }
        public void LoadFormulas(string filename)
        {
            var formulas = File.ReadAllLines(filename);
            var formulabank = new Dictionary<string, List<Formula>>();
            foreach (var formula in formulas)
            {
                var split = formula.Split('|');
                if (split.Length != 3 || split[0] == null || split[1] == null || split[2] == null)
                    throw new ArgumentException("Неверный формат введённых формул в файле, измените формулу по образцу: Тема|Название формулы|формула");
                if (!formulabank.ContainsKey(split[0]))
                    formulabank.Add(split[0], new List<Formula>() { new Formula(split[1], split[2]) });
                formulabank[split[0]].Add(new Formula(split[1], split[2]));
            }
            _formulabank = formulabank;
        }

        public void LoadTheorems(string filename)
        {
            var theorems = File.ReadAllLines(filename);
            var bank = new Dictionary<string, List<Theorem>>();

            foreach (var theorem in theorems)
            {
                var split = theorem.Split('|');
                if (split.Length != 5 || split[0] == null || split[1] == null || split[2] == null || split[3] == null || split[4] == null)
                    throw new ArgumentException("Неверный формат введённых теорем в файле, измените теорему по образцу: Тема|Название теоремы|условие|заключение|доказательство");

                if (!bank.ContainsKey(split[0]))
                    bank.Add(split[0], new List<Theorem>() { new Theorem(split[1], split[2], split[3], split[4]) });
                else
                    bank[split[0]].Add(new Theorem(split[1], split[2], split[3], split[4]));
            }

            _theorembank = bank;
        }
        public void Training()
        {
            Console.WriteLine("Выберите тему:");
            foreach (var theme in _formulabank)
            {
                Console.WriteLine(theme.Key);
            }

            var input = Console.ReadLine();
            while (!_formulabank.ContainsKey(input))
            {
                Console.WriteLine("Тема не найдена. Повторите ввод:");
                input = Console.ReadLine();
            }

            var formulas = _formulabank[input];

            Console.WriteLine($"Тренируемся на теме: {input}");

            foreach (var formula in formulas)
            {
                Console.WriteLine($"Формула: {formula._fname}");
                Console.WriteLine($"Введите формулу:");

                var userinput = Console.ReadLine();
                if (userinput == formula._expr)
                {
                    Console.WriteLine("Правильно!");
                    _rightcnt++;
                }
                else
                {
                    Console.WriteLine("Неправильно!");
                    _wrongcnt++;
                }
                _traincnt++;
            }

            Console.WriteLine($"Тренировка завершена. Правильных ответов: {_rightcnt}, Неправильных ответов: {_wrongcnt}, Всего вопросов: {_traincnt}");
        }

        public void TheoremProving()
        {
            Console.WriteLine("Выберите тему:");
            foreach (var theme in _theorembank)
            {
                Console.WriteLine(theme.Key);
            }

            var input = Console.ReadLine();
            while (!_theorembank.ContainsKey(input))
            {
                Console.WriteLine("Тема не найдена. Повторите ввод:");
                input = Console.ReadLine();
            }

            var theorems = _theorembank[input];

            Console.WriteLine($"Доказываем теоремы на теме: {input}");

            foreach (var theorem in theorems)
            {
                Console.WriteLine($"Теорема: {theorem._tname}");
                Console.WriteLine($"Условие: {theorem._condition}");
                Console.WriteLine($"Заключение:");
                Console.WriteLine($"Доказательство:");

                Console.WriteLine("Введите заключение или доказательство:");

                var userinput = Console.ReadLine();
                if (userinput == theorem._conclusion || userinput == theorem._proof)
                {
                    Console.WriteLine("Правильно!");
                    _rightcnt++;
                }
                else
                {
                    Console.WriteLine("Неправильно!");
                    _wrongcnt++;
                }
                _traincnt++;
            }

            Console.WriteLine($"Доказательство завершено. Правильных ответов: {_rightcnt}, Неправильных ответов: {_wrongcnt}, Всего вопросов: {_traincnt}");
        }
    }

}
