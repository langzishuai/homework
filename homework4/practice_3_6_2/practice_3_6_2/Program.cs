using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace practice_3_6_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock myClock = new Clock();
            myClock.Alarm += Alarming;
            myClock.Tick += TimeGo;
            myClock.setAlarmTime(17, 22, 30);

        }

        static void TimeGo(object sender,TimeEventArgs args)
        {
            Console.WriteLine("滴答...");
        }
        
        static void Alarming(object sender,TimeEventArgs args)
        {
            Console.WriteLine("闹钟响啦。。叮叮叮叮叮叮叮叮叮");
        }

    }

    public delegate void TickHandler(object sender, TimeEventArgs args);
    public delegate void AlarmHandler(object sender, TimeEventArgs args);

    public class TimeEventArgs
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }

    }

    public class Clock
    {
        public event TickHandler Tick;
        public event AlarmHandler Alarm;

        public TimeEventArgs timeNow=new TimeEventArgs();

        public void setAlarmTime(int hour,int minute,int second)
        {
            while(true)
            {
                timeNow.Hour = DateTime.Now.Hour;
                timeNow.Minute = DateTime.Now.Minute;
                timeNow.Second = DateTime.Now.Second;
                Console.WriteLine($"当前时间: {timeNow.Hour}:{timeNow.Minute}:{timeNow.Second}");
                Tick(this, timeNow);
                if (timeNow.Hour == hour && timeNow.Minute == minute && timeNow.Second == second)
                    Alarm(this, timeNow);
                Thread.Sleep(1000);
            }
        }
    }
}
