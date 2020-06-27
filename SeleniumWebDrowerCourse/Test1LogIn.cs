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
		public void ClickAllMenus()
		{
			int count = driver.FindElements(By.CssSelector("#app- .name")).Count;
			for (int i = 1; i < count + 1; i++)
			{				
				driver.FindElement(By.XPath("(//li[@id='app-']/a/span[2])[" + i + "]")).Click();
				IsElementPresent(By.CssSelector(".fa - stack icon - wrapper"));
				int count2 = driver.FindElements(By.CssSelector("[id^='doc-']")).Count;
				if (count2 > 0)
				{
					for (int j = 1; j < count2 + 1; j++)
					{
						driver.FindElement(By.XPath("//li[starts-with(@id,'doc-')]["+j+"]")).Click();
						IsElementPresent(By.CssSelector(".fa - stack icon - wrapper"));
					}
				}			
			}
		}

		public void OpenMainPage()
		{
			driver.Url = "http://localhost/litecart/admin";
			wait.Until(ExpectedConditions.TitleIs("My Store"));
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