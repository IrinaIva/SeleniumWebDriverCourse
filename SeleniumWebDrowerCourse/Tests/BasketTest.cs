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
		public void BasketTest()
		{
			int defineCoutofItems = 3;
			OpenUserPage2();
			AddSeveralItemsToTheBasket(defineCoutofItems);
			OpenBasket();
			AssertCountOfItemsInTheBasket(defineCoutofItems);
			DeleteAlItemsFromTheBasket();
		}
		public List<string> GetListOfBasketItems()
		{
			List<string> itemsInBasket = new List<string>();
			int countofLines = driver.FindElements(By.CssSelector("#order_confirmation-wrapper .item")).Count-1;
						
			for (int v = 1; v < countofLines + 1; v++)
			{
				string item =  driver.FindElement(By.XPath("//div[@id='order_confirmation-wrapper']/table/tbody/tr["+ v + "+1]/td[2]")).Text;
				itemsInBasket.Add(item);
			}
			return itemsInBasket;
		}
		public void AssertCountOfItemsInTheBasket(int c)
		{
			int couunt = GetCountOfBasketItems();
			Assert.AreEqual(couunt,c);
		}
		public int GetCountOfBasketItems()
		{
			int countOfIBaskettems=0;
			int countofLines = driver.FindElements(By.CssSelector("#order_confirmation-wrapper .item")).Count - 1;
			for (int x = 1; x < countofLines + 1; x++)
			{
				int item = Convert.ToInt32(driver.FindElement(By.XPath("//div[@id='order_confirmation-wrapper']/table/tbody/tr[" + x + "+1]/td")).Text);
				countOfIBaskettems = countOfIBaskettems + item;
			}
			return countOfIBaskettems;
		}
		public void AddSeveralItemsToTheBasket(int count)
		{
			for (int c = 1; c < count + 1; c++)
			{
				OpenAnItemPage();
				AddItemToBasket();
			}
		}
		public void DeleteAlItemsFromTheBasket()
		{
			int count = GetCountOfItemLines();
			for (int c = 1; c < count + 1; c++)
			{
				DeleteItemFromBasket();
			}
		}
		public int GetCountOfItemLines()
		{
			int countofLines = driver.FindElements(By.CssSelector("#order_confirmation-wrapper .item")).Count - 1;
			return countofLines;
		}
		public void DeleteItemFromBasket()
		{
			int itemsCount = GetCountOfBasketItems();
			int countofLines = GetCountOfItemLines();
			List<string> itemsInBasket = GetListOfBasketItems();
			if (countofLines > 1) Click(By.CssSelector(".shortcut:nth-child(1) img"));			
			Thread.Sleep(500);
			string itemName = driver.FindElement(By.CssSelector(".item:nth-child(1) strong")).Text;
			int countOfDeletedItems = Convert.ToInt32(driver.FindElement(By.Name("quantity")).GetAttribute("value"));
			Click(By.Name("remove_cart_item"));
			wait.Until(d => GetCountOfBasketItems()== itemsCount - countOfDeletedItems);
			List<string> itemsInBasketAfter = GetListOfBasketItems();
			itemsInBasketAfter.Add(itemName);
			itemsInBasketAfter.Sort();
			itemsInBasket.Sort();
			CollectionAssert.AreEqual(itemsInBasketAfter, itemsInBasket);
		}
		public void OpenBasket()
		{
			Click(By.LinkText("Checkout »"));
			wait.Until(d => driver.FindElement(By.CssSelector("#box-checkout-cart")));
		}
		public void OpenAnItemPage()
		{
			OpenHomePage();
			Thread.Sleep(500);
			Click(By.CssSelector("#box-most-popular .product:nth-child(1) .image"));
			Thread.Sleep(500);
			wait.Until(d => driver.FindElement(By.CssSelector("#box-product")));			
		}
		public void AddItemToBasket()
		{
			SelectSizeIfNeeded();
			int itemsInBasketBefore = Convert.ToInt32(driver.FindElement(By.CssSelector("#cart-wrapper .quantity")).Text);
			Click(By.Name("add_cart_product"));
			wait.Until(d => Convert.ToInt32(driver.FindElement(By.CssSelector("#cart-wrapper .quantity")).Text) == itemsInBasketBefore + 1);

		}
		public void SelectSizeIfNeeded()
		{
			if (IsElementPresent(By.Name("options[Size]")) == true) FillCombobox(By.Name("options[Size]"), "Small");
		}
		public void OpenHomePage()
		{
			Click(By.CssSelector(".fa-home"));
		}
	}
}