using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_2_test
{
    class Rectangle:Shape
    {
        private double length;
        private double width;

        public Rectangle(double length, double width)
        {
            Length = length;
            Width = width;
        }
        public Rectangle() : this(0, 0) { }

        public double Length
        {
            get { return length; }
            set
            {
                if(value<0)
                {
                    Console.WriteLine("长应该大于等于零。");
                }else
                {
                    length = value;
                }
            }
        }
        
        public double Width
        {
            get { return width; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("长应该大于等于零。");
                }
                else
                {
                    width = value;
                }
            }
        }
        override public double ForSize()
        {
            if (isLeagal())
                return Length * Width;

            return -1;
        }

        public override bool isLeagal()
        {
            return length >= 0 && width >= 0;
        }
    }
}
