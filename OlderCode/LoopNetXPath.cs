using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.ComponentModel;
using System.Xml.XPath;
using System.Xml;
using System.IO;
//
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
//using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
//
using LoopNetReportingClasses;

namespace LoopNetReporting
{
    public class LoopNetXPath
    {
        public string message = "Initiating search...";
        public void DoWork(string uid, string password, BackgroundWorker worker)
        {
            ChromeDriver driver = null;
            List<Property> properties = new List<Property>();
            int i = 0;
            try
            {
                
                worker.ReportProgress(0, 0);

                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().SetScriptTimeout(new TimeSpan(0, 4, 0));
                driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 4, 0));
                driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 4, 0));
                driver.Navigate().GoToUrl("http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
                //
                driver.FindElementByName("ctlLogin$LogonEmail").SendKeys(uid);
                driver.FindElementByName("ctlLogin$LogonPassword").SendKeys(password);
                driver.FindElementById("ctlLogin_btnLogon").Click();
                //
                //Move to search page directly.
                driver.Navigate().GoToUrl("http://www.loopnet.com/forlease/?AutoRedirect=Y&linkcode=20990");
                //
                //Set the search type combo boxes for our three desired markets
                SetPropertySearchTypes(driver);
                driver.FindElementById("SearchButton3").Click();
                //
                //show the polygon menu
                var polyList = (RemoteWebElement)driver.FindElementByXPath(@"//a[@id='savedpolygons']");
                //
                //Get a list of all polygons.
                var polygons =
                    driver.FindElementsByXPath(@"//ul[@id='savedpolygonsdd']/li/a[2]");
                //var polygons = driver.FindElementById('savedpolygonsdd').FindElement(By.ta
                //
                //
                for (; i < polygons.Count; i++)
                {
                    if (worker.CancellationPending)
                        throw new Exception("Operation cancelled.");
                    else
                        worker.ReportProgress((int)(((Double)i / (Double)polygons.Count) * 100.00), 0);
                    //string polygonName = MakePolygonVisible(driver, polyList, ref polygons, i);
                    string polygonName = "";
                    //driver.
                    //driver.FindElementById("savedpolygons").Location
                    driver.FindElementById("savedpolygons").Click();
                    polyList = (RemoteWebElement)driver.FindElementByXPath(@"//a[@id='savedpolygons']");
                    //
                    //Get a list of all polygons.
                    polygons = driver.FindElementsByXPath(@"//ul[@id='savedpolygonsdd']/li/a[2]");
                    polygonName = polygons[i].Text;
                    if (polygonName.Trim() == "")
                        throw new Exception("Could not retrieve name for polygon #:" + i.ToString());
                    polygons[i].Click();
                    Thread.Sleep(2000);
                    //
                    //bool firstResultsForPoly = false;
                    int numResultsProcessed = 0;
                    //
                    int numResults = 0;
                    //driver.FindElementById("FilterGo2_LBtn").Click();
                    Thread.Sleep(2000);
                    IWebElement numResElement = driver.FindElementByXPath(@"//div[@id='pagingtop']/span/span[@class='numberOfResults']");
                    numResults = Convert.ToInt32(numResElement.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    if (numResults == 500)
                        throw new Exception("Search did not filter successfully, polygon click may have failed.");
                    //Use this so we only store properties after the entire polygon has succesfully been
                    //processed
                    List<Property> tempProperties = new List<Property>();
                    while (numResults > 0 && numResultsProcessed < numResults)
                    {
                        if (worker.CancellationPending)
                            throw new Exception("Operation cancelled.");
                        //
                        //This selects all listings on the page
                        IWebElement selectAllBox = driver.FindElementById("chkListingID_Master");
                        Thread.Sleep(2000);
                        selectAllBox.Click();
                        if (selectAllBox.Selected == false)
                            selectAllBox.Click();
                        //
                        //From here we will process the results and parse relevent datqa
                        //
                        ProcessSearchPage(driver, tempProperties, polygonName);
                        //Move our browser back to search page
                        //
                        driver.FindElementById("hlBackTo").Click();
                        Thread.Sleep(2000);
                        driver.FindElementById("lnkBackTo").Click();
                        //driver.Navigate().Back();
                        //Thread.Sleep(2000);
                        //driver.Navigate().Back();
                        //deselect current results
                        //
                        Thread.Sleep(5000);
                        var numParsedElement = driver.FindElementById("spChkNbr");
                        numResultsProcessed += Convert.ToInt32(numParsedElement.Text);
                        Thread.Sleep(2000);
                        driver.FindElementById("aClearChk").Click();
                        //Get next arrow (if it exists.)
                        if (numResultsProcessed != numResults)
                        {
                            ReadOnlyCollection<IWebElement> buttons = driver.FindElementsByXPath("//*[@id='pagingbottom']/a");
                            buttons[buttons.Count - 1].Click();
                        }
                        //Report progress on work
                        worker.ReportProgress((int)((Double)i / (Double)polygons.Count * 100.00), 
                            (int)((Double)numResultsProcessed / (Double)numResults * 100.00));
                    }
                    //We only add to the properties list if all polygons for the entire
                    //property as parsed succesfully
                    properties.AddRange(tempProperties);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Parsing operation failed on polygon #: " + i.ToString()
                   + Environment.NewLine + "Number of properties parsed: " + properties.Count.ToString(), ex);
            }
            finally
            {
                if (driver != null)
                {
                    driver.Navigate().GoToUrl("http://www.loopnet.com/xNet/MainSite/User/logoff.aspx?LinkCode=850");
                    driver.Close();
                    driver.Quit();
                }
                OutputReportInformation(properties);
            }
        }

        private static void OutputReportInformation(List<Property> properties)
        {
            FileStream stream = new FileStream("report.csv", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(stream);
            //
            foreach(Property property in properties)
            {
                writer.Write(property.ToString());
            }
            //
            writer.Close();
            stream.Close();
        }
        private static string MakePolygonVisible(ChromeDriver driver, RemoteWebElement polyList, ref ReadOnlyCollection<IWebElement> polygons, int index)
        {
            polygons =
                    driver.FindElementsByXPath(@"//ul[@id='savedpolygonsdd']/li/a[2]");
            polyList = (RemoteWebElement)driver.FindElementByXPath(@"//a[@id='savedpolygons']");
            //
            DateTime start = DateTime.Now;
            while (polygons[index].Displayed == false && DateTime.Now.Subtract(start).Seconds < 20)
            {
                Actions action = new Actions(driver);
                action.SendKeys(Keys.PageDown);
                action.Perform();
                action.MoveToElement(polyList, 10, 2);
                action.Perform();
            }
            polygons[index].Click();
            return polygons[index].Text;
        }

        private static void ProcessSearchPage(ChromeDriver driver, List<Property> properties, string polygonName)
        {
            //
            //Get property names
            //
            List<string> propertyNames = new List<string>();
            ReadOnlyCollection<IWebElement> propNameElements =
                driver.FindElementsByXPath("//div/div[@id='results']/div/div/div[@class='searchResultDesc']/h2/a[1]");
            foreach (IWebElement propNameElement in propNameElements)
            {
                propertyNames.Add(propNameElement.GetAttribute("title"));
            }
            //Process search results
            //
            driver.FindElementByXPath(@"//a[@id='barLSReport']").Click();
            //Create single page detail report
            //
            string selectTemplatePageUrl = driver.Url;
            driver.FindElementByXPath(@"//a[@id='btnCreateReport1']").Click();
            //Thread.Sleep(20000);
            //
            //IWebElement frame = driver.FindElementByXPath("//iframe[1]")
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.Id("reportFrame")));
            }
            catch
            {
                driver.Navigate().Back();
                driver.FindElementByXPath(@"//a[@id='btnCreateReport1']").Click();
                //
                driver.SwitchTo().Frame(driver.FindElement(By.Id("reportFrame")));
            }
            //
            ReadOnlyCollection<IWebElement> reportOptions =
                driver.FindElementsByXPath(@"//div[@id='reportOptionContainer']/ul[@class='clearfix']/li/input");
            //
            List<string> tgtBoxIds = new List<string> {"LocationDescription", "ListingLinkAndId", "BrokerContactInfo"
                            ,"PropertyDescription", "LeaseSpaces"};
            foreach (IWebElement opCbox in reportOptions)
            {
                string elementID = opCbox.GetAttribute("id");
                if (tgtBoxIds.Contains(elementID))
                {
                    if (!opCbox.Selected)
                    {
                        opCbox.Click();
                    }
                }
                else
                {
                    if (opCbox.Selected)
                        opCbox.Click();
                }
            }
            //Update reports
            driver.FindElementByXPath(@"//button[@id='BtnReportOptionDone']").Click();
            //
            //Need to get a collection of something that contains each report
            //
            ReadOnlyCollection<IWebElement> reports = driver.FindElementsByXPath(@"//div[@data-name='ListingList']");
            //Parse reports
            //
            List<Property> tempProps = new List<Property>();
            Regex regex = new Regex(@"http:.*\s");
            for (int i = 0; i < reports.Count;i++ )
            {
                IWebElement propDiv = reports[i];
                Property property = new Property();
                //property.Url = regex.ma
                var match = regex.Match(propDiv.Text);
                property.Url = match.Success ? match.ToString().Trim() : "URL NOT FOUND";
                if (reports.Count == propertyNames.Count)
                {
                    property.PropertyName = propertyNames[i];
                }
                property.PolygonName = polygonName;
                property.SubMarket = polygonName;
                //Parse the actual report data
                //
                ParseReportData_1(propDiv, property);
                //properties.Add(property);
                tempProps.Add(property);
                //break;
            }
            driver.SwitchTo().ParentFrame();
            properties.AddRange(tempProps);
            //break;
        }
        
        private static void SetPropertySearchTypes(ChromeDriver driver)
        {
            IWebElement propertyTypeCBox = (IWebElement)driver.FindElementById("PropertyTypeCheckboxList1_ctl00");
            //propertyTypeCBox.Clear();
            if (propertyTypeCBox.Selected == false)
                propertyTypeCBox.Click();
            //
            propertyTypeCBox = (IWebElement)driver.FindElementById("PropertyTypeCheckboxList1_ctl01");
            //propertyTypeCBox.Clear();
            if (propertyTypeCBox.Selected == false)
                propertyTypeCBox.Click();
            //
            propertyTypeCBox = (IWebElement)driver.FindElementById("PropertyTypeCheckboxList1_ctl02");
            //propertyTypeCBox.Clear();
            if (propertyTypeCBox.Selected == false)
                propertyTypeCBox.Click();
            //
            propertyTypeCBox = (IWebElement)driver.FindElementById("PropertyTypeCheckboxList1_ctl03");
            //propertyTypeCBox.Clear();
            if (propertyTypeCBox.Selected)
                propertyTypeCBox.Click();
            //
            propertyTypeCBox = (IWebElement)driver.FindElementById("PropertyTypeCheckboxList1_ctl04");
            //propertyTypeCBox.Clear();
            if (propertyTypeCBox.Selected)
                propertyTypeCBox.Click();
        }
      
        private static void ParseReportData(IWebElement propDiv, Property property)
        {
            property.Address = propDiv.FindElement(By.XPath(@".//div[@data-name='StreetAddress']")).Text;
            property.City = propDiv.FindElement(By.XPath(@".//div[@data-name='CityName']")).Text;
            property.State = propDiv.FindElement(By.XPath(@".//div[@data-name='StateProvCode']")).Text;
            property.ZipCode = propDiv.FindElement(By.XPath(@".//div[@data-name='PostalCode']")).Text;

            ReadOnlyCollection<IWebElement> sideAttribLabels = propDiv.FindElements(
                By.XPath(@".//div[@class='statsLeft']/table/tbody/tr/td[@class='label']"));
            ReadOnlyCollection<IWebElement> sideAttribValues = propDiv.FindElements(
                By.XPath(@".//div[@class='statsLeft']/table/tbody/tr/td/div[1]"));

            if (sideAttribLabels.Count != sideAttribValues.Count)
                throw new Exception("Labels do not match values in length.");
            bool prop1 = false; bool prop2 = false; bool prop3 = false; bool prop4 = false;
            for (int i = 0; i < sideAttribLabels.Count; i++)
            {
                string attribLabel = sideAttribLabels[i].Text.Trim();
                if (attribLabel == "Building Size")
                {
                    property.BuildingSF = sideAttribValues[i].Text;
                    prop1 = true;
                    if (GatheredInfo(prop1, prop2, prop3, prop4))
                        break;
                }
                else if (attribLabel == "Property Type")
                {
                    property.PropertyType = sideAttribValues[i].Text;
                    prop2 = true;
                    if (GatheredInfo(prop1, prop2, prop3, prop4))
                        break;
                }
                else if (attribLabel == "Building Class")
                {
                    property.BuildingClass = sideAttribValues[i].Text;
                    prop3 = true;
                    if (GatheredInfo(prop1, prop2, prop3, prop4))
                        break;
                }
                else if (attribLabel == "Property Sub-type")
                {
                    property.PropertySubType = sideAttribValues[i].Text;
                    prop4 = true;
                    if (GatheredInfo(prop1, prop2, prop3, prop4))
                        break;
                }
            }
            //Get loopnet id
            //
            property.LoopnetID = propDiv.FindElements(
                By.XPath(@".//div[@class='statsLeft']/table/tbody/tr[@class='listingLinkAndId']/td"))[1].Text;
            //Get row/availability/lease info
            //
            ReadOnlyCollection<IWebElement> spaceRows =
                propDiv.FindElements(By.XPath(@".//div[@class='recordDetail']/div[@data-name='Lease']/table/tbody/tr"));
            //Parse rows
            //
            foreach (IWebElement row in spaceRows)
            {
               LoopNetReportingClasses.Lease lease = new LoopNetReportingClasses.Lease();
                ReadOnlyCollection<IWebElement> fields = row.FindElements(
                    By.XPath(".//td"));
                lease.Suite = fields[0].Text;
                lease.AvailableSF = fields[1].Text;
                lease.AskingRate = fields[2].Text;
                lease.AskingRateType = fields[5].Text;
                lease.Description = fields[7].Text;
                property.leases.Add(lease);

                //
            }
            //Get property description (unknown if needed but get it anyway)
            //
            //IWebElement propDescDiv = propDiv.FindElement(By.XPath(@".//div/div[@data-name='PropertyDescription']/p[@data-name='../PropertyDescription']"));
            //property.Description = propDescDiv.Text;
            //
            //Get broker info
            //property.ListingBroker = propDiv.FindElement(By.XPath(@".//div/div/div/div/p[@class='sectionDetails']")).Text;
            //property.ListingBroker = property.ListingBroker.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }
        private static void ParseReportData_1(IWebElement propDiv, Property property)
        {
            string partParth = "./h3/div[@class='address']";
            property.Address = propDiv.FindElement(By.XPath(partParth + "/div[@data-name='StreetAddress']")).Text;
            property.City = propDiv.FindElement(By.XPath(partParth + "/div[@data-name='CityName']")).Text;
            property.State = propDiv.FindElement(By.XPath(partParth + "/div[@data-name='StateProvCode']")).Text;
            property.ZipCode = propDiv.FindElement(By.XPath(partParth + "/div[@data-name='PostalCode']")).Text;

            ReadOnlyCollection<IWebElement> sideAttribLabels = propDiv.FindElements(
                By.XPath(@"./div/div/div[@class='statsLeft']/table/tbody/tr/td[@class='label']"));
            ReadOnlyCollection<IWebElement> sideAttribValues = propDiv.FindElements(
                By.XPath(@"./div/div/div[@class='statsLeft']/table/tbody/tr/td/div[1]"));

            if (sideAttribLabels.Count != sideAttribValues.Count)
                throw new Exception("Labels do not match values in length.");
            bool prop1 = false; bool prop2 = false; bool prop3 = false; bool prop4 = false;
            for (int i = 0; i < sideAttribLabels.Count; i++)
            {
                string attribLabel = sideAttribLabels[i].Text.Trim();
                if (attribLabel == "Building Size")
                {
                    property.BuildingSF = sideAttribValues[i].Text;
                    prop1 = true;
                    
                }
                else if (attribLabel == "Property Type")
                {
                    property.PropertyType = sideAttribValues[i].Text;
                    prop2 = true;
                    
                }
                else if (attribLabel == "Building Class")
                {
                    property.BuildingClass = sideAttribValues[i].Text;
                    prop3 = true;
                    
                }
                else if (attribLabel == "Property Sub-type")
                {
                    property.PropertySubType = sideAttribValues[i].Text;
                    prop4 = true;
                }
                if (GatheredInfo(prop1, prop2, prop3, prop4))
                    break;
            }
            //Get loopnet id
            //
            property.LoopnetID = propDiv.FindElement(
                By.XPath(@"./div/div/div[@class='statsLeft']/table/tbody/tr[@class='listingLinkAndId']/td[2]")).Text;
            //Get row/availability/lease info
            //
            ReadOnlyCollection<IWebElement> spaceRows =
                propDiv.FindElements(By.XPath(@"./div[@class='recordDetail']/div[@data-name='Lease']/table/tbody/tr"));
            //Parse rows
            //
            foreach (IWebElement row in spaceRows)
            {
                LoopNetReportingClasses.Lease lease = new LoopNetReportingClasses.Lease();
                //ReadOnlyCollection<IWebElement> fields = row.FindElements(
                    //By.XPath("./td"));
                ReadOnlyCollection<IWebElement> fields = 
                    row.FindElements(By.TagName("td"));
                lease.Suite = fields[0].Text;
                lease.AvailableSF = fields[1].Text;
                lease.AskingRate = fields[2].Text;
                lease.AskingRateType = fields[5].Text;
                lease.Description = fields[7].Text;
                property.leases.Add(lease);

                //
            }
            //Get property description (unknown if needed but get it anyway)
            //
            //IWebElement propDescDiv = propDiv.FindElement(By.XPath(@".//div/div[@data-name='PropertyDescription']/p[@data-name='../PropertyDescription']"));
            //property.Description = propDescDiv.Text;
            //
            //Get broker info
            property.ListingBroker = propDiv.FindElement(By.XPath(@"./div/div/div/div/p[@class='sectionDetails']")).Text;
            property.ListingBroker = property.ListingBroker.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }
        private static bool GatheredInfo(params bool[] checks)
        {
            foreach(bool check in checks)
            {
                if(!check)
                { return false; }
            }
            return true;
        }
    }
}
