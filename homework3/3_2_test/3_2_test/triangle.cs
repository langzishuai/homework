using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_2_test
{
    class Triangle:Shape
    {
        private double a;
        private double b;
        private double c;

        public Triangle(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }
        public Triangle() : this(0, 0, 0) { }

        public double A
        {
            get { return a; }
            set
            {
                if (value <=0)
                {
                    Console.WriteLine("边长不合法.");
                }
                else
                    a = value;
            }
        }

        public double B
        {
            get { return b; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("边长不合法.");
                }
                else
                    b = value;
            }
        }
        
        public double C
        {
            get { return c; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("边长不合法.");
                }
                else
                    c = value;
            }
        }

        override public double ForSize()
        {
            if(isLeagal())
            {
                double p = (A + B + C) / 2;
                return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            }

            return -1;
        }

        public override bool isLeagal()
        {
            return a > 0 && b > 0 && c > 0 & a + b > c && a + c > b && b + c > a;
        }
    }
}
