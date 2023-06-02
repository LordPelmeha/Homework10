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
            Console.WriteLine("Здравствуйте! Приветствую вас в симуляторе математики. Вам доступны следующие функции:");
            Console.WriteLine("1) Симулятор геометрии");
            Console.WriteLine("2) Проверка знаний");
            Console.WriteLine("3) Зазубривание теории");
            string ans = Console.ReadLine();
            while (ans.Length != 1 || !"12345678".Contains(ans))
            {
                Console.WriteLine("Вы пытаетесь поломать симулятор! Фу таким быть. Введите один из номеров функциий, которые были вам предложены:");
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
//Код Лероя было легко понять из-за оставленных им комментариев и понятных названий перемменных. У него в одном файле лежало несколько классов, что меня не устроило,
//поэтому я раскидал их по разным файлам. Ранее написанный код, по большей части, исправлять не пришлось. Только дополнять в соответствии с новым заданием. 
//Разобраться в нём оказалось довольно просто, опять же, благодаря чистому коду Лероя.