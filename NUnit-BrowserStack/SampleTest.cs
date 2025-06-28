using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowserStack
{
    [TestFixture]
    [Category("sample-test")]
    public class SampleTest : BrowserStackNUnitTest
    {
        public SampleTest() : base() { }

        [Test]
        public void VerifyTitle()
        {
            driver.Navigate().GoToUrl("https://kolkata.bugbash.live/");
            Assert.AreEqual("StackDemo", driver.Title);


        }


        [Test]
        public void VerifyProdcutsCanBeAddedToCart()
        {
            driver.Navigate().GoToUrl("https://kolkata.bugbash.live/");
            Assert.AreEqual("StackDemo", driver.Title);
            var noOfproducts = driver.FindElementsByXPath("//*[text()='Add to cart']");
            foreach (WebElement product in noOfproducts)
            {

                Thread.Sleep(2000);
                product.Click();
            }
            int noOfAddtoCartProduct = Convert.ToInt32(driver.FindElementByXPath("//span[@class='bag__quantity'][0]").Text);
            Assert.AreEqual(noOfproducts.Count, noOfAddtoCartProduct);

        }

        [Test]
        public void VerifyVendorFilterFunctionality()
        {
            driver.Navigate().GoToUrl("https://kolkata.bugbash.live/");
            Assert.AreEqual("StackDemo", driver.Title);
            driver.FindElement(By.XPath("//div[@class='filters-available-size']//span[contains(text(), 'OnePlus')]")).Click(); 

            Thread.Sleep(2000); 
            var filteredProducts = driver.FindElements(By.XPath("//div[@class='shelf-item__title']"));
            foreach (var product in filteredProducts)
            {
                string title = product.Text.ToLower();
                Assert.IsTrue(title.Contains("oneplus"), $"Products does not match the filter 'OnePlus'");
            }
        }

        [Test]
        public void VerifyAddToFavoritesForAllProducts()
        {
            driver.Navigate().GoToUrl("https://kolkata.bugbash.live/");
            Assert.AreEqual("StackDemo", driver.Title);


            var favIcons = driver.FindElements(By.XPath("//span[@class='MuiIconButton-label']"));

            int expectedCount = 0;

            for (int i = 0; i < favIcons.Count; i++)
            {
                
                var currentFavIcons = driver.FindElements(By.XPath("//span[@class='MuiIconButton-label']"));
                currentFavIcons[i].Click();
                expectedCount++;

                Thread.Sleep(1000);

                driver.FindElement(By.XPath("//*[text()='Favourites']")); //Go to favourites tab
                
                var favCounterElement = driver.FindElement(By.XPath("//small[@class='products-found']"));
                int actualCount = Convert.ToInt32(favCounterElement.Text.Split(" ")[0].Replace("\"",""));//to get the number of favorites items
               
                // Validate the count
                Assert.AreEqual(expectedCount, actualCount, $"Count is not matching");
            }

            
        }

    }
}
