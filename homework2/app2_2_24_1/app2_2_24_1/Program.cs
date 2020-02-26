using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2_2_24_1
{
    class Program
    {
        int[] a;
        int length=0;
        int max;
        int min;
        int sum=0;
        double average;
        static void Main(string[] args)
        {
            Program p1 = new Program();
            p1.createArray();
            Console.WriteLine($"最大值为{p1.getMax()}");
            Console.WriteLine($"最小值为{p1.getMin()}");
            Console.WriteLine($"和为{p1.getSum()}");
            Console.WriteLine($"平均值为{p1.getAverage()}");
        }

        void createArray()
        {
            this.length = 0;
            bool isok = false;
            Console.WriteLine("请输入数组的长度：");
            isok = int.TryParse(Console.ReadLine(), out length);
            while(!isok||length<=0)
            {
                Console.WriteLine("数组长度应为正整数。");
                isok = int.TryParse(Console.ReadLine(), out length);
            }
            this.a = new int[length];
            Console.WriteLine("请输入数组中的数字");
            for (int i = 0; i < this.length; i++)
            {
                int.TryParse(Console.ReadLine(), out a[i]);
                if(i==0)
                    this.max = this.min = a[i];
                else
                {
                    if (a[i] > max)
                        max = a[i];
                    if (a[i] < min)
                        min = a[i];
                }
                this.sum += a[i];
            }
            this.average = sum * 1.0 / length;

        }

        int getMax() { return this.max; }

        int getMin() { return this.min; }

        int getSum() { return this.sum; }

        double getAverage() { return this.average; }

        void showArray()
        {
            for (int i = 0; i < this.length; i++)
                Console.WriteLine(a[i]);
        }


    }
}
