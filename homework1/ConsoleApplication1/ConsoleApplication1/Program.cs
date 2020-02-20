using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            double num1 = Double.NaN, num2 = Double.NaN;
            String mySymble = "";
            num1 = getNumber();
            mySymble = getSymble();
            num2 = getNumber();
            caculate(num1, num2, mySymble);

        }

        static double getNumber()
        {
            double ret = double.NaN;//做输入数字的返回值
            
            Console.WriteLine("请输入数字：");
            bool isRight = Double.TryParse(Console.ReadLine(), out ret);
            while(!isRight)
            {
                Console.WriteLine("请正确输入数字：");
                isRight = Double.TryParse(Console.ReadLine(), out ret);
            }

            return ret;
        }

        static string getSymble()
        {
            string ret = "";
            Console.WriteLine("请输入运算符：a对应+  s对应-    m对应*    d对应/");
            ret = Console.ReadLine();
            while(!(ret=="a"||ret=="s"||ret=="m"||ret=="d"))
            {
                Console.WriteLine("请正确输入运算符！！！！规则如下：a对应+  s对应-    m对应*    d对应/");
                ret = Console.ReadLine();
            }


            return ret;
        }

        static void caculate(double num1,double num2,string sym)
        {
            try
            {
                switch(sym)
                {
                    case "a":
                        Console.WriteLine($"运算结果为：{num1 + num2}");
                        break;
                    case "s":
                        Console.WriteLine($"运算结果为：{num1 - num2}");
                        break;
                    case "m":
                        Console.WriteLine($"运算结果为：{num1 * num2}");
                        break;
                    case "d":
                        if (num2 != 0)
                            Console.WriteLine($"运算结果为：{num1 / num2}");
                        else
                            Console.WriteLine("除数为零，非法运算，无结果！");
                        break;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("非法运算！！");
            }
        }
    }
}
