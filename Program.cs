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
            // Start the Chrome browser session
            var driver = new ChromeDriver(options);
            // Goto blank start page so that HttpWatch recording can be started
            driver.Navigate().GoToUrl("about:blank");
            // Set a unique title on the first tab so that HttpWatch can attach to it
            var uniqueTitle = Guid.NewGuid().ToString();
            driver.ExecuteScript("document.title = '" + uniqueTitle + "'");

            // Attach HttpWatch to the instance of Chrome created through Selenium
            Plugin plugin = control.AttachByTitle(uniqueTitle);
            driver.Navigate().GoToUrl(url);

            // Start recording now that page containing the form is loaded
            plugin.Log.EnableFilter(false);
            plugin.Clear();
            plugin.Record();

            // Put 200 in the amount field
            //driver.FindElement(By.Name("Amount")).Clear();
            //driver.FindElement(By.Name("Amount")).SendKeys("200");

            // Click on the submit button
            //driver.FindElement(By.Name("B2")).Click();

            //Console.WriteLine("\r\nClicked on submit button...");

            // Use the HttpWatch Wait call to ensure HTTP activity has ceased
            control.Wait(plugin, -1);

            // Stop recording HTTP
            plugin.Stop();

            // Read the updated account balance back from the page
            //string accountBalance = driver.FindElement(By.Id("balanceSpan")).Text;

            foreach (Page page in plugin.Log.Pages)
            {
                Console.WriteLine($"{page.Title}");
                foreach(Entry entry in page.Entries)
                {
                    Console.WriteLine($"{entry.URL}: {entry.Content.Text}");
                }
            }

            // Need to use Selenium Quit to correctly shutdown Selenium and browser
            driver.Quit();

            return 0;
        }
    }
}
