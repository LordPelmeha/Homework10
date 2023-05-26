using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Homework10
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Здравствуйте! Приветствую вас в симуляторе математики. Вам доступны следующие симуляторы:");
            Console.WriteLine("1) Симулятор геометрии");
            Console.WriteLine("2) Проверка знаний");
            Console.WriteLine("3) Зазубривание теории");
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
                        var start = new GeometrySimulator().Start;
                        start();
                        break;
                    }
                case "2":
                    {
                        StartingTheProgram stp = new StartingTheProgram();
                        stp.StartKnowledgeCheck();
                        break;
                    }
                case "3":
                    {
                        var x = new FormulaSimulator();
                        x.LoadFormulas("MemorizingTheory.txt");
                        x.Training();
                        break;
                    }
            }
        }
    }
}