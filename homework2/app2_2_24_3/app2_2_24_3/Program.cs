using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2_2_24_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            bool isok = false;
            Console.WriteLine("请输入数字:");
            isok = int.TryParse(Console.ReadLine(), out num);
            while(!isok)
            {
                Console.WriteLine("请正确输入数字:");
                isok = int.TryParse(Console.ReadLine(), out num);
            }
            showSushu(num);
        }

        static void showSushu(int num)
        {
            Console.WriteLine("质数因子有：");
            for (int i = 2; i*i<=num; i++)
            {
                while(num%i==0)
                {
                    Console.Write($"  {i}");
                    num /= i;
                }
            }
            if(num!=1)
                Console.Write($"  {num}");
        }

    }
}
