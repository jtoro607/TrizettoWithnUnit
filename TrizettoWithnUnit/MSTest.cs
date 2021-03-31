using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TrizettoWithMSTest
{

    [TestClass]
    public class MSTest
    {
        IWebDriver _driver;

        [TestMethod]
        public void TestMethod1()
        {
            //Configure Chrome driver
            var options = new ChromeOptions();
            options.AddArgument("--start-Maximized");
            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            
            //Navigate to http://automationpractice.com/index.php
            _driver.Navigate().GoToUrl("http://automationpractice.com/index.php");


            //Click on sign in button
            _driver.FindElement(By.ClassName("login")).Click();

            //Enter email address to create an account
            _driver.FindElement(By.Id("email_create")).SendKeys("JonDue6@myemail.com");
            _driver.FindElement(By.Name("SubmitCreate")).Click();

            //Enter User information
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement(By.Id("customer_firstname"))));
            _driver.FindElement(By.Id("customer_firstname")).SendKeys("Jon");
            _driver.FindElement(By.Id("customer_lastname")).SendKeys("Doe");

            var validateEmail = _driver.FindElement(By.Id("email")).GetAttribute("value");
            if (!validateEmail.Contains("JonDue5@myemail.com"))
                NUnit.Framework.Assert.AreEqual("JonDue5@myemail.com", validateEmail, "Wrong email value");

            _driver.FindElement(By.Id("passwd")).SendKeys("password1");

            var webElement = _driver.FindElement(By.Id("firstname"));
            IJavaScriptExecutor js = (IJavaScriptExecutor) _driver;
            js.ExecuteScript(String.Format("window.scrollTo({0}, {1})", webElement.Location.X, webElement.Location.Y));


            var validateFirstName = _driver.FindElement(By.Id("firstname")).GetAttribute("value");
            NUnit.Framework.Assert.AreEqual("Jon", validateFirstName, "Email field did not autopopulate First Name value");

            var validateLastName = _driver.FindElement(By.Id("lastname")).GetAttribute("value");
            NUnit.Framework.Assert.AreEqual("Doe", validateLastName, "Email field did not autopopulate Last name value");

            _driver.FindElement(By.Id("address1")).SendKeys("1 main st");
            _driver.FindElement(By.Id("city")).SendKeys("Orlando");

            SelectElement State = new SelectElement(_driver.FindElement(By.Id("id_state")));
            State.SelectByText("Florida");

            _driver.FindElement(By.Id("postcode")).SendKeys("32822");

            SelectElement Country = new SelectElement(_driver.FindElement(By.Id("id_country")));
            Country.SelectByText("United States");
            _driver.FindElement(By.Id("phone_mobile")).SendKeys("4073214567");
            _driver.FindElement(By.Id("alias")).Clear();
            _driver.FindElement(By.Id("alias")).SendKeys("TestCustomer");


            //Click on Register button
            _driver.FindElement(By.Id("submitAccount")).Click();
      
            wait.Until(ExpectedConditions.ElementToBeClickable(_driver.FindElement(By.ClassName("account"))));

            var user = _driver.FindElement(By.ClassName("account")).Text;
            NUnit.Framework.Assert.AreEqual("Jon Doe", user, "User first Name or last Name is not displayed after login");
            _driver.Close();
        }
    }
}
