using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    public class Rectangle
    {
        private Dot Center { get; set; }
        public Dot BottomLeft { get; private set; }
        public Dot UpperLeft { get; private set; }
        public Dot UpperRight { get; private set; }
        public Dot BottomRight { get; private set; }
        private Dot a { get; set; }
        private Dot b { get; set; }
        private Dot c { get; set; }
        private Dot d { get; set; }
        /// <summary>
        /// Проверяет, является ли данный чётырёхугольник прямоугольником
        /// </summary>
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
        /// <summary>
        /// Получает вектора четырёхугольника
        /// </summary>
        private void GetVectors()
        {
            a = new Dot(UpperLeft.X - BottomLeft.X, UpperLeft.Y - BottomLeft.Y);
            b = new Dot(UpperRight.X - UpperLeft.X, UpperRight.Y - UpperLeft.Y);
            c = new Dot(BottomRight.X - UpperRight.X, BottomRight.Y - UpperRight.Y);
            d = new Dot(BottomLeft.X - BottomRight.X, BottomLeft.Y - BottomRight.Y);
            //Console.WriteLine($"a: {a.X} {a.Y}, b: {b.X} {b.Y}, c: {c.X} {c.Y}, d: {d.X} {d.Y}");
        }
        /// <summary>
        /// Находит площадь четырёхугольника
        /// </summary>
        public double Square() => Math.Sqrt((a.X * a.X + a.Y * a.Y) * (b.X * b.X + b.Y * b.Y));
        /// <summary>
        /// Находит периметр четырёхугольника
        /// </summary>
        public double Perimeter() => (Math.Sqrt(a.X * a.X + a.Y * a.Y) + Math.Sqrt(b.X * b.X + b.Y * b.Y)) * 2;
        /// <summary>
        /// Находит дистанцию от центра координат до ближайшего угла четырёхугольника
        /// </summary>
        public double DistanceToCenter() => Math.Sqrt(
            Math.Min(BottomLeft.X * BottomLeft.X + BottomLeft.Y * BottomLeft.Y,
                Math.Min(UpperLeft.X * UpperLeft.X + UpperLeft.Y * UpperLeft.Y,
                    Math.Min(UpperRight.X * UpperRight.X + UpperRight.Y * UpperRight.Y, BottomRight.X * BottomRight.X + BottomRight.Y * BottomRight.Y))));
        /// <summary>
        /// Поворачивает четырёхугольник на заданный угол (в градусах)
        /// </summary>
        public void Turn(double angle)
        {
            double rad = angle * Math.PI / 180;
            double cos = Math.Round(Math.Cos(rad), 6);
            double sin = Math.Round(Math.Sin(rad), 6);
            Dot NotBottomLeft = new Dot(BottomLeft.X, BottomLeft.Y);
            Dot NotUpperLeft = new Dot(UpperLeft.X, UpperLeft.Y);
            Dot NotUpperRight = new Dot(UpperRight.X, UpperRight.Y);
            Dot NotBottomRight = new Dot(BottomRight.X, BottomRight.Y);

            BottomLeft = new Dot((NotBottomLeft.X - Center.X) * cos - (NotBottomLeft.Y - Center.Y) * sin + Center.X,
                (NotBottomLeft.X - Center.X) * sin + (NotBottomLeft.Y - Center.Y) * cos + Center.Y);

            UpperLeft = new Dot((NotUpperLeft.X - Center.X) * cos - (NotUpperLeft.Y - Center.Y) * sin + Center.X,
                (NotUpperLeft.X - Center.X) * sin + (NotUpperLeft.Y - Center.Y) * cos + Center.Y);

            UpperRight = new Dot((NotUpperRight.X - Center.X) * cos - (NotUpperRight.Y - Center.Y) * sin + Center.X,
                (NotUpperRight.X - Center.X) * sin + (NotUpperRight.Y - Center.Y) * cos + Center.Y);

            BottomRight = new Dot((NotBottomRight.X - Center.X) * cos - (NotBottomRight.Y - Center.Y) * sin + Center.X,
                (NotBottomRight.X - Center.X) * sin + (NotBottomRight.Y - Center.Y) * cos + Center.Y);
            GetVectors();
        }
        /// <summary>
        /// Двигает четырёхугольник по оси X и оси Y на заданные значения
        /// </summary>
        public void ShiftXY(double x = 0, double y = 0)
        {
            BottomLeft = new Dot(BottomLeft.X + x, BottomLeft.Y + y);
            UpperLeft = new Dot(UpperLeft.X + x, UpperLeft.Y + y);
            UpperRight = new Dot(UpperRight.X + x, UpperRight.Y + y);
            BottomRight = new Dot(BottomRight.X + x, BottomRight.Y + y);
            Center = new Dot((UpperLeft.X + BottomRight.X) / 2, (UpperLeft.Y + BottomRight.Y) / 2);
            GetVectors();
        }
        /// <summary>
        /// Увеличение высоты и ширины прямоугольника на заданные коэффициенты
        /// </summary>
        public void Stretch(double lengthhtScale = 1, double widthScale = 1)
        {
            lengthhtScale -= 1;
            widthScale -= 1;
            Dot NotBottomLeft = new Dot(BottomLeft.X, BottomLeft.Y);
            Dot NotUpperLeft = new Dot(UpperLeft.X, UpperLeft.Y);
            Dot NotUpperRight = new Dot(UpperRight.X, UpperRight.Y);
            Dot NotBottomRight = new Dot(BottomRight.X, BottomRight.Y);
            UpperLeft = new Dot(NotUpperLeft.X + (NotUpperLeft.X - NotUpperRight.X) / 2 * widthScale, NotUpperLeft.Y + (NotUpperLeft.Y - NotBottomLeft.Y) / 2 * lengthhtScale);
            UpperRight = new Dot(NotUpperRight.X + (NotUpperRight.X - NotUpperLeft.X) / 2 * widthScale, NotUpperRight.Y + (NotUpperRight.Y - NotBottomRight.Y) / 2 * lengthhtScale);
            BottomLeft = new Dot(NotBottomLeft.X + (NotBottomLeft.X - BottomRight.X) / 2 * widthScale, NotBottomLeft.Y + (NotBottomLeft.Y - NotUpperLeft.Y) / 2 * lengthhtScale);
            BottomRight = new Dot(NotBottomRight.X + (NotBottomRight.X - NotBottomLeft.X) / 2 * widthScale, NotBottomRight.Y + (NotBottomRight.Y - NotUpperRight.Y) / 2 * lengthhtScale);
            GetVectors();
        }
        public Rectangle(Dot BottomLeft, Dot UpperLeft, Dot UpperRight, Dot BottomRight)
        {
            this.BottomLeft = BottomLeft;
            this.UpperLeft = UpperLeft;
            this.UpperRight = UpperRight;
            this.BottomRight = BottomRight;
            this.Center = new Dot((UpperLeft.X + BottomRight.X) / 2, (UpperLeft.Y + BottomRight.Y) / 2);

            GetVectors();

            if (!IsRectangele())
                throw new ArgumentException("Данная фигура не является прямоугольником!");
        }
        public override string ToString() => $"{BottomLeft.X} {BottomLeft.Y}; {UpperLeft.X} {UpperLeft.Y}; {UpperRight.X} {UpperRight.Y}; " +
            $"{BottomRight.X} {BottomRight.Y}";
    }
}
