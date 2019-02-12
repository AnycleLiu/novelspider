using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace NovelSpider.Core
{
    public class CrawlQueue
    {
        private static volatile CrawlQueue _instance;
        private static object _lockObj = new object();

        private ConcurrentQueue<ICrawlTask> tasks;

        private CrawlQueue()
        {
            tasks = new ConcurrentQueue<ICrawlTask>();
            Start(Convert.ToInt32(Configuration.config["CrawTaskNum"]));
        }

        public static CrawlQueue Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CrawlQueue();
                        }
                    }
                }
                return _instance;
            }
        }

        private void Start(int concurrentNum = 5)
        {
            Console.WriteLine("开启{0}个线程，开始读取CrawlQueue，处理Task", concurrentNum);
            for (int i = 1; i <= concurrentNum; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((state) =>
               {
                   ICrawlTask task = null;
                   while (true)
                   {
                       if (tasks.TryDequeue(out task))
                       {
                           try
                           {
                               task.Do();
                           }
                           catch (Exception ex)
                           {
                               Console.WriteLine("执行Task({0})失败,{1}", task?.Context?.Url.AbsoluteUri, ex.ToString());
                           }
                       }
                       else
                       {
                           Thread.Sleep(500);
                       }
                   }
               }));
            }
        }

        public void EnterTask(ICrawlTask task)
        {
            tasks.Enqueue(task);
        }
    }
}
