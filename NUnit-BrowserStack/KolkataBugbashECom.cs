using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;
using BrowserStack.PageObjects;

namespace BrowserStack
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
        public void VerifyTitle()
        {
            Assert.AreEqual("StackDemo", page.GetTitle());
        }

        [Test]
        public void VerifyProdcutsCanBeAddedToCart()
        {
            var products = page.GetAddToCartButtons();
            page.ClickAllAddToCart();

            int cartCount = page.GetCartItemCount();
            Assert.AreEqual(products.Count, cartCount);
        }

        [Test]
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
