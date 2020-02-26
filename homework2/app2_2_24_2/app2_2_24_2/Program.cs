using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app2_2_24_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[101];
            for(int i=2;i*2<101;i++)
            {
                for (int j = 2; j * i < 101; j++)
                    if (a[j * i] == 0)
                        a[j * i] = 1;
            }
            Console.WriteLine("2--100的素数有:");
            for(int i=2;i<101;i++)
                if (a[i]==0)
                    Console.Write($"  {i}");
        }
    }
}
