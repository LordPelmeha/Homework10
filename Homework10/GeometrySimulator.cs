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
        private void AddRectangle()
        {
            double[] ar = Console.ReadLine().Replace('.', ',').Split().Select(x => double.Parse(x)).ToArray();
            AllRectangles.Add(new Rectangle(new Dot(ar[0], ar[1]), new Dot(ar[2], ar[3]), new Dot(ar[4], ar[5]), new Dot(ar[6], ar[7])));
        }
        private void MostRemoteFromCenter()
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
            Console.WriteLine($"Самым удалённым от центра прямоугольником является {index + 1}-й прямоугольник со следующими координатами: {AllRectangles[index]}");
        }
        private int GetInd()
        {
            string ind = Console.ReadLine();
            while (Regex.Match(ind, @"\D").Success || ind == "0" || int.Parse(ind) > AllRectangles.Count)
            {
                Console.WriteLine("Ну нет такого номера! Попробуйте ещё раз:");
                ind = Console.ReadLine();
            }
            return int.Parse(ind) - 1;
        }
        private double GetNum()
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
        private void PrintAllRectangles()
        {
            foreach (var x in AllRectangles)
                Console.WriteLine(x);
        }
        private void Predicate()
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
            Console.WriteLine("Введите номер придиката:");
            string ans = Console.ReadLine();
            while (ans.Length != 1 || !"12345678".Contains(ans))
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
                        double num = GetNum();
                        foreach (var x in AllRectangles)
                            if (x.Square() > num)
                                Console.WriteLine(x);
                        break;
                    }
                case "6":
                    {
                        double num = GetNum();
                        foreach (var x in AllRectangles)
                            if (x.Square() < num)
                                Console.WriteLine(x);
                        break;
                    }
                case "7":
                    {
                        double num = GetNum();
                        foreach (var x in AllRectangles)
                            if (x.Perimeter() > num)
                                Console.WriteLine(x);
                        break;
                    }
                case "8":
                    {
                        double num = GetNum();
                        foreach (var x in AllRectangles)
                            if (x.Perimeter() < num)
                                Console.WriteLine(x);
                        break;
                    }
            }
        }
        public void Start()
        {
            Console.WriteLine("Приветствую вас в симуляторе геометрии! Вам доступны следующие функции: ");
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
                            int ind = GetInd();
                            Console.WriteLine($"{ind + 1}-й прямоугольник имеет следующие площадь и периметр: " +
                                $"{AllRectangles[ind].Square()} {AllRectangles[ind].Perimeter()}");
                            break;
                        }
                    case "3":
                        {
                            MostRemoteFromCenter();
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Введите номер прямоугольника:");
                            int ind = GetInd();
                            Console.WriteLine("Введите  угол, на который будет повёрнут прямоугольник:");
                            double angle = GetNum();
                            AllRectangles[ind].Turn(angle);
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Введите номер прямоугольника:");
                            int ind = GetInd();
                            Console.WriteLine("Введите сдвиг по Х");
                            double x = GetNum();
                            Console.WriteLine("Введите сдвиг по Y");
                            double y = GetNum();
                            AllRectangles[ind].ShiftXY(x, y);
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("Введите номер прямоугольника:");
                            int ind = GetInd();
                            Console.WriteLine("Введите коэффициент для длины");
                            double len = GetNum();
                            Console.WriteLine("Введите коэффициент для ширины");
                            double wid = GetNum();
                            AllRectangles[ind].Stretch(len, wid);
                            break;
                        }
                    case "7":
                        {
                            Predicate();
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
            Console.WriteLine("Спасибо, что воспользовались нашим симулятором! Всего вам доброго!");
        }
    }
}
