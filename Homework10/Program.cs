using System.Text.RegularExpressions;

namespace Homework10
{
    internal class Program
    {
        static void Main()
        {
            var x = new FormulaSimulator();
            x.LoadFormulas("C:\\Users\\Тая\\source\\repos\\LordPelmeha\\Homework10\\Homework10\\MemorizingTheory.txt");
            x.Training();
        }
    }
}