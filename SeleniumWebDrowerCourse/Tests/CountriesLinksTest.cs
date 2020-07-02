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
			CheckLink(MainCountryPage);
		}

		public void CheckLink(string MainCountryPage)
		{
			int countOfLines = driver.FindElements(By.XPath("//td[@id='content']//form/table[1]//tr")).Count;						
			for (int q = 1; q < countOfLines + 1; q++)
			{
				if (IsElementPresent(By.XPath("(//td[@id='content']//form/table[1]//tr)["+q+ "]//*[@class='fa fa-external-link']"))==true)				
				{
					CheckWindow(By.XPath("(//td[@id='content']//table/tbody/tr)[" + q + "]//*[@class='fa fa-external-link']"), MainCountryPage);
				}
			}
		}
		public void OpenCerateCountryPage()
		{
			Click(By.LinkText("Countries"));
			Click(By.LinkText("Add New Country"));
		}
		public string ThereIsWindowOtherThan(ICollection<string> list)
		{
			ICollection<string> allWindows = driver.WindowHandles;
			ICollection<string> newWindow = allWindows.Except(list).ToList();
			return newWindow.First();
		}
		public void CheckWindow(By findLocator, string mainW)
		{
			ICollection<string> oldWindows = driver.WindowHandles;
			Click(findLocator);
			string codeWindow = wait.Until(d => ThereIsWindowOtherThan(oldWindows));
			SwitchTo(codeWindow);			
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

