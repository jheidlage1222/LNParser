using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using LoopNetReportingClasses;

namespace LoopNetReporting
{
    internal class Polygon
    {
        public Polygon(string mainUrl, string name
            , int numProps = -1)
        {
            Urls = new List<string>() { mainUrl };
            Name = name;
            //PolySearchElement = polySearchElement;
            NumberOfProperties = numProps;
            Properties = new List<Property>();
            jsonStorage = new Dictionary<string, dynamic>();
        }
        public object this[int i]
        {
            get { return jsonStorage[Convert.ToString(i)]; }
            //private { set { InnerList[i] = value; }
        }
        /// <summary>
        /// THIS IS THE URL TO SPLASH PAGE OF THIS POLYGON + addtl pages
        /// </summary>
        public List<string> Urls = null;
        public readonly string Name = "";
        //public int loopnetId = int.MinValue;
        /// <summary>
        /// This may equal null so be warned.  
        /// </summary>
        //public IWebElement PolySearchElement = null;
        public int NumberOfProperties = 0;
        public List<Property> Properties;
        //Bools indicating just what happened to the properties during processing
        //
        public bool StartedProcessing = false;
        public bool EndedProcessing = false;
        public bool ErrorOnProcessing = false;
        private Dictionary<string, dynamic> jsonStorage;
        //public List<string> PageUrls = new List<string>();
        public List<IWebElement> RawHTMLData = new List<IWebElement>();
        public string GetFieldValueFromJson(string inputStr = "")
        {
            return null;
        }
        //
        public int origIndex = -1;
    }
    internal class RegexException : Exception
    {

    }
    internal class IdSelector
    {
        internal string[] ids;
        internal string es = string.Empty;
    }
    //
    
}
