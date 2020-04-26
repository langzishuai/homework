using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace reptile_prac_4_13
{

    public class SimpleCrawler
    {
        public event Action<String> PageDownloaded;
        private Hashtable urls = new Hashtable();

        //public Queue myurls = new Queue();

        public ConcurrentQueue<string> myurls = new ConcurrentQueue<string>();  

        public string startUrl = "";
        public int MaxPage { get; set; } //最大下载数量

        public SimpleCrawler()
        {
            MaxPage = 100;
        }

        private string Start
        {
            get
            {
                return startUrl.Split('/')[2];
            }
        }

        private int count = 0;
        static void Main(string[] args)
        {
            
            SimpleCrawler myCrawler = new SimpleCrawler();
            myCrawler.startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) myCrawler.startUrl = args[0];
            myCrawler.urls.Add(myCrawler.startUrl, false);//加入初始页面
            myCrawler.myurls.Enqueue(myCrawler.startUrl);

            new Thread(myCrawler.Crawl).Start();
            
            /*
            string url= "https://www.cnblogs.com/zerosymbol/p/11516136.html";
            string[] ss = url.Split('/');
            foreach (string s in ss)
                System.Console.WriteLine(s);
                */
        }

        public void Crawl()
        {
            Console.WriteLine("开始爬行了.... ");
            myurls = new ConcurrentQueue<string>();
            myurls.Enqueue(startUrl);
            List<Task> tasks = new List<Task>();
            int complatedCount = 0;//已完成的任务数
            PageDownloaded += (str) => { complatedCount++; };
            //修改部分
            /*
            while(count<30&&myurls.Count!=0)
            {
                string current = (string)myurls.Dequeue();
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                count++;
                Parse(html);
                Console.WriteLine("爬行结束");

            }
            */
            while (tasks.Count < MaxPage)
            {
                string url;
                if (!myurls.TryDequeue(out url))
                {
                    if (complatedCount < tasks.Count)
                    {
                        continue;
                    }
                    else
                    {
                        break;//应该是出队速度比任务完成速度要快很多
                    }
                }

                Task task = Task.Run(() => DownLoadAndParse(url));
                //System.Console.WriteLine(1);
                tasks.Add(task);

            }
            //Task.WaitAll(tasks.ToArray()); //等待剩余任务全部执行完毕
        }

        public void DownLoadAndParse(string url)
        {
            try
            {
                string html = DownLoad(url);
                Parse(html);
                PageDownloaded(url);
            }
            catch(Exception ex)
            {
                PageDownloaded(url + "  ERROR:" + ex.Message);
            }
            
        }

        public string DownLoad(string url)
        {
            /*
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                string html = webClient.DownloadString(url);
                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
                PageDownloaded(url);
                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                PageDownloaded(url + "  ERROR:" + ex.Message);
                return "";
            }*/ // 修改一下 事件触发的顺序 一定要在Parse解析完成之后触发！！

            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            string html = webClient.DownloadString(url);
            string fileName = count.ToString();
            File.WriteAllText(fileName, html, Encoding.UTF8);
            //PageDownloaded(url);
            return html;
        }

        private void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+.html.*?[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            
            //修改部分
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (!myurls.Contains(strRef))
                {
                    if (strRef.StartsWith("/"))
                        strRef = startUrl + strRef;
                    else if (strRef.StartsWith("http") || strRef.StartsWith("https")) { }
                    else
                    {
                        strRef = Start + strRef;
                    }
                }
                myurls.Enqueue(strRef);
            }

        }
    }

}
