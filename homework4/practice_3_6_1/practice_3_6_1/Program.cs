using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice_3_6_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建测试表
            GenericList<int> numList = new GenericList<int>();
            for (int x = 1; x <= 20; x++)
            {
                numList.Add(x);
            }
            
            //打印元素
            Console.WriteLine("打印元素：");
            numList.Foreach(x => Console.WriteLine(x));
            //求最大值
            Console.WriteLine("求最大值：");
            int max = numList.Head.Data;
            numList.Foreach(x => { if (x > max) max = x; });
            Console.WriteLine($"最大值为：{max}");
            //求最小值
            Console.WriteLine("求最小值: ");
            int min = numList.Head.Data;
            numList.Foreach(x => { if (x < min) min = x; });
            Console.WriteLine($"最小值为：{min}");
            //求和
            Console.WriteLine("求和： ");
            int sum = 0;
            numList.Foreach(x => sum += x);
            Console.WriteLine($"和为：{sum}");
        }
    }

    class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }

        public Node(T data)
        {
            Next = null;
            Data = data;
        }

    }

    class GenericList<T>
    {
        private Node<T> tail;
        private Node<T> head;

        public GenericList()
        {
            tail = head = null;
        }

        public Node<T> Head
        {
            get { return head; }
        }

        public void Add(T d)
        {
            Node<T> n = new Node<T>(d);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }

        public void Foreach(Action<T> action)
        {
            Node<T> temp = head;
            while (temp.Next != null)
            {
                action(temp.Data);
                temp = temp.Next;
            }
            action(temp.Data);
        }
    }
        



    
}
