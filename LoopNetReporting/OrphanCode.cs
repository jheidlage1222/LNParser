using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopNetReporting
{
    class OrphanCode
    {
        private void UnusedCode()
        {
            //if ((currentPolygon.NumberOfProps += propertiesOnPage.Count) > 0)
            //{
            //    currentPolygon.RawHTMLData.AddRange(propertiesOnPage);
            //    //Stupid name, this allows you to select all properties on a page.
            //    createReportsBtn.Click();
            //    selectAllPropsLink.Click();
            //}
            //for (int k = 0; k < resultPageLinkElements.Count; k++)
            //{
            //    resultPageLinkElements[k].Click();
            //    var propsOnResultPage = driver.FindElementsByXPath(propertiesOnPageXPathA);
            //    //int numPropsThisPage = driver.FindElementsByXPath(propertiesOnPageXPath).Count;
            //    //
            //    if ((currentPolygon.NumberOfProps += propsOnResultPage.Count) + 20 < maxResultsPerReport)
            //    {
            //        currentPolygon.RawHTMLData.AddRange(propsOnResultPage);
            //        driver.FindElementByXPath(selectAllPropsOnPageXPath).Click();
            //    }
            //    else
            //    {
            //        //Need to process what we have.
            //        ProcessSelectedReportData(currentPolygon);
            //        //Reset what we need to reset.
            //        currentPolygon.NumberOfProps -= propsOnResultPage.Count;
            //        k--;
            //    }
            //}

            #region DeadCodeForNow
            //var propLeaseTypeDD = driver.FindElement(By.Id(@"aMultiLevelDropDown"));
            //propLeaseTypeDD.Click();
            //DateTime start = DateTime.Now;
            //bool visible = false;
            //while (DateTime.Now.Subtract(start) <= awaitDisplay)
            //{
            //    if (propLeaseTypeDD.Displayed == true)
            //    {
            //        visible = true;
            //        break;
            //    }
            //    else
            //        Thread.Sleep(100);
            //}
            //if (!visible)
            //    throw new Exception("Could not make properties sub type list display.");
            //
            //Select For Lease option
            //
            //var forLeaseLink = driver.FindElement(By.PartialLinkText("For Lease"));

            //var locationTxtBox = driver.FindElement(By.Id("Location"));
            //locationTxtBox.SendKeys("Boise, Idaho");
            //df
            //var propSubTypeList = driver.FindElement(By.Id("aMultiLevelDropDown"));
            //propSubTypeList.Click();
            //Get the URLs for 
            #endregion


        }
    }
}
