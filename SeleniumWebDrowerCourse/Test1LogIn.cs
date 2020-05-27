using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace csharp_example
{
	[TestFixture]
	public class MyFirstTest
	{
		private IWebDriver driver;
		private WebDriverWait wait;

		[SetUp]
		public void start()
		{


			var o = new ChromeOptions();
			o.AddArgument("-disable-features=RendererCodeIntegrity");
			driver = new ChromeDriver(o);
			driver.Manage().Window.Maximize();
			wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
		}

		[Test]
		public void FirstTest()
		{
			driver.Url = "http://localhost/litecart/admin";
			wait.Until(ExpectedConditions.TitleIs("My Store"));
			Login("admin", "admin");
			AssertLogin();
			Logout();
		}
		public void Login(string id, string password)
		{
			driver.FindElement(By.Name("username")).SendKeys(id);
			driver.FindElement(By.Name("password")).SendKeys(password);
			driver.FindElement(By.Name("login")).Click();
			Thread.Sleep(500);
		}
		public void AssertLogin()
		{			
			Assert.IsTrue(IsElementPresent(By.CssSelector(".fa-sign-out")));
		}		
		public void Logout()
		{			
			driver.FindElement(By.CssSelector(".fa-sign-out")).Click();
		}
		public bool IsElementPresent(By by)
		{
			try
			{
				driver.FindElement(by);
				return true;
			}
			catch (NoSuchElementException e)
			{
				return false;
			}
		}
		[TearDown]
		public void stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}