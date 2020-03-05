using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_2_test
{
    class Program
    {
        static void Main(string[] args)
        {
            int randNum;
            double areaTotal = 0;
            double s = 0;
            Shape myShape = null;
            Factory myFactory = new Factory();
            for (int i = 0; i < 10; i++)
            {
                randNum = new Random().Next(0, 3);
                myShape = myFactory.GenerateShape(randNum);
                s = myShape.ForSize();
                if (s != -1)
                    areaTotal += s;

            }
            Console.WriteLine($"total size is: {areaTotal}");
        }

    }

    class Factory
    {
        public Shape GenerateShape(int shapeClass)
        {
            Random randNum = new Random();
            switch(shapeClass)
            {
                case 0:
                    return new Rectangle(randNum.Next(1, 100), randNum.Next(1, 100));
                case 1:
                    return new Square(randNum.Next(1, 100));
                case 2:
                    return new Triangle(randNum.Next(1, 100), randNum.Next(1, 100), randNum.Next(1, 100));
                default:
                    return null;
            }
        }
    }
}
