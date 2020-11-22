using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemberManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MemberManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: false)
          .Build();
            AppSettings appSettings = new AppSettings();
            config.GetSection("AppSettings").Bind(appSettings);
            CreateHostBuilder(args, appSettings.EnvironmentName).Build().Run();
            //Build()當前置準備都設定完成後，就可以呼叫此方法實例化 Host，同時也會實例化 WebHost。
            //Run()啟動 Host。
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string environmentName) =>
       //CreateDefaultBuilder()預設使用  Kestrel 做為 WebServer 建立WebHost
       Host.CreateDefaultBuilder(args)
           .UseEnvironment(environmentName)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();//設定 WebHostBuilder 產生的 WebHost 時，所要執行的類別。
                });
    }
}
