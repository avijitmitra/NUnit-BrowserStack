using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;

namespace BrowserStack.PageObjects
{
    public class KolkataBugbashEComPageObject
    {
        private readonly IWebDriver driver;

        public KolkataBugbashEComPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl("https://kolkata.bugbash.live/");
        }

        public string GetTitle()
        {
            return driver.Title;
        }

        public IList<IWebElement> GetAddToCartButtons()
        {
            return driver.FindElements(By.XPath("//*[text()='Add to cart']"));
        }

        public void ClickAllAddToCart()
        {
            var buttons = GetAddToCartButtons();
            foreach (var button in buttons)
            {
                Thread.Sleep(2000);
                button.Click();
            }
        }

        public int GetCartItemCount()
        {
            return Convert.ToInt32(driver.FindElement(By.XPath("(//span[@class='bag__quantity'])[1]")).Text);
        }

        public void FilterByVendor(string vendor)
        {
            driver.FindElement(By.XPath($"//div[@class='filters-available-size']//span[contains(text(), '{vendor}')]")).Click();
        }

        public IList<IWebElement> GetFilteredProducts()
        {
            return driver.FindElements(By.XPath("//div[@class='shelf-item__title']"));
        }

        public IList<IWebElement> GetFavoriteIcons()
        {
            return driver.FindElements(By.XPath("//span[@class='MuiIconButton-label']"));
        }

        public void ClickFavoriteIconAt(int index)
        {
            var icons = GetFavoriteIcons();
            icons[index].Click();
        }

        public void NavigateToFavoritesTab()
        {
            driver.FindElement(By.XPath("//*[text()='Favourites']")).Click();
        }

        public int GetFavoritesCount()
        {
            var favCounter = driver.FindElement(By.XPath("//small[@class='products-found']"));
            return Convert.ToInt32(favCounter.Text.Split(" ")[0].Replace("\"", ""));
        }
    }
}
