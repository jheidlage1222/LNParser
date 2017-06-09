using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
//using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace LoopNetHTTP
{
    public class LoopNetParser
    {
        private string HostName = "www.loopnet.com";
        public void MakeRequests()
        {
            try
            {
                HttpWebResponse response;
                bool loginSuccess = PerformLoginIn(out response);
                if (loginSuccess)
                {
                    //
                    loginSuccess = false;
                    string resp = ReadResponse(response);
                    HtmlDocument hDoc = new HtmlDocument();
                    hDoc.LoadHtml(resp.Replace("/r", "").Replace("/n", ""));
                    response.Close();
                    //
                    string url = string.Empty;
                    var linkNode = hDoc.CreateNavigator().SelectSingleNode("//a");
                    url = linkNode.GetAttribute("href", "");
                    //
                    

                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Parsing failed.", ex);
            }
        }
        private string ReadResponse(HttpWebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
	        {
		        Stream streamToRead = responseStream;
		        if (response.ContentEncoding.ToLower().Contains("gzip"))
		        {
			        streamToRead = new GZipStream(streamToRead, CompressionMode.Decompress);
		        }
		        else if (response.ContentEncoding.ToLower().Contains("deflate"))
		        {
			        streamToRead = new DeflateStream(streamToRead, CompressionMode.Decompress);
		        }

                using (StreamReader streamReader = new StreamReader(streamToRead, Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
	        }

        }
        private bool MoveToLoginHome(out HttpWebResponse response, HttpWebResponse prevResponse, string url)
        {
            response = null;

            try
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.loopnet.com/xNet/MainSite/User/home/myloopnet.aspx?linkcode=20");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
                request.Referer = "http://www.loopnet.com/xNet/MainSite/User/home/myloopnet.aspx";
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                //request.Headers.Set(HttpRequestHeader.Cookie, @"WSSecure=2371215; UserLocation_CountryCode=US; UserLocation_PostalCode=83702; UserLocation_StateProvCode=ID; UserLocation_CityName=Boise; UserLocation_QuickRegEnable=true; UserLocation_PhoneCode=1; LN_CK=%5b%2218248357%22%5d; ReportFrameUrl=PR=http%3A%2F%2Freporting.loopnet.com%2FReport%2FEdit%2F8cb1ce15-b034-4f53-a24a-412242f49eac; UserInfo_LastVisited=Wed%20Sep%2030%202015; UserInfo_DateCreated=Mon%20Sep%2028%202015; UserInfo_AssociateID=2980577; UserInfo_MembershipType=PSM; UserInfo=MapSettings=%7B%22style%22%3A%22r%22%2C%20%22zoom%22%3A2%2C%20%22expand%22%3Afalse%2C%20%22orientation%22%3Anull%7D; ADRUM_BTa=R:58|g:766a44ee-1a98-4594-8291-0e3621b1da8b; ADRUM_BT1=R:58|i:2872|e:85; SessionFarm_GUID=f30c22dd-8f3a-47e9-b36f-041fbb194240; cmpi=lncEfu6y979g; PDFReportingSession=xv2pt5hth040dzmixoxeraje; safari_cookie_fix=; PropertyFirst=SessionToken=&AssociateID=; __utmc=69319693; UDV=SegmentID=0&AnalysisID=2476&AssociateID=2980577; LNUniqueVisitor=d1a785c9-c52e-40d9-b6a1-247c5841a6fc; _ga=GA1.2.10694929.1443487270; __utma=69319693.10694929.1443487270.1443573626.1443649453.5; __utmz=69319693.1443487270.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); optimizelySegments=%7B%223205580370%22%3A%22gc%22%2C%223205760545%22%3A%22direct%22%2C%223222130482%22%3A%22false%22%2C%223235610269%22%3A%22none%22%7D; optimizelyEndUserId=oeu1443487270527r0.6601626568681453; optimizelyBuckets=%7B%7D; LNPageLead=1; PDFReporting=4586C255F53F305FB2998C9732B96244084D27FF31755A57813DE9081A2178245C19E705F0EAE1786B6AC0A23474FC264F431ECD334FE8C882C8DBFD80C20FF096A4244252CBA917615305C7C408F70002A67FCA7DA47AB051EA29D3EA006F37C043DF8972128A0C31C02F01946FD90FB8848CFB; _gat=1; LastUserEmail=julie.culver@colliers.com; optimizelyPendingLogEvents=%5B%5D; ShowPopupRegConfirmation=false");
                SetCookies(request, prevResponse);
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;

        }

        private void SetCookies(HttpWebRequest request, HttpWebResponse prevResponse)
        {
            if (prevResponse.Cookies == null)
                return;
            foreach (Cookie cookie in prevResponse.Cookies)
            {
                try
                {
                    if(cookie.Discard == false)
                        request.Headers.Set(HttpRequestHeader.Cookie, cookie.Value);
                }
                catch { }
            }
        }
        private bool PerformLoginIn(out HttpWebResponse response)
        {
            response = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530");

                request.Accept = "text/html, application/xhtml+xml, image/jxr, */*";
                request.Referer = "http://www.loopnet.com/xNet/MainSite/User/customlogin.aspx?LinkCode=530";
                request.Headers.Set(HttpRequestHeader.AcceptLanguage, "en-US,en;q=0.5");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10240";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Set(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                request.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
                request.Headers.Set(HttpRequestHeader.Cookie, @"WSSecure=-1; UserLocation_CountryCode=US; UserLocation_PostalCode=45069; UserLocation_StateProvCode=OH; UserLocation_CityName=West Chester; UserLocation_QuickRegEnable=true; UserLocation_PhoneCode=1; LN_CK=%5b%2218248357%22%5d; UserInfo_LastVisited=Tue%20Sep%2029%202015; UserInfo_DateCreated=Mon%20Sep%2028%202015; UserInfo_AssociateID=2980577; UserInfo_MembershipType=PSM; UserInfo=MapSettings=%7B%22style%22%3A%22r%22%2C%20%22zoom%22%3A2%2C%20%22expand%22%3Afalse%2C%20%22orientation%22%3Anull%7D; _bizo_bzid=6fd17b8a-8839-437d-a690-1a2c186f5ba7; _bizo_cksm=FB90A8372B52BE4E; _bizo_np_stats=155%3D199%2C; ReportFrameUrl=PR=http%3A%2F%2Freporting.loopnet.com%2FReport%2FEdit%2F8cb1ce15-b034-4f53-a24a-412242f49eac; ADRUM_BTa=R:71|g:98bc8f5f-8492-4e23-924c-636852aaf71e; ADRUM_BT1=R:71|i:2872|e:74; UDV=SegmentID=0&AnalysisID=0&AssociateID=0; LNUniqueVisitor=d1a785c9-c52e-40d9-b6a1-247c5841a6fc; _ga=GA1.2.10694929.1443487270; __utma=69319693.10694929.1443487270.1443487270.1443562517.2; __utmz=69319693.1443487270.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); optimizelySegments=%7B%223205580370%22%3A%22gc%22%2C%223205760545%22%3A%22direct%22%2C%223222130482%22%3A%22false%22%2C%223235610269%22%3A%22none%22%7D; optimizelyEndUserId=oeu1443487270527r0.6601626568681453; optimizelyBuckets=%7B%7D; LNPageLead=1; SessionFarm_GUID=f30c22dd-8f3a-47e9-b36f-041fbb194240; cmpi=lncEfu6y979g; PDFReportingSession=xv2pt5hth040dzmixoxeraje; PDFReporting=4586C255F53F305FB2998C9732B96244084D27FF31755A57813DE9081A2178245C19E705F0EAE1786B6AC0A23474FC264F431ECD334FE8C882C8DBFD80C20FF096A4244252CBA917615305C7C408F70002A67FCA7DA47AB051EA29D3EA006F37C043DF8972128A0C31C02F01946FD90FB8848CFB; safari_cookie_fix=; PropertyFirst=SessionToken=&AssociateID=; optimizelyPendingLogEvents=%5B%5D; _gat=1; ShowPopupRegConfirmation=false");

                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false;

                string body = @"__EVENTTARGET=ctlLogin%24btnLogon&__EVENTARGUMENT=&VIEWSTATEID=12648993&PgCxtGuid=b0616aee-200c-447b-8be6-83c12b050574&PgCxtFLKey=&PgCxtCurFLKey=&PgCxtDir=&__VIEWSTATE=&ctlLogin%24LogonEmail=julie.culver%40colliers.com&ctlLogin%24LogonPassword=tessarae";
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();

                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError) response = (HttpWebResponse)e.Response;
                else return false;
            }
            catch (Exception)
            {
                if (response != null) response.Close();
                return false;
            }

            return true;
        }
    }
}
