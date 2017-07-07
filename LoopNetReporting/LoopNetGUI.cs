using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
//
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Support.UI;

namespace LoopNetReporting
{
    public partial class LoopNetGUI : Form
    {
        #region Variables
        //BackgroundWorker worker;
        //private static string output = "";
        #endregion

        #region Constructors
        public LoopNetGUI()
        {
            InitializeComponent();
            //
            //tBox_Username.Text = "julie.culver@colliers.com";
            //tBox_Password.Text = "tessarae";
            //tBox_Password.Text = "Paragon2015";
            //
            //worker = new BackgroundWorker();
            //worker.DoWork += worker_DoWork;
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //worker.ProgressChanged += worker_ProgressChanged;
            //worker.WorkerSupportsCancellation = true;
            //worker.WorkerReportsProgress = true;
            tBox_Username.Text = "jennifer.leblanc@colliers.com";
            tBox_Password.Text = "investinboise2017";
        }
        #endregion

        #region EventHandlers
        //private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    tBox_Output.Text = "Polygons parsed : " + e.ProgressPercentage.ToString() + @"%"
        //    + Environment.NewLine + "Polygon Properties Parsed: " + ((int)e.UserState).ToString() +@"%";
        //}
        //private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    tBox_Output.Text = output;
        //    btn_Submit.Enabled = true;
        //}
        //private void worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {

        //        LoopNetXPath parser = new LoopNetXPath();
        //        parser.DoWork(tBox_Username.Text, tBox_Password.Text, worker);
        //        output = "Parsing completed succesfully!!!";

        //    }
        //    catch (Exception ex)
        //    {
        //        output = "ERROR: " + Environment.NewLine + ex.Message + Environment.NewLine + "STACK TRACE:"
        //           + Environment.NewLine + ex.StackTrace + Environment.NewLine;
        //        if (ex.InnerException != null)
        //        {
        //            output += "INNER ERROR: " + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + "INNER STACK TRACE:"
        //            + Environment.NewLine + ex.InnerException.StackTrace + Environment.NewLine;
        //        }
        //        //tBox_Output.Text = output;
        //    }
            
        //}
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            tBox_Output.Text = "Working...";
            btn_Submit.Enabled = false;
            //worker.RunWorkerAsync();
            //LoopNetDriver driver = new LoopNetDriver();
            //Task task = new Task(driver.BeginWork);
            //task.Start();
            LoopnetDriverAlpha driverA = new LoopnetDriverAlpha();
            //
            var retText = driverA.BeginWork(tBox_Username.Text, tBox_Password.Text);
            tBox_Output.AppendText(retText);
            //driverA.BeginWork(tBox_Username.Text, tBox_Password.Text);

        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            tBox_Output.Text = "Cancelling operation....this may take a few moments...";
            //if (worker.IsBusy)
            //    worker.CancelAsync();
        }
        #endregion

        //private void PerformOperations()
        //{
        //    try
        //    {

        //        LoopNetXPath parser = new LoopNetXPath();
        //        parser.DoWork(tBox_Username.Text, tBox_Password.Text);

        //    }
        //    catch (Exception ex)
        //    {
        //        string output = "ERROR: " + Environment.NewLine + ex.Message + Environment.NewLine + "STACK TRACE:"
        //           + Environment.NewLine + ex.StackTrace + Environment.NewLine;
        //        if (ex.InnerException != null)
        //        {
        //            output += "INNER ERROR: " + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + "INNER STACK TRACE:"
        //            + Environment.NewLine + ex.InnerException.StackTrace + Environment.NewLine;
        //        }
        //        tBox_Output.Text = output;

        //    }
        //}

        //private void LoopWebRequest()
        //{
        //    //WebRequest request = WebRequest.Create("http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
        //    //request.Method = "GET";
        //    //WebResponse response = request.GetResponse();
        //    ////
        //    //Stream responseStream = response.GetResponseStream();
        //    //StreamReader reader = new StreamReader(responseStream);
        //    ////
        //    //tBox_Output.Text = reader.ReadToEnd();
        //    ////
        //    string uri = "http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530";
        //    WebClient client = new WebClient();
        //    client.BaseAddress = "http://www.loopnet.com";
        //    Stream responseStream = client.OpenRead("/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
        //    //
        //    StreamReader reader = new StreamReader(responseStream);
        //    tBox_Output.Text = reader.ReadToEnd();
        //    //
        //    //client.Headers = client.ResponseHeaders;
        //    //
        //    try
        //    {
        //        WebHeaderCollection headers = new WebHeaderCollection();
        //        headers.Add(HttpRequestHeader.Host, "www.loopnet.com");
        //        headers.Add(HttpRequestHeader.UserAgent, @"Mozilla/5.0 (Windows NT 6.1; WOW64; rv:35.0) Gecko/20100101 Firefox/35.0");
        //        headers.Add(HttpRequestHeader.Accept, @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        //        headers.Add(HttpRequestHeader.AcceptLanguage, @"en-US,en;q=0.5");
        //        headers.Add(HttpRequestHeader.AcceptEncoding, @"gzip, deflate");
        //        headers.Add(HttpRequestHeader.Referer, @"http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
        //        headers.Add(HttpRequestHeader.Cookie, @" SessionFarm_GUID=d571b18f-8602-460e-bf7f-4e4e6a54cdf6; UDV=SegmentID=0&AnalysisID=0&AssociateID=0; LNUniqueVisitor=6bcc4138-617c-4538-a04e-aaa198f5d6e8; WSSecure=-1; UserLocation_CountryCode=US; UserLocation_PostalCode=45202; UserLocation_StateProvCode=OH; UserLocation_CityName=Cincinnati; UserLocation_QuickRegEnable=true; UserLocation_PhoneCode=1; UserInfo_LastVisited=Sun%20Feb%2022%202015; UserInfo_DateCreated=Thu%20Feb%2019%202015; _ga=GA1.2.1891554898.1424392440; __utma=69319693.1891554898.1424392440.1424480252.1424631005.4; __utmc=69319693; __utmz=69319693.1424392440.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); UserInfo_AssociateID=2980577; UserInfo_MembershipType=PSM; UserInfo=MapSettings=%7B%22style%22%3A%22r%22%2C%20%22zoom%22%3A10%2C%20%22expand%22%3Afalse%2C%20%22orientation%22%3Anull%7D; PDFReportingSession=blxygmmb1hspk20h4pvanef2; PDFReporting=2B911A55D25BBA15FDABDCF50A1141B263BA24C56971F269146610812F9D416705080629730CE00AFA11983F50A7574F37C359BF11228D4B0BE396175821926EB53AF12308DBD1074C6EA8782FB0450BD4DADB5DB3604D1A89E7B7ED4DDA0B4270319B9920B46619EFEBF455379465873BBD6D7F; ReportFrameUrl=PR=http%3A%2F%2Freporting.loopnet.com%2FReport%2FEdit%2F8aad537c-ac88-4613-b59d-f48579ae0dfa; LN_CK=%5b%2218271352%22%5d; ASP.NET_SessionId=i0xv51ugbkojsrve1qypwj2t; AutoRedirect=true; PropertyFirst=SessionToken=&AssociateID=; __utmb=69319693.0.10.1424631005; SiteAccessLockedLocation=/xNet/MainSite/Listing/AddEdit/RenewListing.aspx?FromLocation=&PageLoginRequired=&CustomLogInID=; LNPageLead=1");
        //        //headers.Add(HttpRequestHeader.Connection, @"keep-alive");
        //        headers.Add(HttpRequestHeader.ContentType, @"application/x-www-form-urlencoded");
        //        client.Headers = headers;
        //        //
        //        tBox_Output.Text = client.UploadString(@"/xNet/MainSite/User/customlogin.aspx?LinkCode=530", "VIEWSTATEID=2072408&PgCxtGuid=9ab7ef29-21b5-46c3-ba58-96cc1edabea3&PgCxtFLKey=&PgCxtCurFLKey=&PgCxtDir=&__VIEWSTATE=&ctlLogin%24LogonEmail=julie.culver%40colliers.com&ctlLogin%24LogonPassword=Paragon2015&ctlLogin%24btnLogon.x=38&ctlLogin%24btnLogon.y=15");
        //        //client.Headers = client.ResponseHeaders

        //    }
        //    catch (Exception ex) { }

        //    //
        //}

        //private void StartPathOld()
        //{
        //    string uid = "";
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.CreateHttp("http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");
        //    request.Method = "GET";
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    //
        //    Stream stream = response.GetResponseStream();
        //    tBox_Output.Text = new StreamReader(stream).ReadToEnd();
        //    //
        //    stream.Close();
        //    //
        //    //string loginURI = "http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530";
        //    request.AllowAutoRedirect = true;
        //    request.Method = "POST";
        //    //request.Headers = new WebHeaderCollection();
        //    //request.Headers.Add(response.Headers);
        //    request.Headers.Add("VIEWSTATEID=37694340&PgCxtGuid=533d92b1-23b3-4c7e-a73c-ea66d8fcae36&PgCxtFLKey=&PgCxtCurFLKey=&PgCxtDir=&__VIEWSTATE=&ctlLogin%24LogonEmail=julie.culver%40colliers.com&ctlLogin%24LogonPassword=Paragon2015&ctlLogin%24btnLogon.x=34&ctlLogin%24btnLogon.y=4");
        //    response = (HttpWebResponse)request.GetResponse();
        //    stream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(stream);
        //    //
        //    tBox_Output.Text = reader.ReadToEnd();
        //}
    }
}
