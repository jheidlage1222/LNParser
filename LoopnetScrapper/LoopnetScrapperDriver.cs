using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace LoopnetScrapper
{
    class LoopnetScrapperDriver
    {
        public void CompileTestRun(string uid, string pwd)
        {
            var webDriver = new ChromeDriver(@"C:\Users\Jake\Documents\Selenium");
            try
            {
                webDriver.Navigate().GoToUrl(@"http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
                webDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 1, 1));
                webDriver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 1, 1));
                webDriver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 1, 1));
                //
                var loginElement = webDriver.FindElementById("ctlLogin_LogonEmail");
                loginElement.SendKeys("julie.culver@colliers.com");
                var pwdElement = webDriver.FindElementById("ctlLogin_LogonPassword");
                pwdElement.SendKeys("tessarae");
                webDriver.FindElementById("ctlLogin_btnLogon").Click();
                webDriver.FindElementByPartialLinkText("For Lease").Click();
                //
                try
                {
                    SetPropertyTypeCheckBoxes(webDriver);
                }
                catch (Exception ex)
                {
                    //Todo: Figure out how to make sure this works.  
                }
               

                //
            }
            catch (Exception ex)
            {
                
            }
            webDriver.Navigate().GoToUrl(@"http://www.loopnet.com/xNet/MainSite/User/logoff.aspx?LinkCode=850");
            webDriver.Close();
            webDriver.Dispose();
            webDriver = null;
        }

        private void SetPropertyTypeCheckBoxes(ChromeDriver webDriver)
        {
            var industrialCb = webDriver.FindElementById("PropertyTypeCheckboxList1_ctl00");
            var officeCb = webDriver.FindElementById("PropertyTypeCheckboxList1_ctl01");
            var retailCb = webDriver.FindElementById("PropertyTypeCheckboxList1_ctl02");
            var landCb = webDriver.FindElementById("PropertyTypeCheckboxList1_ctl03");
            var specialPurpCb = webDriver.FindElementById("PropertyTypeCheckboxList1_ctl04");
            //
            SetCheckBox(industrialCb, true);
            SetCheckBox(officeCb, true);
            SetCheckBox(retailCb, true);
            SetCheckBox(landCb, false);
            SetCheckBox(specialPurpCb, false);
        }

        private void SetCheckBox(IWebElement checkBox, bool shouldSelect)
        {
                if (shouldSelect && !checkBox.Selected)
                    checkBox.Click();
        }
    }
}
