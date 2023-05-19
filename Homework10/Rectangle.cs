using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    public class Rectangle
    {
        public Dot Center { get; }
        public Dot BottomLeft { get; private set; }
        public Dot UpperLeft { get; private set; }
        public Dot UpperRight { get; private set; }
        public Dot BottomRight { get; private set; }

        private bool IsRectangele()
        {
            // Координаты векторов прямоугольника, начиная с левого
            Dot a = new Dot(UpperLeft.X - BottomLeft.X, UpperLeft.Y - BottomLeft.Y);
            Dot b = new Dot(UpperRight.X - UpperLeft.X, UpperRight.Y - UpperLeft.Y);
            Dot c = new Dot(BottomRight.X - UpperRight.X, BottomRight.Y - UpperRight.Y);
            Dot d = new Dot(BottomLeft.X - BottomRight.X, BottomLeft.Y - BottomRight.Y);

            // Углы между векторами в радианах
            double ab = Math.Acos((a.X * b.X + a.Y * b.Y) / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y)));

            double bc = Math.Acos((c.X * b.X + c.Y * b.Y) / (Math.Sqrt(c.X * c.X + c.Y * c.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y)));

            double cd = Math.Acos((c.X * d.X + c.Y * d.Y) / (Math.Sqrt(c.X * c.X + c.Y * c.Y) * Math.Sqrt(d.X * d.X + d.Y * d.Y)));

            double ad = Math.Acos((a.X * d.X + a.Y * d.Y) / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(d.X * d.X + d.Y * d.Y)));

            if (ab == bc && bc == cd && cd == ad)
                return true;
            return false;
        }
        public Rectangle(Dot BottomLeft, Dot UpperLeft, Dot UpperRight, Dot BottomRight)
        {
            this.BottomLeft = BottomLeft;
            this.UpperLeft = UpperLeft;
            this.UpperRight = UpperRight;
            this.BottomRight = BottomRight;
            this.Center = new Dot((UpperLeft.X + BottomRight.X) / 2, (UpperLeft.Y + BottomRight.Y) / 2);

            if (!IsRectangele())
                throw new ArgumentException("Данная фигура не является прямоугольником!");
        }
        public override string ToString() => $"{BottomLeft.X} {BottomLeft.Y}, {UpperLeft.X} {UpperLeft.Y}, {UpperRight.X} {UpperRight.Y}, " +
            $"{BottomRight.X} {BottomRight.Y}, {Center.X} {Center.Y}";
    }
}
