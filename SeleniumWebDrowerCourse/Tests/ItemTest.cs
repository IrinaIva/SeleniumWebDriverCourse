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
		public void OpenItemTest()
		{
			OpenUserPage();
			Assert.IsTrue(IsElementPresent(By.CssSelector("#box-campaigns .product .regular-price")));
			Assert.IsTrue(IsElementPresent(By.CssSelector("#box-campaigns .product .campaign-price")));
			string itemName = driver.FindElement(By.CssSelector("#box-campaigns .product .name")).Text;
			string regularPrice = driver.FindElement(By.CssSelector("#box-campaigns .product .regular-price")).Text;
			string regularPriceColor = driver.FindElement(By.CssSelector("#box-campaigns .product .regular-price")).GetCssValue("color");
			string regularPriceLine = driver.FindElement(By.CssSelector("#box-campaigns .product .regular-price")).GetCssValue("text-decoration-line");
			string regularPriceFontSize = driver.FindElement(By.CssSelector("#box-campaigns .product .regular-price")).GetCssValue("font-size");
			int regularPriceFontWeight = Convert.ToInt32(driver.FindElement(By.CssSelector("#box-campaigns .product .regular-price")).GetCssValue("font-weight"));
			string campaignPrice = driver.FindElement(By.CssSelector("#box-campaigns .product .campaign-price")).Text;
			string campaignPriceColor = driver.FindElement(By.CssSelector("#box-campaigns .product .campaign-price")).GetCssValue("color");
			int campaignPriceFontWeight = Convert.ToInt32(driver.FindElement(By.CssSelector("#box-campaigns .product .campaign-price")).GetCssValue("font-weight"));
			string campaignPriceFontSize = driver.FindElement(By.CssSelector("#box-campaigns .product .campaign-price")).GetCssValue("font-size");
			string campaignPriceFontLine = driver.FindElement(By.CssSelector("#box-campaigns .product .campaign-price")).GetCssValue("text-decoration-line");

			//return (text.Substring(text.IndexOf("'") + 1, (text.LastIndexOf("'") - text.IndexOf("'") - 1)));
			string r11 = regularPriceColor.Substring(regularPriceColor.IndexOf("(") + 1, 3);
			string g11 = regularPriceColor.Substring(regularPriceColor.IndexOf("(") + 6, 3);
			string b11 = regularPriceColor.Substring(regularPriceColor.IndexOf("(") + 11, 3);
			Assert.AreEqual(r11,g11);
			Assert.AreEqual(g11,b11);
			//"rgba(119, 119, 119, 1)");

			int r12 = Convert.ToInt32(campaignPriceColor.Substring(campaignPriceColor.IndexOf("(") + 1, 3));
			int g12 = Convert.ToInt32(campaignPriceColor.Substring(campaignPriceColor.IndexOf("(") + 6, 1));
			int b12 = Convert.ToInt32(campaignPriceColor.Substring(campaignPriceColor.IndexOf("(") + 9, 1));
			Assert.AreEqual(g12, 0);
			Assert.AreEqual(b12, 0);
			Assert.IsTrue(r12>0);
			//"rgba(204, 0, 0, 1)"
			int regularPriceInt = Convert.ToInt32(regularPrice.Substring(1));
			int campaignPriceInt = Convert.ToInt32(campaignPrice.Substring(1));
			double regularPriceFontSizeInt = Convert.ToDouble(regularPriceFontSize.Substring(0, regularPriceFontSize.Length-2));
			double campaignPriceFontSizeInt = Convert.ToDouble(campaignPriceFontSize.Substring(0, campaignPriceFontSize.Length - 2));

			Assert.AreEqual(regularPriceLine, "line-through");
			Assert.AreEqual(campaignPriceFontLine, "none");
			Assert.AreEqual(regularPriceFontWeight, 400);
			Assert.IsTrue(campaignPriceFontWeight>=700);
			Assert.IsTrue(regularPriceFontSizeInt < campaignPriceFontSizeInt);
			Assert.IsTrue(campaignPriceInt < regularPriceInt);

			driver.FindElement(By.CssSelector("#box-campaigns .product .campaign-price")).Click();

			Assert.IsTrue(IsElementPresent(By.CssSelector("#box-product .content .regular-price")));
			Assert.IsTrue(IsElementPresent(By.CssSelector("#box-product .content .campaign-price")));
			string itemName2 = driver.FindElement(By.CssSelector("#box-product .title")).Text;
			string regularPrice2 = driver.FindElement(By.CssSelector("#box-product .content .regular-price")).Text;
			string regularPriceColor2 = driver.FindElement(By.CssSelector("#box-product .content .regular-price")).GetCssValue("color");
			string regularPriceLine2 = driver.FindElement(By.CssSelector("#box-product .content .regular-price")).GetCssValue("text-decoration-line");
			string regularPriceFontSize2 = driver.FindElement(By.CssSelector("#box-product .content .regular-price")).GetCssValue("font-size");
			int regularPriceFontWeight2 = Convert.ToInt32(driver.FindElement(By.CssSelector("#box-product .content .regular-price")).GetCssValue("font-weight"));
			string campaignPrice2 = driver.FindElement(By.CssSelector("#box-product .content .campaign-price")).Text;
			string campaignPriceColor2 = driver.FindElement(By.CssSelector("#box-product .content .campaign-price")).GetCssValue("color");
			int campaignPriceFontWeight2 = Convert.ToInt32(driver.FindElement(By.CssSelector("#box-product .content .campaign-price")).GetCssValue("font-weight"));
			string campaignPriceFontSize2 = driver.FindElement(By.CssSelector("#box-product .content .campaign-price")).GetCssValue("font-size");
			string campaignPriceFontLine2 = driver.FindElement(By.CssSelector("#box-product .content .campaign-price")).GetCssValue("text-decoration-line");

			
			string r21 = regularPriceColor2.Substring(regularPriceColor2.IndexOf("(") + 1, 3);
			string g21 = regularPriceColor2.Substring(regularPriceColor2.IndexOf("(") + 6, 3);
			string b21 = regularPriceColor2.Substring(regularPriceColor2.IndexOf("(") + 11, 3);
			Assert.AreEqual(r21, g21);
			Assert.AreEqual(g21, b21);
			//rgba(119, 119, 119, 1)
			
			int r22 = Convert.ToInt32(campaignPriceColor2.Substring(campaignPriceColor2.IndexOf("(") + 1, 3));
			int g22 = Convert.ToInt32(campaignPriceColor2.Substring(campaignPriceColor2.IndexOf("(") + 6, 1));
			int b22 = Convert.ToInt32(campaignPriceColor2.Substring(campaignPriceColor2.IndexOf("(") + 9, 1));
			Assert.AreEqual(g22, 0);
			Assert.AreEqual(b22, 0);
			Assert.IsTrue(r22 > 0);
			//"rgba(204, 0, 0, 1)"
			
			int regularPriceInt2 = Convert.ToInt32(regularPrice2.Substring(1));
			int campaignPriceInt2 = Convert.ToInt32(campaignPrice2.Substring(1));
			double regularPriceFontSizeInt2 = Convert.ToDouble(regularPriceFontSize2.Substring(0, regularPriceFontSize2.Length - 2));
			double campaignPriceFontSizeInt2 = Convert.ToDouble(campaignPriceFontSize2.Substring(0, campaignPriceFontSize2.Length - 2));
			Assert.AreEqual(itemName,itemName2);
			Assert.AreEqual(regularPrice2, regularPrice);
			Assert.AreEqual(regularPriceLine2, regularPriceLine);			
			Assert.AreEqual(regularPriceFontWeight2, regularPriceFontWeight);
			Assert.AreEqual(campaignPrice2, campaignPrice);			
			Assert.IsTrue(campaignPriceFontSizeInt2 > regularPriceFontSizeInt2);
			Assert.IsTrue(campaignPriceFontWeight2 >= 700);
			Assert.AreEqual(campaignPriceFontLine2, campaignPriceFontLine);
		}


	}
}