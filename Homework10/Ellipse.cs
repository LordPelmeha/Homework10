using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10
{
    public class Ellipse
    {
        private Dot _center { get; set; }
        public Dot _toppnt { get; private set; }
        public Dot _downpnt { get; private set; }
        public Dot _leftpnt { get; private set; }
        public Dot _rightpnt { get; private set; }
        private Dot _a { get; set; }
        private Dot a { get; set; }
        private Dot _b { get; set; }
        private Dot b { get; set; }
        /// <summary>
        /// Возвращает большие и малые полуоси эллипса в удобном виде
        /// </summary>
        private void GetSemiaxis()
        {
            a = new Dot(_rightpnt.X-_center.X,_rightpnt.Y-_center.Y);
            b = new Dot(_toppnt.X-_center.X, _toppnt.Y-_center.Y);
            _a = new Dot(_leftpnt.X-_center.X,_leftpnt.Y-_center.Y);
            _b = new Dot(_downpnt.X-_center.X,_downpnt.Y-_center.Y);
        }

        // <summary>
        /// Находит периметр эллипса
        /// </summary>
        public double Perimeter() => Math.PI * (Math.Sqrt((a.X * a.X) + (a.Y * a.Y)+(b.X * b.X) + (b.Y * b.Y))/2);


        /// <summary>
        /// Находит площадь эллипса
        /// </summary>
        /// <returns></returns>
        public double Square() => Math.PI * Math.Sqrt((a.X * a.X) + (a.Y * a.Y)) * Math.Sqrt((b.X * b.X) + (b.Y * b.Y));


        /// <summary>
        /// Находит расстояние от центра фигуры до начала координат
        /// </summary>
        /// <returns></returns>
        public double DistanceToCenter() => Math.Sqrt(Math.Pow(0 - _center.X, 2) + Math.Pow(0 - _center.Y, 2));

        /// <summary>
        /// Поворачивает эллипс на заданный угол (в градусах)
        /// </summary>
        public void Turn(double angle)
        {
            double rad = angle * Math.PI / 180;
            double cos = Math.Round(Math.Cos(rad), 6);
            double sin = Math.Round(Math.Sin(rad), 6);
            Dot NotLeft = new Dot(_leftpnt.X, _leftpnt.Y);
            Dot NotTop = new Dot(_toppnt.X, _toppnt.Y);
            Dot NotRight = new Dot(_rightpnt.X, _rightpnt.Y);
            Dot NotDown = new Dot(_downpnt.X, _downpnt.Y);

            _leftpnt = new Dot((NotLeft.X - _center.X) * cos - (NotLeft.Y - _center.Y) * sin + _center.X,
                (NotLeft.X - _center.X) * sin + (NotLeft.Y - _center.Y) * cos + _center.Y);
                
            _toppnt = new Dot((NotTop.X - _center.X) * cos - (NotTop.Y - _center.Y) * sin + _center.X,
                (NotTop.X - _center.X) * sin + (NotTop.Y - _center.Y) * cos + _center.Y);

            _rightpnt = new Dot((NotRight.X - _center.X) * cos - (NotRight.Y - _center.Y) * sin + _center.X,
                (NotRight.X - _center.X) * sin + (NotRight.Y - _center.Y) * cos + _center.Y);

            _downpnt = new Dot((NotDown.X - _center.X) * cos - (NotDown.Y - _center.Y) * sin + _center.X,
                (NotDown.X - _center.X) * sin + (NotDown.Y - _center.Y) * cos + _center.Y);
            GetSemiaxis();
        }

        /// <summary>
        /// Двигает эллипс по оси X и оси Y на заданные значения
        /// </summary>
        public void ShiftXY(double x = 0, double y = 0)
        {
            _leftpnt = new Dot(_leftpnt.X + x, _leftpnt.Y + y);
            _toppnt = new Dot(_toppnt.X + x, _toppnt.Y + y);
            _rightpnt = new Dot(_rightpnt.X + x, _rightpnt.Y + y);
            _downpnt = new Dot(_downpnt.X + x, _downpnt.Y + y);
            _center = new Dot((_center.X + _center.X) / 2, (_center.Y + _center.Y) / 2);
            GetSemiaxis();
        }

        /// <summary>
        /// Увеличение высоты и ширины эллипса на заданные коэффициенты
        /// </summary>
        public void Stretch(double lengthhtScale = 1, double widthScale = 1)
        {
            lengthhtScale -= 1;
            widthScale -= 1;
            Dot NotLeft = new Dot(_leftpnt.X, _leftpnt.Y);
            Dot NotTop = new Dot(_toppnt.X, _toppnt.Y);
            Dot NotRight = new Dot(_rightpnt.X, _rightpnt.Y);
            Dot NotDown = new Dot(_downpnt.X, _downpnt.Y);
            _leftpnt = new Dot(NotLeft.X + (NotLeft.X - NotRight.X) / 2 * widthScale, NotLeft.Y + (NotLeft.Y - NotRight.Y) / 2 * lengthhtScale);
            _toppnt = new Dot(NotTop.X + (NotTop.X - NotDown.X) / 2 * widthScale, NotTop.Y + (NotTop.Y - NotDown.Y) / 2 * lengthhtScale);
            _rightpnt = new Dot(NotRight.X + (NotRight.X - NotLeft.X) / 2 * widthScale, NotRight.Y + (NotRight.Y - NotLeft.Y) / 2 * lengthhtScale);
            _downpnt = new Dot(NotDown.X + (NotDown.X - NotTop.X) / 2 * widthScale, NotDown.Y + (NotDown.Y - NotTop.Y) / 2 * lengthhtScale);
            GetSemiaxis();
        }

        public Ellipse(Dot top, Dot left, Dot down, Dot right)
        {
            this._toppnt = top;
            this._leftpnt = left;
            this._downpnt = down;
            this._rightpnt = right;
            this._center = new Dot((left.X + right.X) / 2, (top.Y + down.Y) / 2);

            GetSemiaxis();



            
        }

        public bool IsCircle()
        {
            {
               
                double A = Math.Sqrt(Math.Pow(a.X,2)+ Math.Pow(a.Y, 2));
                double _A = Math.Sqrt(Math.Pow(_a.X, 2) + Math.Pow(_a.Y, 2));
                double B = Math.Sqrt(Math.Pow(b.X, 2) + Math.Pow(b.Y, 2));
                double _B = Math.Sqrt(Math.Pow(_b.X, 2) + Math.Pow(_b.Y, 2));
                if (A == _A && B == _B )
                    return true;
                return false;
            }
        }

        public override string ToString() => $"{_center.X}{_center.Y}{_toppnt.X}{_toppnt.Y}{_downpnt.X}{_downpnt.Y}{_leftpnt.X}{_leftpnt.Y}{_rightpnt.X}{_rightpnt.Y}";
    }
}
