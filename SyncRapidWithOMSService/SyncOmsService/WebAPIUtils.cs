using CrowlData.ValueObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CrawlDataService
{
    class WebAPIUtils
    {
        string _listFields = string.Empty;
        string _baseAddress = string.Empty;
        string _procName = string.Empty;
        double _timeBetweenRuns = 10000;
        public void InitWebClient(string listFields, string baseAddress, string procName, double timeBetweenRuns)
        {
            _listFields = listFields;
            _baseAddress = baseAddress;
            _procName = procName;
            _timeBetweenRuns = timeBetweenRuns;
        }

        public WebResponse CallToPostWebAPI(PostData data, string apiFunctionNamePath)
        {
            string json = new JavaScriptSerializer().Serialize(data);
            // Create a request using a URL that can receive a post.   
            WebRequest request = WebRequest.Create(_baseAddress + apiFunctionNamePath);
            // Set the Method property of the request to POST.  
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            //byte[] byteArray = Encoding.UTF8.GetBytes(GetJsonFromList(data));
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            request.Headers.Add(HttpRequestHeader.Authorization, "Basic cnBAZWVlZWU6ZTFlMmUzZTRlNWQ2");
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();

            return request.GetResponse();
        }

        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead(_baseAddress))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public string GetJsonFromDataTable(DataTable dt, int limit = 1)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows =
              new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;
            serializer.MaxJsonLength = int.MaxValue;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    //if (col.ColumnName.Trim() == "MSGXML" && limit == 1)
                    //{
                    //    row.Add(col.ColumnName.Trim(), "");
                    //}
                    //else
                    //{
                        row.Add(col.ColumnName.Trim(), dr[col]);
                    //}
                }
                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }

        public string GetJsonFromList(PostData dataInput)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            Dictionary<string, object> row = new Dictionary<string, object>();
            
            row.Add("ShipmentNo", dataInput.ShipmentNo);
            //row.Add("Status", dataInput.Status);

            return serializer.Serialize(row);
        }

        public void WriteLog(Exception ex)
        {
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile"+DateTime.Today.ToString("yyyyMMdd")+".txt", true);
                writer.WriteLine("Exception at: " + DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                writer.Flush();
                writer.Close();
            }
            catch { }
        }

        public void WriteLog(string message)
        {
            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile" + DateTime.Today.ToString("yyyyMMdd") + ".txt", true);
                writer.WriteLine();
                writer.WriteLine(DateTime.Now.ToString() + ": " + message);
                writer.Flush();
                writer.Close();
            }
            catch { }
        }
    }
}
