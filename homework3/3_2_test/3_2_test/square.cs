using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_2_test
{
    class Square:Shape
    {
        private double a;//边长
        public double A
        {
            get { return a; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("边长不合法。");
                }
                else
                    a = value;
            }
        }
        public Square(double a)
        {
            A = a;
        }
        public Square() : this(0) { }


        override public double ForSize()
        {
            if (isLeagal())
                return a * a;

            return -1;
        }

        public override bool isLeagal()
        {
            return a >= 0;
        }
    }
}
