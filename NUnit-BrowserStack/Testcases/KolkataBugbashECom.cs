using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using BrowserStack.PageObjects;
using BrowserStack;

namespace SingleTest.Testcases
{
    [TestFixture]
    [Category("KolkataBugbashECom")]
    public class KolkataBugbashECom : BrowserStackNUnitTest
    {
        private KolkataBugbashEComPageObject page;

        public KolkataBugbashECom() : base() { }

        [SetUp]
        public void SetUp()
        {
            page = new KolkataBugbashEComPageObject(driver);
            page.Navigate();
            Assert.AreEqual("StackDemo", page.GetTitle());
        }

        [Test]
        [Description("Verify that the home page title is 'StackDemo' after navigation.")]
        public void VerifyTitle()
        {
            Assert.AreEqual("StackDemo", page.GetTitle());
        }

        [Test]
        [Description("Verify that all products on the page can be added to the cart and the cart count reflects the total.")]
        public void VerifyProdcutsCanBeAddedToCart()
        {
            var products = page.GetAddToCartButtons();
            page.ClickAllAddToCart();

            int cartCount = page.GetCartItemCount();
            Assert.AreEqual(products.Count, cartCount);
        }

        [Test]
        [Description("Verify that filtering by 'OnePlus' vendor displays only OnePlus products.")]
        public void VerifyVendorFilterFunctionality()
        {
            page.FilterByVendor("OnePlus");
            Thread.Sleep(2000);

            var filteredProducts = page.GetFilteredProducts();
            foreach (var product in filteredProducts)
            {
                string title = product.Text.ToLower();
                Assert.IsTrue(title.Contains("oneplus"), $"Product title '{title}' does not match the 'OnePlus' filter.");
            }
        }

        [Test]
        [Description("Verify that clicking on the favorite icon for each product adds it to the favorites tab and the count updates correctly.")]
        public void VerifyAddToFavoritesForAllProducts()
        {
            int expectedCount = 0;

            var favIcons = page.GetFavoriteIcons();

            for (int i = 0; i < favIcons.Count; i++)
            {
                page.ClickFavoriteIconAt(i);
                expectedCount++;

                Thread.Sleep(1000);
                page.NavigateToFavoritesTab();
                int actualCount = page.GetFavoritesCount();
                Assert.AreEqual(expectedCount, actualCount, "Favorites count mismatch.");
            }
        }
    }
}
