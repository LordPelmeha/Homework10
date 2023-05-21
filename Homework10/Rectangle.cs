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
        private Dot a { get; }
        private Dot b { get; }
        private Dot c { get; }
        private Dot d { get; }
        private bool IsRectangele()
        {
            // Углы между векторами в радианах
            double ab = Math.Acos((a.X * b.X + a.Y * b.Y) / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y)));

            double bc = Math.Acos((c.X * b.X + c.Y * b.Y) / (Math.Sqrt(c.X * c.X + c.Y * c.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y)));

            double cd = Math.Acos((c.X * d.X + c.Y * d.Y) / (Math.Sqrt(c.X * c.X + c.Y * c.Y) * Math.Sqrt(d.X * d.X + d.Y * d.Y)));

            double ad = Math.Acos((a.X * d.X + a.Y * d.Y) / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(d.X * d.X + d.Y * d.Y)));

            if (ab == bc && bc == cd && cd == ad)
                return true;
            return false;
        }
        public double Square() => Math.Sqrt((a.X * a.X + a.Y * a.Y) * (b.X * b.X + b.Y * b.Y));

        public double Perimeter() => (Math.Sqrt(a.X * a.X + a.Y * a.Y) + Math.Sqrt(b.X * b.X + b.Y * b.Y)) * 2;

        public Rectangle(Dot BottomLeft, Dot UpperLeft, Dot UpperRight, Dot BottomRight)
        {
            this.BottomLeft = BottomLeft;
            this.UpperLeft = UpperLeft;
            this.UpperRight = UpperRight;
            this.BottomRight = BottomRight;
            this.Center = new Dot((UpperLeft.X + BottomRight.X) / 2, (UpperLeft.Y + BottomRight.Y) / 2);

            // Координаты векторов прямоугольника по часовой стрелке, начиная с левой стороны
            this.a = new Dot(UpperLeft.X - BottomLeft.X, UpperLeft.Y - BottomLeft.Y);
            this.b = new Dot(UpperRight.X - UpperLeft.X, UpperRight.Y - UpperLeft.Y);
            this.c = new Dot(BottomRight.X - UpperRight.X, BottomRight.Y - UpperRight.Y);
            this.d = new Dot(BottomLeft.X - BottomRight.X, BottomLeft.Y - BottomRight.Y);

            if (!IsRectangele())
                throw new ArgumentException("Данная фигура не является прямоугольником!");
        }
        public override string ToString() => $"{BottomLeft.X} {BottomLeft.Y}, {UpperLeft.X} {UpperLeft.Y}, {UpperRight.X} {UpperRight.Y}, " +
            $"{BottomRight.X} {BottomRight.Y}, {Center.X} {Center.Y}";
    }
}
