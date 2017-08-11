using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using OpenQA.Selenium;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
//using Selenium;
using OpenQA.Selenium.Chrome;
using interactions = OpenQA.Selenium.Interactions;
//using OpenQA.selenium.interactions.Actions;
using OpenQA.Selenium.Support;
using System.Collections;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using LoopNetReportingClasses;
using Newtonsoft.Json.Converters;
using HtmlAgilityPack;
using ListingFullJSON;

namespace LoopNetReporting
{
    class LoopnetDriverAlpha
    {
        #region Global Variables
        ChromeDriver driver = null;
        private TimeSpan awaitDisplay = new TimeSpan(0, 0, 30);
        string hostName = @"http://www.loopnet.com/";
        List<Polygon> polygons = new List<Polygon>();
        private const bool _doLogout = true;
        //private static List<string> errors = new List<string>();
        //private static List<string> info = new List<string>();
        #endregion
        //
        public string BeginWork(string uid, string pwd)
        {
            #region Strings And XPaths
            //todo: HOLY DOG BALLS THIS NEEDS TO BE ANOTHER CLASS.
            string propertiesOnPageXPath = "/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/div//article[@data-id]";
            string propertiesOnPageXPathA = "/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/div/article";
            //string propertiesOnPageXPath = "";
            var resultPageLinkElementsXPath = "/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/ol";//li[position() < last()]/a";
            //string resultPageLinkElementsXPath = "id('placardSec')/div[2]/ol/li/a[text() != '']";
            //string resultPageLinkElements2XPath = "/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/ol";
            //string selectSummaryCutDownXPath = "//*[@id=/"flListingSummary/"]";
            string selectSummaryReportXPath = @"/html/body/form/div[4]/div[2]/div[1]/ul[2]/li[3]/label/input";
            string selectAllCheckBoxXPath = @"/html/body/section/main/section[1]/div/section[1]/div/div/div[2]/div[1]/div[1]/div/ul/li[1]/label/input";
            string generateReportsBtnXPath = "id('placardSec')/div[1]/ul[2]/li[1]/button";
            string loginUrl = "http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=31824";
            //string loginUrl2 = @"javascript:void(0);";
            string logoutUrl = @"http://www.loopnet.com/xNet/MainSite/User/logoff.aspx";
            //string savedSearchesUrl = @"http://www.loopnet.com/xNet/MainSite/Listing/SavedSearches/MySavedSearches_FSFL.aspx";
            string savedSearchesXPath = "/html/body/form/div[4]/div[1]/ul/li[4]/a";
            string savedSearches2XPath = "id('TabNav_liSavedSearches')/a";
            string polygonUrlsXPath = @"/html/body/form/div[5]/div/div/table/tbody//tr/td[1]/div/a[1]";
            string polygonUrlsXPathA = "id('form1')/div[5]/div/div/table/tbody/tr/td[1]/div/a[1]";
            string polygonNamesXPath = @"/html/body/form/div[5]/div/div/table/tbody//tr/td[2]";
            string polygonNamesXPathA = "id('form1')/div[5]/div/div/table/tbody/tr/td[2]";
            //string reportBtnXPath = "/html/body/section/main/section[1]/div/section[1]/div/div/div[2]/div[4]/div/button";
            string createReportsBtnXPath = "/html/body/section/main/section[1]/div/section[1]/div/div/div[2]/div[4]/div/button";
            string createReportsBtnXPathA = "/html/body/section/main/section[1]/div/section[1]/div/div/div[2]/div[4]/div/button";
            //string selectAllBtnXPath = @"/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[1]/ul[1]/li[2]/button";
            //Strin to pull the total number of properties for each poly from that map overlay
            //string totListingsPerPolyXPath = @"/html/body/section/main/section/div/section[2]/section[1]/div[1]/div[3]/div/div[2]/div[1]/span";
            //string totListingsPerPolyXPath = "/html/body/section/main/section[1]/div/section[2]/div/div/div/section[2]/section/section[1]/div/div[3]/div/div[2]/div[1]/span";
            string getTotalListingsFromMapXPath = @"/html/body/section/main/section[1]/div/section[2]/div/div/div/section[2]/section/section[1]/div/div[3]/div/div[2]/div[1]/span";
            //string matchingStringLengthForCheck = @"/html/body/section/main/section[1]/div/section[2]/div/div/div/section[2]/section/section[1]/div/div[3]/div/div[2]/div[1]/span";
            //string aboveisfullofshit = @" / html/body/section/main/section[1]/div/section[2]/div/div/div/section[2]/section/section[1]/div/div[3]/div/div[2]/div[1]";
            string addtlResultPageLinkXPath = @" / html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/ol/li/a";
            //string getCurrentResultsXPath = @"/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/div/article";
            //string selectAllPropsXPath = @"/html/body/section/main/section/div/section[2]/section[2]/div[1]/ul[1]/li[2]/button";
            //string selectAllPropsOnPageXPath = @"/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[1]/ul[1]/li[2]/button";
            string selectAllPropertiesOnPageXPath = "id('placardSec')/div[1]/ul[1]/li[2]/button";
            string clearAllPropertiesOnPageXPath = " / html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[1]/ul[1]/li[3]/button";
            string nextPageLinkXPath = "id('placardSec')/div[2]/ol/li/a[@class='caret-right-large']";
            string previousPageLinkXPath = "id('placardSec')/div[2]/ol/li[1]/a";
            string splashPageUrl = "http://www.loopnet.com/xNet/MainSite/User/home/myloopnet.aspx#";
            string moveToReportTypesXPath = @"/html/body/section/main/section/div/section[2]/section[2]/div[1]/ul[2]/li[1]/button";
            string leaseTypesXPath = @" /html/body/form/div[4]/div[2]/div/div[1]/div/div/div[2]/di;v[1]/ul[2]/li/a";
            string pageListingsXPath = @"/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/div/article[1]/div[3]//section";
            string numListingsFromMapXPath = "id('mapState')/div[2]/div[1]";
            string numElementsFromListLinksXPath = "id('placardSec')/div[2]/ol/li";
            string getJsonSpanXPath = "/html/body/pre/span[153]";
            string logInBtnXPath = "/html/body/section/header/nav/div/div[2]/ul/li[5]/a";
            string logInBtnSmallXPath = "/html/body/section/header/nav/div/div[2]/ul/li[5]/a";
            string subMarkets = "/html/body/section/main/section/div/section[1]/div/div/div[2]/div[1]/div[1]/div/ul/li[2]/label[1]/input";
            //string propertyNamesXPath = @"//*[@id='placardSec']/div[2]/div/article[1]/div[1]/section[2]/div/p";
            string propertyNamesXPath1 = @"/html/body/section/main/section/div/section[2]/section[2]/div[2]/div[1]/article[3]/div[3]/section";
            string propertyNamesXPath2 = @"/html/body/section/main/section/div/section[2]/section[2]/div[2]/div[1]//article/div[1]/section[2]/div[1]/p";
            string propertyTypesDropDownBtnXPath = "/html/body/section/main/section/div/section[1]/div/div/div[2]/div[1]/div[1]/button";
            string pageUrlElementsXPath = "id('placardSec')/div[2]/ol/li/a";
            string pageUrlElementsXPath1 = @"/html/body/section[@class = 'master']/main/section[@class = 'search']/div/section[@class ='results mapview mapview-desktop']/section[@id ='placardSec']/div[2]/ol/li/a";

            const int MAXPROPS4RPRT = 200; //Only For Summary Reports but the data should all be there.  I think.
            //ref; ListingList":[
            #endregion

            //TimeSpan awaitSelectAllTimeSpan = new TimeSpan(0, 0, 2);
            try
            {
                //Variables local to this method
                int index = -1, pageNum = 0;
                int lastPolygonIndex = 0;
                //int TOTAL_REPORTS_SELECTED = 0;
                ChromeOptions options = new ChromeOptions();
                options.AddExtensions(//"libraries\\AdBIock-Plus_v2.1.crx",
                      "libraries\\Block-image_v1.1.crx", "libraries\\AdBlock.crx");
                //options.BinaryLocation = @"C:\Users\john.heidlage\AppData\Local\Chromium\Application\chrome.exe";
                driver = new ChromeDriver(options);
                //driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = awaitDisplay;
                driver.Manage().Timeouts().PageLoad = awaitDisplay;
                //
                try
                {
                    //Incase i didnt logout or someone is using the account #thuglife
                    if (_doLogout)
                    {
                        driver.Navigate().GoToUrl(logoutUrl);
                        //I was kind of hoping to avoid these but screw it, we're on the clock.
                        //Just give it an extra second to do it's work.
                        Thread.Sleep(1000);
                    }
                }
                catch
                {

                }
                driver.Url = @"http://www.loopnet.com";
                //
                driver.FindElementByXPath(logInBtnSmallXPath).Click();
                //Go to login page
                //driver.Navigate().GoToUrl(loginUrl);
                //uid
                var loginUidTextBox = driver.FindElement(By.Id("ctlLogin_LogonEmail"));
                loginUidTextBox.SendKeys(uid);
                //login
                var loginPwdTextBox = driver.FindElement(By.Id("ctlLogin_LogonPassword"));
                loginPwdTextBox.SendKeys(pwd);
                //
                //Click the login button
                //
                var loginBtn = driver.FindElementById("ctlLogin_btnLogon");
                loginBtn.Click();
                //
                //Fast track to the lease polygons (Collier's submarket)
                //
                string namesXPath = "/html/body/form/div[5]/div/div/table/tbody/tr[1]";
                driver.Url = splashPageUrl;
                //
                ////*[@id="logInState"]/section[2]/ul[2]/li[4]/ul/li[5]/a
                //string possibleFixNameUrl = id('form1')/div[5]/div/div/table/tbody//td[1]
                driver.FindElementByXPath(savedSearches2XPath).Click();
                //(@"http://www.loopnet.com/xNet/MainSite/Listing/SavedSearches/MySavedSearches_FSFL.aspx?LinkCode=29400");
                //
                //TODO.  Figure out this XPath stuff.
                //var polyLinkElements = driver.FindElementsByXPath(polygonUrlsXPathA);
                //var polyNameElements = driver.FindElementsByXPath(polygonNamesXPathA);
                //This is so brutal I should be punished for it.

                HtmlDocument assistDoc = new HtmlDocument();
                assistDoc.LoadHtml(driver.PageSource);
                var polyLinkElements = driver.FindElementsByXPath("/html/body/form/div[5]/div/div/table/tbody/tr/td[1]/div/a[position() = 1 and contains(@href, 'for-lease')]");
                var polyNameElements = driver.FindElementsByXPath("/html/body/form/div[5]/div/div/table/tbody/tr/td[2]").Where(x => x.Text.ToLower().Contains("sale") == false).ToList();
                //
                List<string> pageLinkUrls = new List<string>();

                for (int polyCreateIndex = 0; polyCreateIndex < polyLinkElements.Count; polyCreateIndex++)
                {
                    //
                    string polyName = "Polygon Search Num: " + polyCreateIndex;
                    string tempName = polyNameElements[polyCreateIndex].Text;
                    string polyMainUrl = polyLinkElements[polyCreateIndex].GetAttribute("href");
                    //
                    var newPolygon = new Polygon(polyMainUrl, tempName, -1);
                    polygons.Add(newPolygon);
                }
                foreach (var currentPolygon in polygons)
                {
                    index++;
                    currentPolygon.StartedProcessing = true;
                    driver.Navigate().GoToUrl(currentPolygon.Urls[0]);
                    if (index == 0)
                    {
                        OverlayKiller();
                        //
                        driver.FindElementByXPath(propertyTypesDropDownBtnXPath).Click();
                        //
                        var selectAllPropTypesCheckBox = driver.FindElementByXPath(selectAllCheckBoxXPath);
                        if (selectAllPropTypesCheckBox.Selected == false)
                            selectAllPropTypesCheckBox.Click();
                        //
                        //string loopnetSubMarketsXPath = "/html/body/section/main/section/div/section[1]/div/div/div[2]/div[1]/div[1]/div/ul/li/label[1]";
                        driver.FindElementByXPath(propertyTypesDropDownBtnXPath).Click();
                        Thread.Sleep(5000);
                    }
                    //
                    bool initSplash = true;
                    bool moreResults = true;
                    while (moreResults)
                    {
                        pageNum++;
                        //bool screenOverlay = true;
                        List<IWebElement> propertyNameParagraphs = driver.FindElements(By.CssSelector("p.property-name")).ToList();
                        List<string> propertyNamesCollection = new List<string>(propertyNameParagraphs.Count);
                        foreach (IWebElement propParElement in propertyNameParagraphs)
                        {
                            propertyNamesCollection.Add(propParElement.Text);
                        }
                        //
                        for (int resultPageRunCounter = 0; resultPageRunCounter < 2; resultPageRunCounter++)
                        //for (int currentPageUrlIndex = 0; currentPageUrlIndex < currentPolygon.Urls.Count; currentPageUrlIndex++)
                        {
                            System.Diagnostics.Debug.Print("Polygon " + (index + " of " + polygons.Count) + ". Page: "+ pageNum + ". Half: " + resultPageRunCounter + ". " + DateTime.Now.ToShortTimeString());
                            //
                            driver.FindElementByXPath(createReportsBtnXPathA).Click();
                            if (resultPageRunCounter == 1)
                            {
                                if (driver.FindElements(By.ClassName("check-mark")).Count < 11)
                                    break;
                            }
                            //
                            //driver.FindElementByXPath(selectAllPropertiesOnPageXPath).Click();
                            driver.ExecuteScript(@"var checkMarkSpans = document.getElementsByClassName('check-mark');
                            for(var index = arguments[0];index<checkMarkSpans.length && index < arguments[0] + 10;index++) checkMarkSpans[index].click();
                        ", resultPageRunCounter == 0 ? 0 : 10);
                            //driver.ExecuteScript("alert(arguments[0]);", 15);
                            //
                            driver.FindElementByXPath(generateReportsBtnXPath).Click();
                            //
                            //driver.FindElementById("flListingSummary").Click();
                            driver.FindElementById("flListingFull").Click();
                            //
                            driver.FindElementById("btnCreateReport1").Click();
                            //
                            //ChromeWebElement reportFrame = (ChromeWebElement)driver.FindElementById("reportFrame");
                            ///html/body/script[2]
                            try
                            {
                                driver.SwitchTo().Frame(0);
                                //
                                #region Format Json String
                                string jsonRaw = driver.PageSource;
                                jsonRaw = jsonRaw.Substring(jsonRaw.IndexOf(@"<script>Report={"));
                                //
                                //Subtract one because of the quote.  There is also one after.
                                string configEnd = "Config=";
                                int startIndex = jsonRaw.IndexOf("\"ListingList\":");
                                jsonRaw = jsonRaw.Substring(startIndex);
                                jsonRaw = jsonRaw.Remove(jsonRaw.LastIndexOf(configEnd));
                                //
                                jsonRaw = " { " + jsonRaw;
                                jsonRaw = jsonRaw.TrimEnd();
                                jsonRaw = jsonRaw.Remove(jsonRaw.Length - 1, 1);
                                jsonRaw = jsonRaw.Remove(jsonRaw.Length - 1, 1);
                                //
                                #endregion Json String Formatted
                                //
                                //Get an object from the JSON string.
                                Rootobject jsonRootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(jsonRaw);

                                //
                                //currentPolygon.Properties = ew List<LoopNetReportingClasses.Property>();
                                //LoopNetReportingClasses props = currentPolygon.Properties;
                                //Need to populate the properties.
                                //
                                var listingList = jsonRootObject?.ListingList;
                                //
                                //Iterating through the number of properties
                                for (int jsonIndex = 0; jsonIndex < listingList.Length; jsonIndex++)
                                {
                                    //variables starting with x are program built variables, not json data
                                    //Property xProperty = currentPolygon
                                    Property xProperty = new Property();
                                    currentPolygon.Properties.Add(xProperty);
                                    var jsonListing = listingList[jsonIndex];
                                    //
                                    //xProperty.PropertyType = currentPolygon.Name;
                                    xProperty.PropertyName = propertyNamesCollection[resultPageRunCounter == 1 ? jsonIndex + 10 : jsonIndex];
                                    //This needs to be pulled from the dictionary.
                                    xProperty.SubMarket = currentPolygon.Name;
                                    xProperty.Description = jsonListing.PropertyDescription;
                                    xProperty.LoopnetID = Convert.ToString(jsonListing.Id);
                                    xProperty.Url = jsonListing.Uri;
                                    //
                                    var jsonAddressInfo = jsonListing.Address;
                                    xProperty.State = jsonListing.Address.StateProvName;
                                    xProperty.StateId = jsonListing.Address.StateProvCode;
                                    xProperty.City = jsonListing.Address.CityName;
                                    xProperty.ZipCode = jsonListing.Address.PostalCode;
                                    xProperty.Address = jsonListing.Address.StreetAddress;
                                    //
                                    xProperty.Latitude = Convert.ToString(jsonListing.Address?.Geopoint?.Latitude);
                                    xProperty.Longitude = Convert.ToString(jsonListing.Address?.Geopoint?.Longitude);
                                    //
                                    //xProperty.Status = jsonListing.Status;
                                    //xProperty.Description = jsonListing.PropertyDescription;
                                   
                                    //
                                    if (jsonListing.Broker != null)
                                    {
                                        xProperty.Broker = new BrokerClass(jsonListing.Broker?.Name, jsonListing.Broker?.Email,
                                            jsonListing.Broker?.CompanyName, jsonListing.Broker?.Phone);
                                    }
                                    //
                                    ListingFullJSON.Detail[] details = jsonListing?.Details;
                                    for (int detailIndex = 0; detailIndex < details.Length; detailIndex++)
                                    {
                                        var currPropertyCollection = details[detailIndex];
                                        string propertyDetailValue = currPropertyCollection.Value?[0];
                                        //
                                        switch (currPropertyCollection.Name.Trim().ToLower())
                                        {
                                            //TODO: Be fucking careful.  There are objeccts in here where the values are in a one dimensional array.
                                            case "total space available":
                                            case "space available":
                                                if(xProperty.AvailableSF == string.Empty)
                                                    xProperty.AvailableSF = propertyDetailValue;
                                                break;
                                            case "building size":
                                                xProperty.BuildingSF = propertyDetailValue;
                                                break;
                                            case "spaces":
                                                xProperty.spaces = Convert.ToInt32(propertyDetailValue);
                                                break;
                                            case "property sub-type":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.PropertySubType = propertyDetailValue;
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "property type":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.PropertyType = propertyDetailValue;
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "additional sub-types":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.AddtlSubTypes.AddRange(currPropertyCollection.Value);
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "building class":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.BuildingClass = propertyDetailValue;
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "year built":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.YearBuilt = Convert.ToInt32(propertyDetailValue);
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "zoning description":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.ZoningDescription = propertyDetailValue;
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "status":
                                                //TODO: Sort out this cluster fuck
                                                xProperty.Status = propertyDetailValue;
                                                //xProperty.PropertyType = Utils.GetPropType(propertyDetailValue);
                                                break;
                                            case "min. divisible":
                                                xProperty.MinDivisibleSF = propertyDetailValue;
                                                break;
                                            case "max. contiguous":
                                                xProperty.MaxContiguousSF = propertyDetailValue;
                                                break;
                                            default:
                                                //Utils.errors.Add("Detail came through that was unexpected: " + propertyDetailObject.Value[0]);
                                                Utils.SetErrorInfo($"UNHANDLED DETAIL: {propertyDetailValue}");
                                                break;
                                        }
                                    }
                                    int numLeases = jsonListing?.Lease?.Spaces?.Length ?? 0;
                                    xProperty.Leases = new List<LoopNetReportingClasses.Lease>(numLeases);
                                    var jsonLeaseCollection = jsonListing?.Lease?.Spaces ?? new ListingFullJSON.Space[0];
                                    //This is the singular object for all the spaces and the type of them.
                                    for (int leaseIndex = 0; leaseIndex < numLeases; leaseIndex++)
                                    {
                                        var jsonLease = jsonLeaseCollection?[leaseIndex];
                                        var xLease = new LoopNetReportingClasses.Lease();
                                        xProperty.Leases.Add(xLease);
                                        //
                                        //DONT BE CONFUSED.  THE LEASE MONIKER IN THE JSON POINTS TO
                                        //A STRUCT NOT A LIST.  IT'S GOT THE TYPE AND THEN THE LIST.
                                        //var propertyLeaseContainer = jsonLease.
                                        //var propertyLeases = propertyLeaseContainer.Spaces;
                                        xLease.LeaseType = jsonLease.LeaseType;
                                        xLease.Suite = jsonLease?.Number;
                                        xLease.AvailableSF = jsonLease?.SpaceAvailable;
                                        xLease.AskingRateMonthly = jsonLease?.RentalRateMo;
                                        xLease.AskingRate = jsonLease?.RentalRate;
                                        //xLease.AskingRateType = jsonLease.t
                                        //Minimum divisible
                                        //Maximum continguous
                                        xLease.Description = jsonLease?.Description;
                                        xLease.DateAvailable = jsonLease?.DateAvailable;
                                    }
                                }

                                //string linkToGetBackXPath = "id('hlBackTo')";
                                //driver.SwitchTo().ParentFrame();
                                currentPolygon.EndedProcessing = true;
                                //driver.FindElementByXPath(linkToGetBackXPath).Click();
                                //Thread.Sleep(2000);
                                //Add other data
                                //driver.FindElementByXPath(linkToGetBackXPath).Click();

                                //index = indexHolder;
                            }
                            catch
                            {
                                System.Diagnostics.Debug.WriteLine("Error parsing JSON....");
                            };
                            driver.SwitchTo().ParentFrame();
                            //
                            driver.FindElementById("hlBackTo").Click();
                            //
                            driver.FindElementById("lnkBackTo").Click();
                        } //while (moreResults);
                          //Check for more pages.
                        HtmlDocument hDoc = new HtmlDocument();
                        hDoc.LoadHtml(driver.PageSource);
                        //var placNode = hDoc.GetElementbyId("placardSec").ChildNodes.Where(x => ()=
                        //HtmlNodeCollection addtlPagesElements = hDoc.DocumentNode.SelectNodes("id('placardSec')/div[2]/ol/li/a");
                        try
                        {
                            var caretLink = hDoc.DocumentNode.SelectSingleNode("id('placardSec')/div[2]/ol/li/a[@class='caret-right-large']");
                            if (caretLink != null)
                            {
                                moreResults = true;
                                driver.Navigate().GoToUrl(caretLink.GetAttributeValue("href", ""));
                            }
                            else moreResults = false;
                        }
                        catch
                        {
                            moreResults = false;
                        }
                        //string frameSource = frameElement.Text;
                        //driver.SwitchTo().Frame("reportFrame");
                        //

                        ///html/body/table/tbody/tr[55]/td[2]/span
                        //
                        //var leaseCollection = driver.findele



                        //This should work but has a pretty solid chance of breaking somewhere along the line.  
                        // numPolygonProperties = GetTotalPolygonProperties(numListingsFromMapXPath);


                        //string urlOfPropData = @"http://reporting.loopnet.com/Report/Edit/829121a2-7d43-4eeb-8e3c-47d826ee3e3b";
                        //string anotherUrl = @"http://reporting.loopnet.com/Report/Edit/b7c56218-a7a9-41f0-932b-5472a007f0a9";
                        //logout if neccesary
                        //driver.Navigate().GoToUrl(@"http://www.loopnet.com/xNet/MainSite/User/logoff.aspx?LinkCode=850");
                        //
                        //currentPolygon.EndedProcessing = true;
                    }
                }
                try
                {
                    PrintData();
                }
                catch { }
            }
            //
            catch (System.Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                //PrintData();
                driver.Navigate().GoToUrl(logoutUrl);
                //
                driver.Close();
                driver.Quit();
                //
                //
            }
            return "Operation completed succesfully.";


        }

        private void PrintData()
        {
            FileStream stream = new FileStream("loopnet_output.csv",FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < polygons.Count; i++)
            {
                var currentPolygon = polygons[i];
                try
                {
                    for (int j = 0; j < currentPolygon.Properties.Count; j++)
                    {
                        var propertiesList = currentPolygon.Properties[j];
                        writer.Write(propertiesList.ToString());

                    }
                }
                catch
                {
                }
            }
            writer.Close();
            stream.Close();
        }

        private void SelectPropertiesPerPage(string selectAllPropertiesOnPageXPath, string nextPageLinkXPath)
        {

            //var carrotRightElement = driver.FindElementByXPath(nextPageLinkXPath);
            //IWebElement selectAllPropsLink = null;
            //if (carrotRightElement.Enabled)
            //    selectAllPropsLink = driver.FindElementByXPath(selectAllPropertiesOnPageXPath);
            //else
            //    return;
            ////
            //while (carrotRightElement.Enabled == true)
            //{
            //    selectAllPropsLink = driver.FindElementByXPath(selectAllPropertiesOnPageXPath);
            //    selectAllPropsLink.Click();
            //    Thread.Sleep(500);
            //    carrotRightElement.Click();
            //    //
            //    carrotRightElement = driver.FindElementByXPath(nextPageLinkXPath);
            //}
            //string sectionsForIds = "/html/body/section/main/section[1]/div/section[2]/div/div/div/section[1]/div[2]/div/article";
            //IdSelector test = new IdSelector();
            //test.es = driver.Url;
            //List<string> propIdsList = new List<string>();
            //foreach (var propertyInfo in driver.FindElementsByXPath(sectionsForIds))
            //{
            //    propIdsList.Add(propertyInfo.GetAttribute("data-id"));
            //}
            //propIdsList.Add("19535906");
            //test.ids = propIdsList.ToArray();
            //string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(test);
            //string tgtUrl = @"http://www.loopnet.com/services/search/saveState";
            ////test.

        }
        /// <summary>
        /// This works.  May need to slow down though.
        /// </summary>
        /// <param name="numListingsFromMap"></param>
        /// <returns></returns>
        private int GetTotalPolygonProperties(string numListingsFromMap)
        {
            int numProperties = 0, maxTries = 3, tries = 0;
            string resultText = "";
            string pattern = @"^\d+";
            while (true)
            {
                tries++;
                if (tries == maxTries)
                    throw new Exception("Failed to get the # of properties from the Map overlay.");
                try
                {
                    Regex regex = new Regex(pattern);
                    resultText = driver.FindElementByXPath(numListingsFromMap).Text;
                    var match = regex.Match(resultText);
                    if (match.Success == false)
                        throw new RegexException();
                    resultText = match.Captures[0].Value;
                    numProperties = Int32.Parse(resultText);
                    //Got it, return.
                    return numProperties;
                }
                catch (RegexException rex)
                {
                    throw rex;
                }
                catch (Exception ex) { }
            }
        }

        private void ProcessSelectedReportData(Polygon currentPolygon)
        {

            //throw new NotImplementedException();
        }

        private void OverlayKiller()
        {
            try
            {
                //driver.Navigate().GoToUrl(polygons[0].Url);
                var bastardOverlay = driver.FindElementByClassName("onboarding-modal");
                if (bastardOverlay != null & bastardOverlay.Displayed == true)
                {
                    interactions.Actions builder = new OpenQA.Selenium.Interactions.Actions(driver);
                    builder.MoveToElement(bastardOverlay).Click().Build().Perform();
                }
            }
            catch { }
        }

        private void RunManualHttpRequest(ChromeDriver driver)
        {
            ChromeWebElement cwe = (ChromeWebElement)driver.FindElementById("tango");

        }

        private void ProcessJsonReportData(ChromeDriver driver, List<Polygon> polygons)
        {
            string moveToReportTypesXPath = @"/html/body/section/main/section/div/section[2]/section[2]/div[1]/ul[2]/li[1]/button";
            var generateReportsBtn = driver.FindElementByXPath(moveToReportTypesXPath);
            //


        }
    }
}
