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

		private static Random random = new Random();
		[Test]
		public void RegisterNewUser()
		{
			string email = GetEmail(7);
			string password = "12345";
			OpenUserPage();
			Registration(email, password);
			LogoutUser();
			LoginUser(email, password);
			LogoutUser();
		}

		public void Registration(string email, string password)
		{

			driver.FindElement(By.LinkText("New customers click here")).Click();
			Thread.Sleep(500);
			driver.FindElement(By.CssSelector("[name=firstname]")).SendKeys("Oleg");
			Thread.Sleep(500);
			driver.FindElement(By.CssSelector("[name=lastname]")).SendKeys("Petrov");
			Thread.Sleep(500);
			driver.FindElement(By.CssSelector("[name=address1]")).SendKeys("Lenina 1");
			Thread.Sleep(500);
			driver.FindElement(By.CssSelector("[name=postcode]")).SendKeys("12345");
			Thread.Sleep(500);
			driver.FindElement(By.CssSelector("[name=city]")).SendKeys("Kazan");
			FillCombobox(By.CssSelector("[name=country_code]"), "United States");
			Thread.Sleep(500);
			driver.FindElement(By.CssSelector("[name=phone]")).SendKeys("89531234567");
			driver.FindElement(By.CssSelector("[name=email]")).SendKeys(email);
			driver.FindElement(By.CssSelector("[name=password]")).SendKeys(password);
			driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys(password);			
			driver.FindElement(By.CssSelector("[name=create_account]")).Click();
			Thread.Sleep(500);
			FillCombobox(By.CssSelector("[name=zone_code]"), "California");
			driver.FindElement(By.CssSelector("[name=password]")).SendKeys(password);
			driver.FindElement(By.CssSelector("[name=confirmed_password]")).SendKeys(password);			
			driver.FindElement(By.CssSelector("[name=create_account]")).Click();
			Thread.Sleep(500);
		}

		public void FillCombobox(By locator, string value)
		{
			IWebElement element = driver.FindElement(locator);
			var selectElement = new SelectElement(element);
			selectElement.SelectByText(value);
		}
		public string GetEmail(int length)
		{
			string emailPart = RandomString(length);
			string email = (emailPart + "@gmail.com");
			return email;
		}
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}		
		public void LogoutUser()
		{
			driver.FindElement(By.LinkText("Logout")).Click();
			Thread.Sleep(500);
		}

		public void LoginUser(string email, string password)
		{
			driver.FindElement(By.Name("email")).SendKeys(email);
			driver.FindElement(By.Name("password")).SendKeys(password);
			driver.FindElement(By.Name("login")).Click();
			Thread.Sleep(500);
		}
	}
}
