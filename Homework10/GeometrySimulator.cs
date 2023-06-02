using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Homework10
{
    public class GeometrySimulator
    {
        List<Rectangle> AllRectangles = new List<Rectangle>();
        List<Ellipse> AllEllipses = new List<Ellipse>();
        /// <summary>
        /// Добавляет прямоугольнмик в список
        /// </summary>
        private void AddRectangle()
        {
            double[] ar = Console.ReadLine().Replace('.', ',').Split().Select(x => double.Parse(x)).ToArray();
            AllRectangles.Add(new Rectangle(new Dot(ar[0], ar[1]), new Dot(ar[2], ar[3]), new Dot(ar[4], ar[5]), new Dot(ar[6], ar[7])));
        }
        /// <summary>
        /// Добавляет эллипс в список
        /// </summary>
        private void AddEllipse()
        {
            double[] ar = Console.ReadLine().Replace('.', ',').Split().Select(x => double.Parse(x)).ToArray();
            AllEllipses.Add(new Ellipse(new Dot(ar[0], ar[1]), new Dot(ar[2], ar[3]), new Dot(ar[4], ar[5]), new Dot(ar[6], ar[7])));
        }
        /// <summary>
        /// Возвращает координаты прямоугольника наиболее удаленного от начала координат
        /// </summary>
        private void MostRemoteFromCenterRectangle()
        {
            int index = 0;
            double distance = int.MinValue;
            for (int i = 0; i < AllRectangles.Count; i++)
            {
                if (AllRectangles[i].DistanceToCenter() > distance)
                {
                    distance = AllRectangles[i].DistanceToCenter();
                    index = i;
                }
            }
            Console.WriteLine($"Самый удалённый от центра прямоугольник имеет номер {index + 1} и следующие координаты:  {AllRectangles[index]}");
        }
        /// <summary>
        /// Возвращает координаты эллипса наиболее удаленного от начала координат
        /// </summary>
        private void MostRemoteFromCenterEllipse()
        {
            int index = 0;
            double distance = int.MinValue;
            for (int i = 0; i < AllEllipses.Count; i++)
            {
                if (AllEllipses[i].DistanceToCenter() > distance)
                {
                    distance = AllEllipses[i].DistanceToCenter();
                    index = i;
                }
            }
            Console.WriteLine($"Самый удалённый от центра эллипс имеет номер {index + 1} и следующие координаты: {AllEllipses[index]}");
            
        }
        /// <summary>
        /// Доп функция для удобного ввода номера прямоугольника
        /// </summary>
        /// <returns></returns>
        private int GetRectInd()
        {
            string ind = Console.ReadLine();

            while (Regex.Match(ind, @"\D").Success || ind == "0" || int.Parse(ind) > AllRectangles.Count)
            {
                Console.WriteLine("Ну нет такого номера! Попробуйте ещё раз:");
                ind = Console.ReadLine();
            }
            return int.Parse(ind) - 1;
        }
        /// <summary>
        /// Доп функция для удобного ввода значения для предиката
        /// </summary>
        /// <returns></returns>
        private double GetRectNum()
        {
            string n = Console.ReadLine();
            bool flag = true;
            while (flag)
            {
                if (Regex.Match(n, @"^-?\d+\.?(\d+)?$").Success)
                    flag = false;
                else
                {
                    Console.WriteLine("Нет такого числа! Попробуйте ещё раз:");
                    n = Console.ReadLine();
                }
            }
            return double.Parse(n.Replace('.', ','));
        }
        /// <summary>
        /// Доп функция для удобного ввода номера эллипса
        /// </summary>
        /// <returns></returns>
        private int GetEllInd()
        {
            string ind = Console.ReadLine();

            while (Regex.Match(ind, @"\D").Success || ind == "0" || int.Parse(ind) > AllEllipses.Count)
            {
                Console.WriteLine("Ну нет такого номера! Попробуйте ещё раз:");
                ind = Console.ReadLine();
            }
            return int.Parse(ind) - 1;
        }
        /// <summary>
        /// Доп функция для удобного ввода значения для предиката
        /// </summary>
        /// <returns></returns>
        private double GetEllNum()
        {
            string n = Console.ReadLine();
            bool flag = true;
            while (flag)
            {
                if (Regex.Match(n, @"^-?\d+\.?(\d+)?$").Success)
                    flag = false;
                else
                {
                    Console.WriteLine("Нет такого числа! Попробуйте ещё раз:");
                    n = Console.ReadLine();
                }
            }
            return double.Parse(n.Replace('.', ','));
        }
        /// <summary>
        /// Выводит координаты всех прямоугольников из списка
        /// </summary>
        private void PrintAllRectangles()
        {
            foreach (var x in AllRectangles)
                Console.WriteLine(x);
        }
        /// <summary>
        /// Выводит координаты всех эллипсов из списка
        /// </summary>
        private void PrintAllEllipses()
        {
            foreach (var x in AllEllipses)
                Console.WriteLine(x);
        }
        /// <summary>
        /// Функция с доступными предикатами для прямоугольника
        /// </summary>
        private void RectanglePredicate()
        {
            Console.WriteLine("Вам доступны следующие предикаты:");
            Console.WriteLine("1) Вывести все прямоугольники из 1-й четверти");
            Console.WriteLine("2) Вывести все прямоугольники из 2-й четверти");
            Console.WriteLine("3) Вывести все прямоугольники из 3-й четверти");
            Console.WriteLine("4) Вывести все прямоугольники из 4-й четверти");
            Console.WriteLine("5) Вывести все прямоугольники, площадь которых больше некоторого числа");
            Console.WriteLine("6) Вывести все прямоугольники, площадь которых меньше некоторого числа");
            Console.WriteLine("7) Вывести все прямоугольники, периметр которых больше некоторого числа ");
            Console.WriteLine("8) Вывести все прямоугольники, периметр которых меньше некоторого числа ");
            Console.WriteLine("9) Вывести прямоугольник с наименьшим периметром ");
            Console.WriteLine("Введите номер придиката:");
            string ans = Console.ReadLine();
            while (ans.Length != 1 || !"123456789".Contains(ans))
            {
                Console.WriteLine("Вы пытаетесь поломать симулятор! Фу таким быть. Введите один из номеров предикатов, которые были вам предложены:");
                ans = Console.ReadLine();
            }
            switch (ans)
            {
                case "1":
                    {
                        foreach (var x in AllRectangles)
                            if (x.BottomLeft.X > 0 && x.BottomLeft.Y > 0 && x.UpperLeft.X > 0 && x.UpperLeft.Y > 0 &&
                                x.UpperRight.X > 0 && x.UpperRight.Y > 0 && x.BottomRight.X > 0 && x.BottomRight.Y > 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "2":
                    {
                        foreach (var x in AllRectangles)
                            if (x.BottomLeft.X < 0 && x.BottomLeft.Y > 0 && x.UpperLeft.X < 0 && x.UpperLeft.Y > 0 &&
                                x.UpperRight.X < 0 && x.UpperRight.Y > 0 && x.BottomRight.X < 0 && x.BottomRight.Y > 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "3":
                    {
                        foreach (var x in AllRectangles)
                            if (x.BottomLeft.X < 0 && x.BottomLeft.Y < 0 && x.UpperLeft.X < 0 && x.UpperLeft.Y < 0 &&
                                x.UpperRight.X < 0 && x.UpperRight.Y < 0 && x.BottomRight.X < 0 && x.BottomRight.Y < 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "4":
                    {
                        foreach (var x in AllRectangles)
                            if (x.BottomLeft.X > 0 && x.BottomLeft.Y < 0 && x.UpperLeft.X > 0 && x.UpperLeft.Y < 0 &&
                                x.UpperRight.X > 0 && x.UpperRight.Y < 0 && x.BottomRight.X > 0 && x.BottomRight.Y < 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "5":
                    {
                        double num = GetRectNum();
                        foreach (var x in AllRectangles)
                            if (x.Square() > num)
                                Console.WriteLine(x);
                        break;
                    }
                case "6":
                    {
                        double num = GetRectNum();
                        foreach (var x in AllRectangles)
                            if (x.Square() < num)
                                Console.WriteLine(x);
                        break;
                    }
                case "7":
                    {
                        double num = GetRectNum();
                        foreach (var x in AllRectangles)
                            if (x.Perimeter() > num)
                                Console.WriteLine(x);
                        break;
                    }
                case "8":
                    {
                        double num = GetRectNum();
                        foreach (var x in AllRectangles)
                            if (x.Perimeter() < num)
                                Console.WriteLine(x);
                        break;
                    }
                case "9":
                    {
                        var minrect = new Rectangle(new Dot(0, 0), new Dot(0, 0), new Dot(0, 0), new Dot(0, 0));
                        double minper = int.MaxValue;
                        foreach (var x in AllRectangles)
                            if (x.Perimeter() < minper)
                            {
                                minper = x.Perimeter();
                                minrect = x;
                            }    
                        Console.WriteLine(minrect);
                        break;
                    }
            }
        }
        /// <summary>
        /// Функция с доступными предикатами для эллипса
        /// </summary>
        private void EllipsePredicate()
        {
            Console.WriteLine("Вам доступны следующие предикаты:");
            Console.WriteLine("1) Вывести все эллипсы из 1-й четверти");
            Console.WriteLine("2) Вывести все эллипсы из 2-й четверти");
            Console.WriteLine("3) Вывести все эллипсы из 3-й четверти");
            Console.WriteLine("4) Вывести все эллипсы из 4-й четверти");
            Console.WriteLine("5) Вывести все эллипсы, площадь которых больше некоторого числа");
            Console.WriteLine("6) Вывести все эллипсы, площадь которых меньше некоторого числа");
            Console.WriteLine("7) Вывести все эллипсы, периметр которых больше некоторого числа ");
            Console.WriteLine("8) Вывести все эллипсы, периметр которых меньше некоторого числа ");
            Console.WriteLine("9) Вывести все эллипсы, являющиеся кругами ");
            Console.WriteLine("Введите номер придиката:");
            string ans = Console.ReadLine();
            while (ans.Length != 1 || !"123456789".Contains(ans))
            {
                Console.WriteLine("Вы пытаетесь поломать симулятор! Фу таким быть. Введите один из номеров предикатов, которые были вам предложены:");
                ans = Console.ReadLine();
            }
            switch (ans)
            {
                case "1":
                    {
                        foreach (var x in AllEllipses)
                            if (x._toppnt.X > 0 && x._toppnt.Y > 0 && x._leftpnt.X > 0 && x._leftpnt.Y > 0 &&
                                x._downpnt.X > 0 && x._downpnt.Y > 0 && x._rightpnt.X > 0 && x._rightpnt.Y > 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "2":
                    {
                        foreach (var x in AllEllipses)
                            if (x._toppnt.X < 0 && x._toppnt.Y > 0 && x._leftpnt.X < 0 && x._leftpnt.Y > 0 &&
                                x._downpnt.X < 0 && x._downpnt.Y > 0 && x._rightpnt.X < 0 && x._rightpnt.Y > 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "3":
                    {
                        foreach (var x in AllEllipses)
                            if (x._toppnt.X < 0 && x._toppnt.Y < 0 && x._leftpnt.X < 0 && x._leftpnt.Y < 0 &&
                                x._downpnt.X < 0 && x._downpnt.Y < 0 && x._rightpnt.X < 0 && x._rightpnt.Y < 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "4":
                    {
                        foreach (var x in AllEllipses)
                            if (x._toppnt.X > 0 && x._toppnt.Y < 0 && x._leftpnt.X > 0 && x._leftpnt.Y < 0 &&
                                x._downpnt.X > 0 && x._downpnt.Y < 0 && x._rightpnt.X > 0 && x._rightpnt.Y < 0)
                                Console.WriteLine(x);
                        break;
                    }
                case "5":
                    {
                        double num = GetEllNum();
                        foreach (var x in AllEllipses)
                            if (x.Square() > num)
                                Console.WriteLine(x);
                        break;
                    }
                case "6":
                    {
                        double num = GetEllNum();
                        foreach (var x in AllEllipses)
                            if (x.Square() < num)
                                Console.WriteLine(x);
                        break;
                    }
                case "7":
                    {
                        double num = GetEllNum();
                        foreach (var x in AllEllipses)
                            if (x.Perimeter() > num)
                                Console.WriteLine(x);
                        break;
                    }
                case "8":
                    {
                        double num = GetEllNum();
                        foreach (var x in AllEllipses)
                            if (x.Perimeter() < num)
                                Console.WriteLine(x);
                        break;
                    }
                case "9":
                    {
                        foreach (var x in AllEllipses)
                            if (x.IsCircle() == true)
                                Console.WriteLine(x);
                        break;
                    }
            }
        }


        public void Start()
        {
            Console.WriteLine("Приветствую вас в симуляторе геометрии! Выберите тип фигуры,с которой вы хотите взаимодействовать(Эллипс/Прямоугольник).  ");
            var figuretype = Console.ReadLine();
            if (figuretype.Equals("Прямоугольник"))
            {
                Console.WriteLine("1) Выввести на экран все прямоугольники на плоскости");
                Console.WriteLine("2) Вывести площадь и периметр прямоугольника");
                Console.WriteLine("3) Вевести наиболее удалённый от центра координат прямоугольник");
                Console.WriteLine("4) Повернуть прямоугольник вокруг его центра на ɑ градусов");
                Console.WriteLine("5) Перестить прямоугольник по осям X и Y на заданные сдвиги");
                Console.WriteLine("6) Увеличить высоты и ширины прямоугольника на заданные коэффициенты");
                Console.WriteLine("7) Получить массива прямоугольников согласно заданному предикату");
                Console.WriteLine("9) Добавить координаты прямоугольника");
                Console.WriteLine("0) Закончить работу симулятора");
                Console.WriteLine("Чтобы выбрать нужную функцию, введите её номер:");
                string ans = Console.ReadLine();
                while (ans != "0")
                {
                    while (ans.Length != 1 || !"123456790".Contains(ans))
                    {
                        Console.WriteLine("Вы пытаетесь поломать симулятор! Фу таким быть. Введите один из номеров функций, которые были вам предложены:");
                        ans = Console.ReadLine();
                    }
                    switch (ans)
                    {
                        case "1":
                            {
                                PrintAllRectangles();
                                break;
                            }
                        case "2":
                            {
                                Console.WriteLine("Введите номер прямоугольника:");
                                int ind = GetRectInd();
                                Console.WriteLine($"{ind + 1}-й прямоугольник имеет следующие площадь и периметр: " +
                                    $"{AllRectangles[ind].Square()} {AllRectangles[ind].Perimeter()}");
                                break;
                            }
                        case "3":
                            {
                                MostRemoteFromCenterRectangle();
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Введите номер прямоугольника:");
                                int ind = GetRectInd();
                                Console.WriteLine("Введите  угол, на который будет повёрнут прямоугольник:");
                                double angle = GetRectNum();
                                AllRectangles[ind].Turn(angle);
                                break;
                            }
                        case "5":
                            {
                                Console.WriteLine("Введите номер прямоугольника:");
                                int ind = GetRectInd();
                                Console.WriteLine("Введите сдвиг по Х");
                                double x = GetRectNum();
                                Console.WriteLine("Введите сдвиг по Y");
                                double y = GetRectNum();
                                AllRectangles[ind].ShiftXY(x, y);
                                break;
                            }
                        case "6":
                            {
                                Console.WriteLine("Введите номер прямоугольника:");
                                int ind = GetRectInd();
                                Console.WriteLine("Введите коэффициент для длины");
                                double len = GetRectNum();
                                Console.WriteLine("Введите коэффициент для ширины");
                                double wid = GetRectNum();
                                AllRectangles[ind].Stretch(len, wid);
                                break;
                            }
                        case "7":
                            {
                                RectanglePredicate();
                                break;
                            }
                        case "8":
                            {
                                Console.WriteLine("Здесь ничего нет!");
                                break;
                            }
                        case "9":
                            {
                                Console.WriteLine("Введите координаты прямоугольника:");
                                AddRectangle();
                                break;
                            }
                        default:
                            break;
                    }
                    Console.WriteLine("Выполнение задачи завершено. Введите номер следующец функции:");
                    ans = Console.ReadLine();
                }
            }
            else if (figuretype.Equals("Эллипс"))
            {
                Console.WriteLine("1) Выввести на экран все эллипсы на плоскости");
                Console.WriteLine("2) Вывести площадь и периметр эллипса");
                Console.WriteLine("3) Вевести наиболее удалённый от центра координат эллипс");
                Console.WriteLine("4) Повернуть эллипс вокруг его центра на ɑ градусов");
                Console.WriteLine("5) Переместить эллипс по осям X и Y на заданные сдвиги");
                Console.WriteLine("6) Увеличить высоты и ширины эллипса на заданные коэффициенты");
                Console.WriteLine("7) Получить массива эллипсов согласно заданному предикату");
                Console.WriteLine("9) Добавить эллипса прямоугольника");
                Console.WriteLine("0) Закончить работу симулятора");
                Console.WriteLine("Чтобы выбрать нужную функцию, введите её номер:");
                string ans = Console.ReadLine();
                while (ans != "0")
                {
                    while (ans.Length != 1 || !"123456790".Contains(ans))
                    {
                        Console.WriteLine("Вы пытаетесь поломать симулятор! Фу таким быть. Введите один из номеров функций, которые были вам предложены:");
                        ans = Console.ReadLine();
                    }
                    switch (ans)
                    {
                        case "1":
                            {
                                PrintAllEllipses();
                                break;
                            }
                        case "2":
                            {
                                Console.WriteLine("Введите номер эллипса:");
                                int ind = GetEllInd();
                                Console.WriteLine($"{ind + 1}-й эллипс имеет следующие площадь и периметр: " +
                                    $"{AllEllipses[ind].Square()} {AllEllipses[ind].Perimeter()}");
                                break;
                            }
                        case "3":
                            {
                                MostRemoteFromCenterEllipse();
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Введите номер эллипса:");
                                int ind = GetEllInd();
                                Console.WriteLine("Введите  угол, на который будет повёрнут эллипс:");
                                double angle = GetEllNum();
                                AllEllipses[ind].Turn(angle);
                                break;
                            }
                        case "5":
                            {
                                Console.WriteLine("Введите номер эллипса:");
                                int ind = GetEllInd();
                                Console.WriteLine("Введите сдвиг по Х");
                                double x = GetEllNum();
                                Console.WriteLine("Введите сдвиг по Y");
                                double y = GetEllNum();
                                AllRectangles[ind].ShiftXY(x, y);
                                break;
                            }
                        case "6":
                            {
                                Console.WriteLine("Введите номер эллипса:");
                                int ind = GetEllInd();
                                Console.WriteLine("Введите коэффициент для длины");
                                double len = GetEllNum();
                                Console.WriteLine("Введите коэффициент для ширины");
                                double wid = GetEllNum();
                                AllEllipses[ind].Stretch(len, wid);
                                break;
                            }
                        case "7":
                            {
                                EllipsePredicate();
                                break;
                            }
                        case "8":
                            {
                                Console.WriteLine("Здесь ничего нет!");
                                break;
                            }
                        case "9":
                            {
                                Console.WriteLine("Введите координаты эллипса:");
                                AddEllipse();
                                break;
                            }
                        default:
                            break;
                    }
                    Console.WriteLine("Выполнение задачи завершено. Введите номер следующец функции:");
                    ans = Console.ReadLine();
                }
            }
            else throw new ArgumentException("Не ломай симулятор пожалуйста");
            
            Console.WriteLine("Спасибо, что воспользовались нашим симулятором! Всего вам доброго!");
        }
    }
}
