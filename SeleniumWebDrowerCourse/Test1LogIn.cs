using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support;
using System.Windows.Input;
using System.IO;
using System.Reflection;


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
		public void LogInTest()
		{
			OpenMainPage();
			Login("admin", "admin");
			wait.Until(ExpectedConditions.TitleIs("My Store"));
			AssertLogin();
			Logout();
		}
		[Test]
		public void ClickToAllSections()
		{
			OpenMainPage();
			Login("admin", "admin");
			wait.Until(ExpectedConditions.TitleIs("My Store"));
			AssertLogin();			
			ClickAllMenus();	

		}
		[Test]
		public void CheckStickersTest()
		{
			OpenUserPage();
			Thread.Sleep(500);
			driver.FindElement(By.LinkText("Rubber Ducks")).Click();
			CheckStickers();
		}
		public void CheckStickers()
		{
			int count = driver.FindElements(By.CssSelector(".product")).Count;
			List<string> asserts = new List<string>();
			
			for (int i = 1; i < count + 1; i++)
			{
				int stickerCount = driver.FindElements(By.XPath("(//li[starts-with(@class,'product')])[" + i + "]//*[starts-with(@class,'sticker')]")).Count;				
				//bool assert = IsElementPresent(By.XPath("(//li[starts-with(@class,'product')])[" + i + "]//*[starts-with(@class,'sticker')]"));
				if (stickerCount !=1) asserts.Add(Convert.ToString(i));
			}
			if (asserts.Count>1 ) Assert.Fail("Not all the items have one sticker. Count of failed items - " + asserts.Count);

		}
		public void ClickAllMenus()
		{
			int count = driver.FindElements(By.CssSelector("#app- .name")).Count;
			List<string> asserts = new List<string>();
			for (int i = 1; i < count + 1; i++)
			{				
				driver.FindElement(By.XPath("(//li[@id='app-']/a/span[2])[" + i + "]")).Click();
				//	Assert.IsTrue(IsElementPresent(By.XPath("//*[@id='content']//[starts-with(@class,'fa-stack')]")));
				bool assert = IsElementPresent(By.CssSelector("h1"));
				if (assert == false) asserts.Add(Convert.ToString(i));
				int count2 = driver.FindElements(By.CssSelector("[id^='doc-']")).Count;
				if (count2 > 0)
				{
					for (int j = 1; j < count2 + 1; j++)
					{
						driver.FindElement(By.XPath("//li[starts-with(@id,'doc-')]["+j+"]")).Click();
						bool assert2 = IsElementPresent(By.CssSelector("h1"));
						if (assert == false) asserts.Add(Convert.ToString(j));
					}
				}			
			}
			if (asserts.Count > 0) Assert.Fail("Title is missing. Count of failed pages - " + asserts.Count);
		}

		public void OpenMainPage()
		{
			driver.Url = "http://localhost/litecart/admin";
			wait.Until(ExpectedConditions.TitleIs("My Store"));
		}
		public void OpenUserPage()
		{
			driver.Url = "http://localhost/litecart/";
			wait.Until(ExpectedConditions.TitleIs("Online Store | My Store"));
		}
		public void Login(string id, string password)
		{
			driver.FindElement(By.Name("username")).SendKeys(id);
			driver.FindElement(By.Name("password")).SendKeys(password);
			driver.FindElement(By.Name("login")).Click();
			if (IsElementPresent(By.CssSelector(".fa-sign-out")) == false)
				driver.FindElement(By.Name("login")).Click();
			Thread.Sleep(500);
		}
		public void AssertLogin()
		{
			Assert.IsFalse(IsElementPresent(By.CssSelector(".fa-sign-out[")));
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
			//catch (InvalidSelectorException ex)
			//{
			//	throw ex;
			//}
			catch (NoSuchElementException ex)
			{
				return false;
			}
			//	return driver.FindElements(by).Count > 0;
		}
		[TearDown]
		public void stop()
		{
			driver.Quit();
			driver = null;
		}
	}
}