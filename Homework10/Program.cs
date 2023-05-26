using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    internal class Program
    {
        static void Main()
        {
            var start = new GeometrySimulator().Start;
            start();
            //var a = new Rectangle(new Dot(0, 0), new Dot(0, 1), new Dot(1, 1), new Dot(1, 0));
            //Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //a.Stretch(2, 2);
            //Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //a.Turn(90);
            //Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //a.ShiftXY(1, 1);
            //Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //var b = new GeometrySimulator();
            //b.AddRectangle();
            //b.AddRectangle();
            //b.MostRemoteFromCenter();
           //StartingTheProgram stp = new StartingTheProgram();
            //stp.StartKnowledgeCheck();
        }
    }
}