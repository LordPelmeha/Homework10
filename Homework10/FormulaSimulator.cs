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
        public int _rightcnt;
        public int _wrongcnt;
        public int _traincnt;

        public FormulaSimulator() 
        {
            _formulabank = new Dictionary<string, List<Formula>>();
            _rightcnt = 0;
            _wrongcnt = 0;
        }

        public void LoadFormulas(string filename)
        {
            
            var formulas = File.ReadAllLines(filename);
            var bank = new Dictionary<string, List<Formula>>();
            foreach (var formula in formulas)
            {
                var split = formula.Split(',');
                if (split.Length != 3 || split[0] == null || split[1] == null || split[2] == null)
                    throw new ArgumentException("Неверный формат введённых формул в файле, измените формулу по образцу: Тема, Название формулы, формула");
                if (!bank.ContainsKey(split[0]))
                    bank.Add(split[0], new List<Formula>() { new Formula(split[1], split[2]) });
                bank[split[0]].Add(new Formula(split[1], split[2]));

            }
            _formulabank= bank;
        }

        public async void Training()
        {
            anothertheme:
            var statthemes = new Dictionary<string, int[]>();
            int curthemewrongans = 0;
            int curthemerightans = 0;
            int tk = 1;
            Console.WriteLine("Здравствуйте, вас приветствует тренажер по запоминанию формул!\nВыберите 1 из предоставленных тем:") ;
            foreach (var themes in _formulabank)
            {
                Console.WriteLine($"{tk}){themes.Key}");
                tk++;
            }
            
            var input = Console.ReadLine();
            if (!_formulabank.ContainsKey(input))
            {
                Console.WriteLine("Такой темы нет, выберите 1 тему из предоставленых либо корректно(точь-в-точь) введите название темы");
                input = Console.ReadLine();
            }

            var fk = _formulabank[input].Count;
            var formulas = _formulabank[input];
            var r = new Random();
            Console.WriteLine($"Для изучения доступно {fk} формул, также обозначим условности ввода формул." +
                $"\nБудьте аккуратнее с форматом ввода формулы, важен порядок ввода переменных, пробелы и формат." +
                $"\nСинус будет выглядеть как: sinx,возведение в степень n: x^n,умножение: x*y,деление: x/y." +
                $"\nПомимо этих обозначений в некоторых формулах также присутствуют геометрические обозначения такие как S-площадь; a,b,c-различные стороны геометрических фигур; d-диагональ;h-высота;r-радиус вписанной окружности ;p-полупериметр;" +
                $"\nТакже во всех формулах между функцией, коротким выражением которое нужно разложить и т.п. и непосредственно самим выражением или разложением ставиться = при том с пробелами с обоих сторон)" +
                $"\nЕсли вы пожелаете выбрать жругую тему для заучивания введите вместо формулы \"Выбор темы\", если пожелаете выйти из программы введите \"Выход\"");
            while (true)
            {
                var tf = r.Next(0,fk);
                if (!statthemes.Keys.Contains(formulas.ElementAt(tf)._fname))
                    statthemes.Add(formulas.ElementAt(tf)._fname,new int[2] {0,0});
                Console.WriteLine($"{formulas.ElementAt(tf)._fname}." +
                $"\nЗапишите формулу соответствующую этому названию на клавиатуре и после нажмите Enter для того,чтобы верная формула отобразилась в консоли");    
                var formulaorexit = Console.ReadLine() ;
                Console.WriteLine($"Формула выглядит: {formulas.ElementAt(tf)._expr}");
                if (formulaorexit.Equals(formulas.ElementAt(tf)._expr))
                {
                    Console.WriteLine("Верно, переёдем к следующей формуле!");
                    _rightcnt++;
                    statthemes[formulas.ElementAt(tf)._fname][0]++;
                    curthemerightans++;
                }
                else
                {
                    Console.WriteLine("Неверно(, переёдем к следующей формуле.");
                    _wrongcnt++;
                    statthemes[formulas.ElementAt(tf)._fname][1]++;
                    curthemewrongans++;
                }
                if (formulaorexit.Equals("Выбор темы"))
                {
                    File.AppendAllText("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt", $"{input}\n", Encoding.Default);
                    foreach (var item in statthemes)
                        File.AppendAllText("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt", $"{item.Key} {item.Value[0]} {item.Value[1]}\n", Encoding.Default);
                    Console.WriteLine($"Сию минуту, на данный момент повторяя формулы на тему {input} вы допустили {curthemewrongans} и верно вспомнили тему {curthemerightans} раз");
                    goto anothertheme;
                }
                    
                if (formulaorexit.Equals("Выход"))
                {
                    var strings = File.ReadAllLines("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt");
                    if (File.Exists("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt") && !strings.Contains("Тренировка 1"))
                        File.AppendAllText("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt","Трениновка 1", Encoding.Default);
                    else if (File.Exists("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt") && strings.Last().Substring(0, 10) == "Тренировка")
                    {
                        var treinnum = strings.Last().Substring(strings.Last().Length - 1, 1);
                        File.AppendAllText("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\statistics.txt", $"Трениновка {treinnum}",Encoding.Default);
                    }
                        Console.WriteLine($"Благодарим за использование тренажёра, вы допустили {_wrongcnt} ошибок и верно вспомнили формулы {_rightcnt} раз, удачи в использовании выученных формул!");
                    break;
                }


            }
            
        }

       
    }

    
}
