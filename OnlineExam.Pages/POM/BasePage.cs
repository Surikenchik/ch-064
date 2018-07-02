﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineExam.Framework;
using OpenQA.Selenium.Support.UI;

namespace OnlineExam.Pages.POM
{
  public abstract  class BasePage
    {
        protected IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public BasePage()
        {

        }

        public void SetDriver(IWebDriver driver)
        {
            this.driver = driver;
        }

        public T ConstructPage<T>() where T : BasePage, new()
        {
            var page = new T();
            page.SetDriver(driver);

            try
            {
                PageFactory.InitElements(driver, page);
                return page;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static TimeSpan WAIT_TIME = new TimeSpan(0, 0, 100);


        public  void WaitWhileNotClickableWebElement(IWebElement webElement)
        {
            WebDriverWait wait = new WebDriverWait(driver, WAIT_TIME);
            wait.Until(ExpectedConditions.ElementToBeClickable(webElement));
        }

        public string GetText(string elementType, string element)
        {
            if (elementType == "Id")
            {
                return driver.FindElement(By.Id(element)).Text;
            }
            else if (elementType == "CssSelector")
            {
                return driver.FindElement(By.CssSelector(element)).Text;
            }
            else return String.Empty;
        }
    }
}
