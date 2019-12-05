using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Cas28
{
    class FirefoxTests
    {
        IWebDriver driver;

        [Test]
        public void TestUnos()
        {
            string txtMessage = "Ovo je neka poruka.";
            GoToWebsite("http://qa.todorowww.net/", true);
            IWebElement txtUnos = FindElement(By.Name("unos"));
            txtUnos?.SendKeys(txtMessage);
            Sleep(2);
            if (txtUnos.Text == txtMessage)
            {
                Assert.Fail("The text differs from requested.");
            } else
            {
                Assert.Pass("Entered text matches the requested one.");
            }
        }

        [Test]
        public void TestRegister()
        {
            GoToWebsite("http://qa.todorowww.net/", true);
            IWebElement lnkRegister;
            lnkRegister = FindElement(By.PartialLinkText("Registruj"));
            if (lnkRegister != null)
            {
                lnkRegister.Click();
            }
            FindElement(By.Name("ime"))?.Clear(); // Clears text element of all text
            FindElement(By.Name("ime"))?.SendKeys("Petar");
            FindElement(By.Name("prezime"))?.SendKeys("Petrovic");
            Sleep();
            string xpath = "//input[@name='korisnicko']";
            FindElement(By.XPath(xpath))?.SendKeys("PPetrovicc");

            Sleep();
            FindElement(By.Id("pol_m"))?.Click();
            FindElement(By.XPath("//input[@value='krastavac']"))?.Click();
            Sleep(5);
            /*
            // Check if radio button is checked
            if (FindElement(By.Id("pol_z"))?.Selected == true) 
            {
                Assert.Pass("Test has passed.");
            }
            // Check if checkbox is checked
            if (FindElement(By.XPath("//input[@value='krastavac']")).Selected == true)
            {
                Assert.Fail("Fail! Cucumber selected.");
            }
            Sleep(2);
            */
            IWebElement select = FindElement(By.Name("zemlja"));
            select.FindElement(By.XPath("//option[@value='mk']"))?.Click();
            Sleep(2);
        }

        [SetUp]
        public void SetUp()
        {
            driver = new FirefoxDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void Sleep(double ms = 1)
        {
            System.Threading.Thread.Sleep(Convert.ToInt32(ms * Convert.ToDouble(1000)));
        }

        public void GoToWebsite(string url, bool wait = false)
        {
            if (wait) { Sleep(); }
            driver.Navigate().GoToUrl(url);
            if (wait) { Sleep(); }
        }

        public IWebElement FindElement(By selector)
        {
            IWebElement elReturn = null;
            try
            {
                elReturn = driver.FindElement(selector);
            } catch (NoSuchElementException)
            {

            } catch (Exception e)
            {
                throw e;
            }

            return elReturn;
        }
    }
}
