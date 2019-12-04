# PageCheck
Simple Test of HttpWatch in Selenium
```
OpenQA.Selenium.WebDriverException
  HResult=0x80131500
  Message=unknown error: cannot process extension #1
from unknown error: CRX verification failed: 3
  Source=WebDriver
  StackTrace:
   at OpenQA.Selenium.Remote.RemoteWebDriver.UnpackAndThrowOnError(Response errorResponse)
   at OpenQA.Selenium.Remote.RemoteWebDriver.Execute(String driverCommandToExecute, Dictionary`2 parameters)
   at OpenQA.Selenium.Remote.RemoteWebDriver.StartSession(ICapabilities desiredCapabilities)
   at OpenQA.Selenium.Remote.RemoteWebDriver..ctor(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
   at OpenQA.Selenium.Chrome.ChromeDriver..ctor(ChromeDriverService service, ChromeOptions options, TimeSpan commandTimeout)
   at OpenQA.Selenium.Chrome.ChromeDriver..ctor(ChromeOptions options)
   at PageCheck.Program.Main(String[] args) in C:\Users\bugma\Source\Repos\PageCheck\Program.cs:line 28
```
CRX file does exist at "C:\\Program Files (x86)\\HttpWatch\\HttpWatchForChrome.crx"
