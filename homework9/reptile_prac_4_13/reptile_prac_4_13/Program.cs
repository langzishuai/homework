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

namespace reptile_prac_4_13
{

    public class SimpleCrawler
    {
        public event Action<String> PageDownloaded;
        private Hashtable urls = new Hashtable();

        public Queue myurls = new Queue();

        public string startUrl = "";
        
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
            /*
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url]) continue;
                    current = url;
                }

                if (current == null || count > 30) break;
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                urls[current] = true;
                count++;
                Parse(html);//解析,并加入新的链接
                Console.WriteLine("爬行结束");
            }
            */
            
            //修改部分
            while(count<30&&myurls.Count!=0)
            {
                string current = (string)myurls.Dequeue();
                Console.WriteLine("爬行" + current + "页面!");
                string html = DownLoad(current); // 下载
                count++;
                Parse(html);
                Console.WriteLine("爬行结束");

            }
            
        }

        public string DownLoad(string url)
        {
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
            }
        }

        private void Parse(string html)
        {
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+.html.*?[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            /*
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1)
                          .Trim('"', '\"', '#', '>');
                if (strRef.Length == 0) continue;
                if (urls[strRef] == null) urls[strRef] = false;
            }
            */
            
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
