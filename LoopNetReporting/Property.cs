using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoopNetReporting;


namespace LoopNetReportingClasses
{
    public class Property
    {
        public Property()
        {

        }
        private const string delim = ",";

        /// <summary>
        /// CRITICAL. Just name of polygon though.
        /// </summary>
        public string PropertyType = "";
        /// <summary>
        /// CRITICAL - MOST OFTEN SET AT LEASE LEVEL
        /// </summary>
        public string PropertyName = "";

        #region FINISHED AND SET
        /// <summary>
        ///  CRITICAL A OK
        /// </summary>
        public string Description = "";
        /// <summary>
        /// CRITICAL
        /// </summary>
        public string Address = "";
        /// <summary>
        /// CRITICAL
        /// </summary>
        public string City = "";
        /// <summary>
        /// CRITICAL
        /// </summary>
        public string State = "";
        /// <summary>
        /// CRITICAL
        /// </summary>
        public string ZipCode = "";
        /// <summary>
        /// CRITICAL-ISH
        /// </summary>
        public string County = "";
        /// <summary>
        /// CRITICAL
        /// </summary>
        public string BuildingSF = "";
        //
        public string Latitude = "";
        public string Longitude = "";
        public string StateId = "";
        //CRITICAL
        public BrokerClass Broker; 
        #endregion

        public string SubMarket = "";
        public string PropertySubType = "";
        public string BuildingClass = "";
        //public string ListingBroker = "";
        public string LoopnetID = "";
        public List<Lease> Leases = new List<Lease>();
        //public string PolygonName = "";
        public string Url = "";
        //New
        public List<string> AddtlSubTypes = new List<string>();
        public int YearBuilt = 2099;
        public string ZoningDescription = "";
        public string MinDivisibleSF = "";
        public string MaxContiguousSF = "";
        //End New
        public int spaces = 0;
        public string AvailableSF = "";
        public string Status = "";
        //
        public override string ToString()
        {
            string retRow = "";
            string baseRow = "";
            //
            //string delim = ",";
            this.SanitizeFields();
            int leaseNum = 1;
            foreach(Lease lease in Leases)
            {
                lease.SanitizeFields();
                baseRow = SubMarket + delim + PropertyType + delim + PropertySubType + delim + Leases.Count + delim + leaseNum + delim + PropertyName + delim + Address + delim
                    + City + delim + State + delim + ZipCode + delim + BuildingSF + delim + Status + delim + YearBuilt.ToString() + delim + ZoningDescription + delim + MinDivisibleSF + delim + MaxContiguousSF + delim + BuildingClass + delim
                    + lease.Suite + delim + lease.AvailableSF + delim + lease.AskingRateMonthly + delim + lease.AskingRate + delim + lease.AskingRateType
                    + delim + lease.LeaseType + delim + LoopnetID + delim + Broker.PrintBrokerData() + delim + lease.Description + delim + Url + delim;
                //
                foreach (string addtlSubType in AddtlSubTypes)
                    baseRow += addtlSubType + " | ";
                retRow += baseRow + delim +  Environment.NewLine;
                leaseNum++;

            }
            //retRow = retRow.Substring(0, retRow.LastIndexOf(Environment.NewLine));
            return retRow;
        }
        //County + delim +
        //private string StripCommas(string value)
        //{
        //    if (value != null)
        //        return value.Replace(",", string.Empty).Replace("\r\n", string.Empty).Replace(Environment.NewLine, string.Empty).Replace("\"", string.Empty);
        //    else
        //        return string.Empty;
        //}
        public void SanitizeFields()
        {
            this.PropertyType = Utils.PrepareData(this.PropertyType);
            this.PropertyName = Utils.PrepareData(this.PropertyName);
            this.Address = Utils.PrepareData(this.Address);
            this.City = Utils.PrepareData(this.City);
            this.State = Utils.PrepareData(this.State);
            this.ZipCode = Utils.PrepareData(this.ZipCode);
            this.BuildingSF = Utils.PrepareData(this.BuildingSF);
            this.SubMarket = Utils.PrepareData(this.SubMarket);
            this.PropertySubType = Utils.PrepareData(this.PropertySubType);
            this.BuildingClass = Utils.PrepareData(this.BuildingClass);
            //this.ListingBroker = Utils.StripCommas(this.ListingBroker);
            this.LoopnetID = Utils.PrepareData(this.LoopnetID);
            this.Description = Utils.PrepareData(this.Description);
            this.Url = Utils.PrepareData(this.Url);
            //this.PolygonName = Utils.StripCommas(this.PolygonName);
            this.Latitude = Utils.PrepareData(this.Latitude);
            this.Longitude = Utils.PrepareData(this.Longitude);
            this.StateId = Utils.PrepareData(this.StateId);
            // public List<string> AddtlSubTypes = new List<string>();
            //public int YearBuilt = 2099;
            //public string ZoningDescription = "";
            //public string MinDivisibleSF = "";
            //public string MaxContiguousSF = "";
            for (int i = 0; i < this.AddtlSubTypes.Count; i++)
                AddtlSubTypes[i] = Utils.PrepareData(AddtlSubTypes[i]);
            //YearBuilt is an int so it's cool.
            this.ZoningDescription = Utils.PrepareData(this.ZoningDescription);
            this.Status = Utils.PrepareData(this.Status);
            this.MinDivisibleSF = Utils.PrepareData(this.MinDivisibleSF);
            this.MaxContiguousSF = Utils.PrepareData(this.MaxContiguousSF);
        }
    }
    public class BrokerClass
    {
        public BrokerClass(string name, string email, string companyName, string phone)
        {
            this.Name = name ?? "NO NAME PRESENT"; this.Email = email ?? "NO EMAIL PRESENT"; this.CompanyName = companyName ?? "COMPANY NAME NOT PRESENT";
            this.Phone = phone ?? "NO PHONE LISTED";

        }
        public string PrintBrokerData()
        {
            string delim = ",";
            return Utils.PrepareData(Name) + delim + Utils.PrepareData(CompanyName) + delim + Utils.PrepareData(Email) + delim + Utils.PrepareData(Phone);
        }
        public string Name = "";
        public string Email = "";
        public string CompanyName = "";
        public string Phone = "";
         
    }
}
