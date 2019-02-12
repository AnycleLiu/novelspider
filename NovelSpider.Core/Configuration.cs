using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovelSpider.Core
{
    public static class Configuration
    {
        public static IConfigurationRoot config;

        static Configuration()
        {
            config = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", false, true)
               .Build();
        }
    }
}
