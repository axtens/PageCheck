using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWatch;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PageCheck
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("needs url");
                return 1;
            }

            var url = args[0];
            Controller control = new Controller();
            var options = new ChromeOptions();
            options.AddExtension(control.Chrome.HttpWatchCRXFile);
            
            var driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("about:blank");
            var uniqueTitle = Guid.NewGuid().ToString();
            driver.ExecuteScript("document.title = '" + uniqueTitle + "'");

            Plugin plugin = control.AttachByTitle(uniqueTitle);
            driver.Navigate().GoToUrl(url);

            plugin.Log.EnableFilter(false);
            plugin.Clear();
            plugin.Record();

            control.Wait(plugin, -1);

            plugin.Stop();

            foreach (Page page in plugin.Log.Pages)
            {
                Console.WriteLine($"{page.Title}");
                foreach(Entry entry in page.Entries)
                {
                    Console.WriteLine($"{entry.URL}: {entry.Content.Text}");
                }
            }

            driver.Quit();

            return 0;
        }
    }
}
