namespace Homework10
{
    internal class Program
    {
        static void Main()
        {
            var a = new Rectangle(new Dot(1, 1), new Dot(1, 2), new Dot(2, 2), new Dot(2, 1));
            Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            a.Strech(-1,-1);
            Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //a.Turn(90);
            //Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //a.ShiftXY(1, 1);
            //Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()} {a.DistanceToCenter()}");
            //var b = new GeometrySimulator();
            //b.AddRectangle();
            //b.AddRectangle();
            //b.MostRemoteFromCenter();
        }
    }
}