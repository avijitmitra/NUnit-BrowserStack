using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace BrowserStack
{
  [TestFixture]
  public class BrowserStackNUnitTest
  {
    protected RemoteWebDriver driver;

    public BrowserStackNUnitTest()
    {
    }

    [SetUp]
    public void Init()
    {
      DriverOptions capability = new OpenQA.Selenium.Chrome.ChromeOptions();
      capability.BrowserVersion = "latest";


      driver = new RemoteWebDriver(
        new Uri("http://localhost:4444/wd/hub/"),
        capability
      );
    }


        public void MarkTestStatus(string status, string reason)
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", " +
                                  "\"arguments\": {\"status\": \"" + status + "\", \"reason\": \"" + reason + "\"}}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while setting BrowserStack session status: " + ex.Message);
            }
        }

        [TearDown]
    public void Cleanup()
    {
            driver.Quit();

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                MarkTestStatus("passed", "Test passed successfully.");
            }
            else
            {
                MarkTestStatus("failed", $"Test failed: {message}");
            }
        }
  }
}
