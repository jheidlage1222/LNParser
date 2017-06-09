using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoopNetReporting;

namespace LoopNetReportingClasses
{
    public class Lease
    {
        public string Suite = "";
        public string AvailableSF = "";
        public string AskingRate = "";
        public string AskingRateMonthly = "";
        public string AskingRateType = "";
        public string LeaseType = "";
        public string Description = "";
        //New
        public string DateAvailable = "";
        //private string StripCommas(string value)
        //{
        //    if (value.Contains("\"") || value.Contains("'") || value.Contains("&amp;"))
        //        return "FIELD UNAVAILABLE";
        //    if (value != null)
        //        return value.Replace(',', ' ').Replace("\r\n", " ").Replace(Environment.NewLine, " ").Replace("\"", string.Empty);
        //    else
        //        return string.Empty;
        //}
        internal void SanitizeFields()
        {
            this.Suite = Utils.PrepareData(this.Suite ?? " ");
            this.AvailableSF = Utils.PrepareData(this.AvailableSF ?? " ");
            this.AskingRateMonthly = Utils.PrepareData(this.AskingRateMonthly ?? " ");
            this.AskingRate = Utils.PrepareData(this.AskingRate ?? " ");
            this.AskingRateType = Utils.PrepareData(this.AskingRateType ?? " ");
            this.LeaseType = Utils.PrepareData(this.LeaseType ?? " ");
            this.Description = Utils.PrepareData(this.Description ?? " ");
            //new
            //LeaseType = StripCommas(LeaseType ?? "");
        }
    }
}
