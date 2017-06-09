//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading;
////
//using OpenQA.Selenium;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.IE;
////using OpenQA.Selenium.Support.UI;
//using OpenQA.Selenium.Chrome;

//namespace LoopNetReporting
//{
//    public class LoopNetDriver
//    {
//        private ChromeDriver browser = new ChromeDriver();
//        public void BeginWork()
//        {
//            try
//            {
//                ICapabilities caps = browser.Capabilities;

//                browser.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 15, 0));
//                browser.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 15, 0));

                
               
//                //browser.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 2, 0));
//                //browser.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 2, 0));
//                string uid = "julie.culver@colliers.com";
//                string password = "Paragon2015";
//                //browser = new InternetExplorerDriver();
//                browser.Navigate().GoToUrl("http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
//                browser.FindElement(By.Id("ctlLogin_LogonEmail")).SendKeys(uid);
//                browser.FindElement(By.Id("ctlLogin_LogonPassword")).SendKeys(password);
//                browser.FindElement(By.Id("ctlLogin_btnLogon")).Click();
//                //
//                //Moves to the search page for leases
//                browser.FindElement(By.ClassName("navForLease")).FindElement(By.ClassName("navPrimaryLink")).Click();
//                //Set Search types
//                //Set the Property types to Industrial, Office and Retail
//                IWebElement propertyTypeCBox = (IWebElement)browser.FindElementById("PropertyTypeCheckboxList1_ctl00");
//                //propertyTypeCBox.Clear();
//                if(propertyTypeCBox.Selected == false)
//                    propertyTypeCBox.Click();
//                //
//                propertyTypeCBox = (IWebElement)browser.FindElementById("PropertyTypeCheckboxList1_ctl01");
//                //propertyTypeCBox.Clear();
//                if (propertyTypeCBox.Selected == false)
//                    propertyTypeCBox.Click();
//                //
//                propertyTypeCBox = (IWebElement)browser.FindElementById("PropertyTypeCheckboxList1_ctl02");
//                //propertyTypeCBox.Clear();
//                if (propertyTypeCBox.Selected == false)
//                    propertyTypeCBox.Click();
//                //
//                propertyTypeCBox = (IWebElement)browser.FindElementById("PropertyTypeCheckboxList1_ctl03");
//                //propertyTypeCBox.Clear();
//                if (propertyTypeCBox.Selected)
//                    propertyTypeCBox.Click();
//                //
//                propertyTypeCBox = (IWebElement)browser.FindElementById("PropertyTypeCheckboxList1_ctl04");
//                //propertyTypeCBox.Clear();
//                if (propertyTypeCBox.Selected)
//                    propertyTypeCBox.Click();
//                //
//                //Move to advanced search (industrial, office and Retail should autoselect)
//                browser.FindElement(By.Id("SearchButton3")).Click();
//                //
//                //Need to show polygon list first.
//                browser.FindElementById("savedpolygons").Click();
//                //
//                //Select a polygon
//                ReadOnlyCollection<IWebElement> results = browser.FindElementsByClassName("PolygonShow");
//                string searchUrl = browser.Url;
//                foreach (IWebElement x in results)
//                {
//                    //if (x.GetAttribute("title") == "Garden City")
//                    string polyName = x.GetAttribute("title");
//                    x.Click();

//                    //
//                    //browser.FindElementById("chkListingID_Master").Click();
//                    //int numberOfListings = Convert.ToInt32(browser.FindElementById("spChkNbr").Text);
//                    int numberOfListings = 0;
//                    int numberOfPages = 0;
//                    bool pagesRemaining = true;
//                    //
//                    //Iterate through all results and select each page worth of them
//                    //TODO: WORKS INTERMITTENTLY. FIX THIS!!!!!
//                    while (numberOfListings < 50)
//                    {
//                        browser.FindElementById("chkListingID_Master").Click();
//                        Thread.Sleep(2000);
//                        numberOfListings = Convert.ToInt32(browser.FindElementById("spChkNbr").Text);
//                        numberOfPages++;
//                        break;
//                        //
//                        //IWebElement pagingTopDiv = (IWebElement)browser.FindElementById("pagingtop");
//                        //TODO: Add code to get all 50.
//                    }
//                    if (numberOfListings > 50)
//                    {
//                        browser.FindElementById("chkListingID_Master").Click();
//                        numberOfListings = Convert.ToInt32(browser.FindElementById("spChkNbr").Text);
//                        numberOfPages--;
//                    }
//                    else if (numberOfListings == 0)
//                        continue;
//                    //Move to create reports
//                    //
//                    browser.FindElementById("barLSReport").Click();
//                    //Continue to generatre one page listing report(s)
//                    //Thread.Sleep(2000);
//                    browser.FindElementById("btnCreateReport1").Click();
//                    //TODO: Fix this.  Data I need is inside of a frame
//                    //
//                    //IWebElement tgtFrame = browser.FindElementById("reportFrame");
//                    //Thread.Sleep(20000);
                    
//                    browser.SwitchTo().Frame(browser.FindElementByTagName("iFrame"));
//                    //bool foundElement = false;
                    
//                    IWebElement opsDiv = (IWebElement)browser.FindElement(By.Id("reportOptionContainer"));
//                    //IWebElement clearFix = (IWebElement)opsDiv.FindElementByClassName("clearfix");
//                    ReadOnlyCollection<IWebElement> opCboxRes = null;
//                    int numTries = 0;
//                    while (true)
//                    {
//                        try
//                        {
//                            opCboxRes = opsDiv.FindElements(By.TagName("input"));
//                            break;
//                        }
//                        catch(Exception ex)
//                        {
//                            Thread.Sleep(2000);
                            
//                            if (numTries > 5)
//                            {
//                                throw new Exception("Could not get reportOptionContainer div element in the time allotted", ex);
                                
//                            }
//                            else
//                                numTries++;
//                        }
//                    }
//                    List<string> tgtBoxIds = new List<string> {"LocationDescription", "ListingLinkAndId", "BrokerContactInfo"
//                            ,"PropertyDescription", "LeaseSpaces"};
//                    foreach (IWebElement opCbox in opCboxRes)
//                    {
//                        string elementID = opCbox.GetAttribute("id");
//                        if (tgtBoxIds.Contains(elementID))
//                        {
//                            if (!opCbox.Selected)
//                            {
//                                opCbox.Click();
//                            }
//                        }
//                        else
//                        {
//                            if (opCbox.Selected)
//                                opCbox.Click();
//                        }

//                    }
//                    //
//                    browser.FindElement(By.Id("BtnReportOptionDone")).Click();
//                    //
//                    //Parse this data
//                    //
//                    //TODO: Figure out how in the hell to get just the containers for the reports.
//                    //browser.FindElement(By.Id("report"))
//                    ReadOnlyCollection<IWebElement> reportDivs = browser.FindElementsByClassName("record breakAfterAlways clearfix dojoDndItem");
//                    foreach(IWebElement reportDiv in reportDivs)
//                    {
//                        Property property = ParseReport(reportDiv);
//                    }
//                    break;
//                }
//            }
//            catch (Exception ex)
//            {
//                browser.Navigate().GoToUrl(@"http://www.loopnet.com/xNet/MainSite/User/logoff.aspx?LinkCode=850");
//                throw new Exception("Error processing information", ex);
//            }
//        }

//        private Property ParseReport(IWebElement reportDiv)
//        {
        
//            //
//            //Get Address info
//            IWebElement addressDiv = reportDiv.FindElement(By.ClassName("address"));
//            ReadOnlyCollection<IWebElement> addressInfoDivs = addressDiv.FindElements(By.TagName("div"));
//            foreach(IWebElement addressElement in addressInfoDivs)
//            {
//                switch (addressElement.GetAttribute("data-name"))
//                {
//                    case "Address":
//                        property.Address = addressElement.Text;
//                        break;
//                    case "CityName":
//                        property.City = addressElement.Text;
//                        break;
//                    case "StateProvCode":
//                        property.State = addressElement.Text;
//                        break;
//                    case "PostalCode":
//                        property.ZipCode = addressElement.Text;
//                        break;
//                } 
//            }
//            //Get left stats
//            //
//            //Get left stats
//            IWebElement statsLeftDiv = reportDiv.FindElement(By.ClassName("statsLeft"));
//            ReadOnlyCollection<IWebElement> tableRows = statsLeftDiv.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
//            foreach (IWebElement statLeftRow in tableRows)
//            {
//                string labelText = null;
//                string value = null;
//                ReadOnlyCollection<IWebElement> cells = statLeftRow.FindElements(By.TagName("td"));
//                labelText = cells[0].Text;
//                switch (labelText)
//                {
//                    //case "Total Space Available":
//                    //    property.AvailableSF = cells[1].Text;
//                    //    break;
//                    case "Property Sub-type":
//                        property.PropertySubType = cells[1].Text;
//                        break;
//                    case "Building Size":
//                        property.BuildingSF = cells[1].Text;
//                        break;
//                    case "Building Class":
//                        property.BuildingClass = cells[1].Text;
//                        break;
//                }
//            }

//            return property;
//        }

//        public void Dispose()
//        {
//            if (browser != null)
//                browser.Quit();
//        }
//    }
//}
