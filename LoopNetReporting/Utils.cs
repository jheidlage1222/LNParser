using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace LoopNetReporting
{
    /// <summary>
    /// By and large, this class just contains enumerations
    /// that can be used to find their sub-type.
    /// </summary>
    public static class Utils
    {
        public static string relayInfo = "";
        public static string SetErrorInfo(params string[] relayInfoArray)
        {
            if (relayInfo != null && relayInfo.Length > 0)
            {
                relayInfo = "THE FOLLOWING INFO HAS BEEN RELAYED: ";
                foreach (string dataBit in relayInfoArray)
                {
                    relayInfo += dataBit;
                }

            }
            return relayInfo;
        }
        public static string subMarketOut = "ERROR.";
        public enum SpaceType { Spaces, Lots };
        public static Dictionary<string, string> subTypesToTypes = new Dictionary<string, string>();
        private static List<List<string>> sMarketsTosTypes = new List<List<string>>();
        
        public 
         static string PrepareData(string value)
        {
            if (value == null)
                return " ";
            if (value.Contains("\"") || value.Contains("'") || value.Contains("&amp;"))
                return "FIELD UNAVAILABLE";
            //
            value = value.Replace(',', ' ').Trim();
            value = value.Replace('\r', ' ');
            value = value.Replace('\n', ' ');
            value = value.Replace('\f', ' ');
            value = value.Replace('\t', ' ');
            value = value.Replace('=', ' ');
            //
            //if (value != null)
            //    value = value.Replace(",", string.Empty).Replace("\r\n", string.Empty).Replace(Environment.NewLine, string.Empty);//.Replace("\"", string.Empty);
            //else
            //    value =  string.Empty;
            return value;
        }
        public static bool TypesAndSubTypesLoaded = false;
        /// <summary>
        /// DOES NOT WORK.
        /// </summary>
        /// <param name="webDriver"></param>
        public static void LoadTypesAndSubTypes(IWebDriver webDriver)
        {

            //string subTypesOfMarketsXPath = "/html/body/section/main/section/div/section[1]/div/div/div[2]/div[1]/div[1]/div/ul/li[2]/div/ul/li/label";
            //string subMarketsXPath = "/html/body/section/main/section/div/section[1]/div/div/div[2]/div[1]/div[1]/div/ul/li/label[1]/input";
            string listElementAboveDataXPath = "/html/body/section/main/section/div/section[1]/div/div/div[2]/div[1]/div[1]/div/ul/li";
            var childNodes = webDriver.FindElements(By.XPath(listElementAboveDataXPath));
            if (childNodes == null || childNodes.Count <= 1)
                throw new Exception("Failed to get the parent element for Sub Markets and Sub Types.");
            //string staticText = "FAILURE.";
            for (int i = 1; i < childNodes.Count; i++)
            {
                var listItemElement = childNodes[i];
                var subMarketInputElement = listItemElement.FindElement(By.XPath("/label[1]/input"));
                var subTypesOfCurrentsubMark = listItemElement.FindElements(By.XPath("/div/ul/li/label/input")).ToList();

                if (subTypesToTypes.ContainsKey(subTypesOfCurrentsubMark[i].Text) == false)
                {
                    //for(int j = 0; j < subTypesOfCurrentsubMark
                    //subTypesToTypes.Add(subTypesOfCurrentsubMark[i].Text, subMarketInputElement.Text);
                }


            }
            TypesAndSubTypesLoaded = true;
        }
        /// <summary>
        /// THIS IS NOT IMPLEMENTED
        /// </summary>
        /// <param name="subType"></param>
        /// <returns></returns>
        public static string GetPropType(string subType)
        {
            if (subTypesToTypes.TryGetValue(subType, out subMarketOut))
            {
                subType = subMarketOut;
                return subType;
            }
            else
            {
                subType = $"Could not retrieve Sub Market for Sub Type {subType}";
                return subType;

            }
        }
        //public static bool (
        //If this words.  Holly dog shit.
        public static int retailProps = 0;
        public static int reportsThatWouldaBeen = 0;

        #region Enums for Types and Subtypes.
        /// <summary>
        /// BE ADVISED.  THE USAGE of THE double underscore symbol will sit in place of
        /// the forward slash.  
        /// </summary>
        public enum Retail
        {
            CommunityCenter = 0,
            StripCenter = 1,
            NeighborhoodCenters = 2,
            OutletCenter = 3,
            PowerCenter = 4,
            RegionalCenterMall  = 5,
            SuperRegionalCenter = 6,
            SpecialtyCenter = 7,
            ThemeFestivalCenter = 8,
            Anchor = 9,
            Restaurant = 10,
            RetailPad = 11,
            FreeStandingBldg = 12,
            StreetRetail = 13,
            VehicleRelated = 14,
            /// <summary>
            /// This is literally "Retail (Other)" 
            /// </summary>
            RetailOther = 15
        }
        /// <summary>
        /// The underscore replaces spaces (rhyme, unintentional)
        /// Double underscore means you've won the Jackpot.  Hah.  Kidding.
        /// It's, again, a forward slash.  And read the damn comments
        /// over the elements please.  I'm begging you.
        /// </summary>
        public enum Industrial
        {
            FlexSpace = 0,
            Manufacturing = 1,
            OfficeShowroom = 2,
            /// <summary>
            /// LOOK OUT.  It's literally 'R&D'
            /// </summary>
            RandD = 3,
            /// <summary>
            /// Truck Terminal/Hub/Transit
            /// </summary>
            TruckTerminalHubTransit = 4,
            Warehouse = 5,
            DistributionWarehouse = 6,
            /// <summary>
            /// "Refrigerated/Cold Storage
            /// </summary>
            RefrigeratedColdStorage = 7


        }
        /// <summary>
        /// Remeber.  Double "" means '/'
        /// Also, check comments over your member.  
        /// </summary>
        public enum Office
        {
            CreativeLoft = 0,
            OfficeBuilding = 1,
            InstitutionalGovernment = 2,
            MedicalOffice = 3,
            /// <summary>
            /// This is literally Office-R&D
            /// </summary>
            OfficeRandD = 4,
            ExecutiveSuiter = 5

        }
        /// <summary>
        /// All had (land) after them so I just
        /// got rid of it.  We'll cross that bridge if we get to it.
        /// </summary>
        public enum Land
        {
            Industrial = 0,
            Multifamily = 1,
            Office = 2,
            Residential = 3,
            Retail = 4,
            /// <summary>
            /// The underscore is actually 
            /// </summary>
            RetailPad = 5

        }
        public enum SpecialPurpose
        {
            /// <summary>
            /// Again with the fucking spaces.....good lord.
            /// </summary>
            SpecialPurpose
        }
        

        #endregion
        
    }
}
