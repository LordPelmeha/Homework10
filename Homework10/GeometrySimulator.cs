using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    public class GeometrySimulator
    {
        List<Rectangle> AllRectangles = new List<Rectangle>();
        public void AddRectangle()
        {
            double[] ar = Console.ReadLine().Replace('.', ',').Split().Select(x => double.Parse(x)).ToArray();
            AllRectangles.Add(new Rectangle(new Dot(ar[0], ar[1]), new Dot(ar[2], ar[3]), new Dot(ar[4], ar[5]), new Dot(ar[6], ar[7])));
        }
    }
}
