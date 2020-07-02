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
		public void CountryLinksTest()
		{			
			OpenMainPage();
			Login("admin", "admin");
			wait.Until(d => driver.FindElement(By.CssSelector(".fa-sign-out")));
			OpenCerateCountryPage();
			string MainCountryPage = driver.CurrentWindowHandle;
			
			CheckWindow(By.CssSelector("a:nth-child(4) > .fa-external-link"), MainCountryPage, "ISO 3166-1 alpha-2 - Wikipedia");
			CheckWindow(By.CssSelector("tr:nth-child(3) .fa"), MainCountryPage, "ISO 3166-1 alpha-3 - Wikipedia");
			CheckWindow(By.CssSelector("tr:nth-child(6) .fa"), MainCountryPage, "Regular expression - Wikipedia");
			CheckWindow(By.CssSelector("a:nth-child(3) > .fa-external-link"), MainCountryPage, "International Address Format Validator: Verify Mailing Formats | Informatica");
			CheckWindow(By.CssSelector("tr:nth-child(8) .fa"), MainCountryPage, "Regular expression - Wikipedia");
			CheckWindow(By.CssSelector("tr:nth-child(9) .fa"), MainCountryPage, "List of countries and capitals with currency and language - Wikipedia");
			CheckWindow(By.CssSelector("tr:nth-child(10) .fa"), MainCountryPage, "List of country calling codes - Wikipedia");
		}
		public void OpenCerateCountryPage()
		{
			Click(By.LinkText("Countries"));
			Click(By.LinkText("Add New Country"));
		}
		public string ThereIsWindowOtherThan(ICollection<string> list)
		{			
			ICollection<string> allWindows = driver.WindowHandles;
			ICollection<string> newWindow =allWindows.Except(list).ToList();			
			return newWindow.First();	
		}
		public void CheckWindow(By findLocator, string mainW, string assertTitle)
		{
			ICollection<string> oldWindows = driver.WindowHandles;
			Click(findLocator);
			string codeWindow = wait.Until(d => ThereIsWindowOtherThan(oldWindows));
			SwitchTo(codeWindow);
			wait.Until(ExpectedConditions.TitleIs(assertTitle));
			driver.Close();
			SwitchTo(mainW);
			Thread.Sleep(500);
		}
		public void SwitchTo(string id)
		{
			driver.SwitchTo().Window(id);
		}
	}
}

