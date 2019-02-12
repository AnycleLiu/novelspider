using System;
using NovelSpider.Core;
using NovelSpider.Core.Impl;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NovelSpider.Core.Models;
using Microsoft.Extensions.Configuration;

namespace NovelSpider.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var ctx = new NovelContext())
            {
                ctx.Database.Migrate();
            }
           
            Crawl.Instance.Execute("http://www.xxbiquge.com");

            Console.WriteLine("enter any key to exit.");
            Console.Read();
        }
    }
}