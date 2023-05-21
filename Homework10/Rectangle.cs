﻿using System;
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
        private Dot a { get; set; }
        private Dot b { get; set; }
        private Dot c { get; set; }
        private Dot d { get; set; }
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
        private void GetVectors()
        {
            a = new Dot(UpperLeft.X - BottomLeft.X, UpperLeft.Y - BottomLeft.Y);
            b = new Dot(UpperRight.X - UpperLeft.X, UpperRight.Y - UpperLeft.Y);
            c = new Dot(BottomRight.X - UpperRight.X, BottomRight.Y - UpperRight.Y);
            d = new Dot(BottomLeft.X - BottomRight.X, BottomLeft.Y - BottomRight.Y);
            Console.WriteLine($"a: {a.X} {a.Y}, b: {b.X} {b.Y}, c: {c.X} {c.Y}, d: {d.X} {d.Y}");
        }
        public double Square() => Math.Sqrt((a.X * a.X + a.Y * a.Y) * (b.X * b.X + b.Y * b.Y));

        public double Perimeter() => (Math.Sqrt(a.X * a.X + a.Y * a.Y) + Math.Sqrt(b.X * b.X + b.Y * b.Y)) * 2;

        public double DistanceToCenter() => Math.Sqrt(
            Math.Min(BottomLeft.X * BottomLeft.X + BottomLeft.Y * BottomLeft.Y,
                Math.Min(UpperLeft.X * UpperLeft.X + UpperLeft.Y * UpperLeft.Y,
                    Math.Min(UpperRight.X * UpperRight.X + UpperRight.Y * UpperRight.Y, BottomRight.X * BottomRight.X + BottomRight.Y * BottomRight.Y))));
        public void Turn(double angle)
        {
            double rad = angle * Math.PI / 180;
            double cos = Math.Round(Math.Cos(rad), 6);
            double sin = Math.Round(Math.Sin(rad), 6);
            Dot NotBottomLeft = new Dot(BottomLeft.X, BottomLeft.Y);
            Dot NotUpperLeft = new Dot(UpperLeft.X, UpperLeft.Y);
            Dot NotUpperRight = new Dot(UpperRight.X, UpperRight.Y);
            Dot NotBottomRight = new Dot(BottomRight.X, BottomRight.Y);
            BottomLeft.X = (NotBottomLeft.X - Center.X) * cos - (NotBottomLeft.Y - Center.Y) * sin + Center.X;
            BottomLeft.Y = (NotBottomLeft.X - Center.X) * sin + (NotBottomLeft.Y - Center.Y) * cos + Center.Y;
            UpperLeft.X = (NotUpperLeft.X - Center.X) * cos - (NotUpperLeft.Y - Center.Y) * sin + Center.X;
            UpperLeft.Y = (NotUpperLeft.X - Center.X) * sin + (NotUpperLeft.Y - Center.Y) * cos + Center.Y;
            UpperRight.X = (NotUpperRight.X - Center.X) * cos - (NotUpperRight.Y - Center.Y) * sin + Center.X;
            UpperRight.Y = (NotUpperRight.X - Center.X) * sin + (NotUpperRight.Y - Center.Y) * cos + Center.Y;
            BottomRight.X = (NotBottomRight.X - Center.X) * cos - (NotBottomRight.Y - Center.Y) * sin + Center.X;
            BottomRight.Y = (NotBottomRight.X - Center.X) * sin + (NotBottomRight.Y - Center.Y) * cos + Center.Y;
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
        public override string ToString() => $"{BottomLeft.X} {BottomLeft.Y}, {UpperLeft.X} {UpperLeft.Y}, {UpperRight.X} {UpperRight.Y}, " +
            $"{BottomRight.X} {BottomRight.Y}";
    }
}
