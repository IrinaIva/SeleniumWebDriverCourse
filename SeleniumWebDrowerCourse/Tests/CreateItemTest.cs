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
			string name = "IP Test Item Name";
			OpenMainPage();
			Login("admin", "admin");
			Thread.Sleep(500);
			AssertLogin();
			OpenNewItemPage();
			FillGeneralData(name);
			FillInformationData();
			FillPricesData();
			Save();
			AsserItemCreation(name);
		}

		public void OpenNewItemPage()
		{
			Click(By.XPath("(//li[@id='app-']/a/span[2])[2]"));
			Click(By.LinkText("Add New Product"));
			Thread.Sleep(500);
		}
		public void AsserItemCreation(string name)
		{
			Click(By.LinkText("Catalog"));
			Assert.IsTrue(IsElementPresent(By.LinkText(name)));
		}
		public void Save()
		{
			Click(By.CssSelector("[name=save]"));
			Thread.Sleep(500);
		}
			public void FillInformationData()
		{
			Thread.Sleep(500);
			Click(By.LinkText("Information"));
			Thread.Sleep(500);
			FillCombobox(By.CssSelector("[name=manufacturer_id]"), "ACME Corp.");
			EnterData(By.CssSelector("[name=keywords]"), "item");
			EnterData(By.CssSelector("[name^=short_description]"), "IPo");
			EnterData(By.CssSelector("[name^=description]"), "description");
			EnterData(By.CssSelector("[name^=head_title]"), "head title");
			EnterData(By.CssSelector("[name^=meta_description]"), "meta description");

		}
		public void FillPricesData()
		{
			Click(By.LinkText("Prices"));
			Thread.Sleep(500);
			EnterData(By.CssSelector("[name=purchase_price]"), "1.15");
			FillCombobox(By.CssSelector("[name=purchase_price_currency_code]"), "Euros");
			EnterData(By.Name("prices[USD]"),"51.15");			
			EnterData(By.Name("prices[EUR]"), "1.10");
			
		}
		public static string RandomNumbString(int length)
		{
			const string chars = "0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}
		public void FillGeneralData(string name)
		{
			Thread.Sleep(500);
			Click(By.XPath("//input[@name='status']"));
			EnterData(By.CssSelector("[name^=name]"),name);
			EnterData(By.CssSelector("[name=code]"), RandomNumbString(10));
			ScrollAndClick(By.XPath("(//input[@name='product_groups[]'])[3]"));
			EnterData(By.CssSelector("[name=quantity]"), "1");
			EnterData(By.CssSelector("[name=date_valid_from]"), "10062020");
			EnterData(By.CssSelector("[name=date_valid_to]"), "10062022");
			LoadImage(By.CssSelector("[name^=new_images]"));
			Thread.Sleep(500);

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
