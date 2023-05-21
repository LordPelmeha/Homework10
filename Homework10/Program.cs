namespace Homework10
{
    internal class Program
    {
        static void Main()
        {
            var a = new Rectangle(new Dot(0, 0), new Dot(0, 1), new Dot(1, 1), new Dot(1, 0));
            Console.WriteLine($"{a}\n{a.Square()} {a.Perimeter()}");
        }
    }
}