using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    internal class Rectangle
    {
        private Dot _bottomLeft;
        private Dot _upperLeft;
        private Dot _upperRight;
        private Dot _bottomRight;
        public Dot Center { get; }
        public Dot BottomLeft
        {
            get
            {
                return _bottomLeft;
            }
            set
            {
                if (IsRectangele(_bottomLeft, _upperLeft, _upperRight, _bottomRight))
                    _bottomLeft = value;
                else
                    throw new ArgumentException("Данные точки не образуют прямоугольник");
            }
        }
        public Dot UpperLeft
        {
            get
            {
                return _upperLeft;
            }
            set
            {
                if (IsRectangele(_bottomLeft, _upperLeft, _upperRight, _bottomRight))
                    _upperLeft = value;
                else
                    throw new ArgumentException("Данные точки не образуют прямоугольник");
            }
        }
        public Dot UpperRight
        {
            get
            {
                return _upperRight;
            }
            set
            {
                if (IsRectangele(_bottomLeft, _upperLeft, _upperRight, _bottomRight))
                    _upperRight = value;
                else
                    throw new ArgumentException("Данные точки не образуют прямоугольник");
            }
        }
        public Dot BottomRight
        {
            get
            {
                return _bottomRight;
            }
            set
            {
                if (IsRectangele(_bottomLeft, _upperLeft, _upperRight, _bottomRight))
                    _bottomRight = value;
                else
                    throw new ArgumentException("Данные точки не образуют прямоугольник");
            }
        }

        private bool IsRectangele(Dot a, Dot b, Dot c, Dot d)
        {
            int ab = (int)(Math.PI / 180 * Math.Acos((a.X * b.X + a.Y * b.Y) / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y))));
            int bc = (int)(Math.PI / 180 * Math.Acos((c.X * b.X + c.Y * b.Y) / (Math.Sqrt(c.X * c.X + c.Y * c.Y) * Math.Sqrt(b.X * b.X + b.Y * b.Y))));
            int cd = (int)(Math.PI / 180 * Math.Acos((c.X * d.X + c.Y * d.Y) / (Math.Sqrt(c.X * c.X + c.Y * c.Y) * Math.Sqrt(d.X * d.X + d.Y * d.Y))));
            int ad = (int)(Math.PI / 180 * Math.Acos((a.X * d.X + a.Y * d.Y) / (Math.Sqrt(a.X * a.X + a.Y * a.Y) * Math.Sqrt(d.X * d.X + d.Y * d.Y))));
            if (ab == bc && bc == cd && cd == ad && ad == 90)
                return true;
            return false;
        }

    }
}
