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


namespace SeleniumTests
{
	[TestFixture]
	public partial class SeleniumTests
	{

		[Test]
		public void CreateItemTest()
		{
			OpenMainPage();
			Login("admin", "admin");
			Thread.Sleep(500);
			AssertLogin();
			OpenNewItemPage();				
			FillGeneralData();

		}
		public void OpenNewItemPage()
		{
			Click(By.XPath("(//li[@id='app-']/a/span[2])[2]"));
			Click(By.LinkText("Add New Product"));
			Thread.Sleep(500);
		}
		public void FillGeneralData()
		{
			Thread.Sleep(500);
			Click(By.XPath("//input[@name='status']"));
			EnterData(By.CssSelector("[name^=name]"),"IP Test Item Name");
			EnterData(By.CssSelector("[name=code]"),"20987654");
			ScrollAndClick(By.XPath("(//input[@name='product_groups[]'])[3]"));
			EnterData(By.CssSelector("[name=quantity]"),"1");
			EnterData(By.CssSelector("[name=date_valid_from]"),"10062020");
			EnterData(By.CssSelector("[name=date_valid_to]"),"10062022");
			LoadImage(By.CssSelector("[name^=new_images]"));
			
			
		}
		protected void ScrollAndClick(By locator)
		{
			ScrollToElement(locator);
			Click(locator);
		}
		protected void ScrollToElement(By locator)
		{
			IWebElement element = driver.FindElement(locator);
			((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
			Thread.Sleep(500);
		}
		//String js = "arguments[0].style.visibility = 'visible';"; executeJavaScript(js, element); element.sendKeys(photo.getAbsolutePath());
		protected void LoadImage(By locator)
		{
			IWebElement element = driver.FindElement(locator);
			string path = Path.GetFullPath(@"..\..\SeleniumWebDrowerCourse\Cup.jpg");
			
			element.SendKeys(path);
			Thread.Sleep(500);
		}
			public void Click(By element)
		{
			driver.FindElement(element).Click();
		}	
		public void EnterData(By element, string text)
		{
			Thread.Sleep(500);
			driver.FindElement(element).Clear();
			driver.FindElement(element).SendKeys(text);
		}
	}
}
