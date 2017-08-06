using CrawlDataService.BusinessLayer;
using CrowlData.BusinessLayer;
using CrowlData.ValueObject;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace CrawlDataService
{
    public partial class SyncOmsService : ServiceBase
    {
        System.Timers.Timer timerCheck = new System.Timers.Timer();//create timer
        string listFields = string.Empty;
        string baseAddress = string.Empty;
        string procName = string.Empty;
        string apiFunctionPath = string.Empty;
        double timeBetweenRuns = 10000;
        int limit = 1;
        int rowAmount = 1;
        ShipmentBUS cpn = new ShipmentBUS();
        WebAPIUtils apiUtils = new WebAPIUtils();

        public SyncOmsService()
        {
            InitializeComponent();
            try
            {
                listFields = ConfigurationManager.AppSettings["listFields"];
                rowAmount = int.Parse(ConfigurationManager.AppSettings["rowAmount"]);
                baseAddress = ConfigurationManager.AppSettings["baseAPIAddress"];
                procName = ConfigurationManager.AppSettings["procSelectOneRowName"];
                timeBetweenRuns = double.Parse(ConfigurationManager.AppSettings["timeBetweenRuns"]);
                apiFunctionPath = ConfigurationManager.AppSettings["apiFunctionPath"];
                limit = int.Parse(ConfigurationManager.AppSettings["limit_MSGXML"]);
                apiUtils.InitWebClient(listFields, baseAddress, procName, timeBetweenRuns);
            }
            catch (Exception ex) { apiUtils.WriteLog(ex); }
        }
        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            timerCheck.Elapsed += new ElapsedEventHandler(timerCheck_Elapsed);
            timerCheck.Interval = timeBetweenRuns;//set timer interval (1000 = 1s)
            timerCheck.Enabled = true;
            timerCheck.Start();//start timer
        }

        protected override void OnStop()
        {
        }

        public void onDebug()
        {
            OnStart(null);
        }

        private void timerCheck_Elapsed(object sender, EventArgs e)
        {
            if (apiUtils.CheckForInternetConnection())
            {
                PostDataToAPI(0);
                PostDataToAPI(1);
            }
        }

        private void PostDataToAPI(int tableType)
        {
            try
            {
                string type = tableType == 1 ? "IN" : "OUT";
                PostData dataPost = new PostData();
                dataPost.Status = type;
                DataTable dataToPost;
                if (tableType == 1)
                {
                    dataToPost = cpn.GetAllShipmentInfoWithStatusZero(rowAmount, listFields);
                }
                else
                {
                    dataToPost = cpn.GetAllShipmentOutWithStatusZero(rowAmount, listFields);
                }

                if (dataToPost != null && dataToPost.Rows.Count > 0)
                {
                    //JObject json = JObject.Parse(apiUtils.GetJson(dataToPost));
                    dataPost.ShipmentNo = apiUtils.GetJsonFromDataTable(dataToPost, limit);
                    HttpWebResponse response = (HttpWebResponse)apiUtils.CallToPostWebAPI(dataPost, apiFunctionPath);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Get the request stream.  
                        Stream dataStream = response.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.  
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        // Read the content.  
                        string responseFromServer = reader.ReadToEnd();
                        // Display the content.  
                        //Console.WriteLine(responseFromServer);

                        string res = string.Empty;

                        // ... Read the string.
                        //Task<string> result = content.ReadAsStringAsync();
                        //res = result.Result;
                        JObject json = JObject.Parse(responseFromServer);

                        if (json["code"].ToString() == "success")
                        {
                            apiUtils.WriteLog("\n******Post sync data success: " + DateTime.Now);
                            //StringBuilder ids = new StringBuilder();
                            StringBuilder shipmentIdList = new StringBuilder();
                            int totalRow = dataToPost.Rows.Count;
                            for (int i = 0; i < totalRow; i++)
                            {
                                if (shipmentIdList.Length == 0)
                                {
                                    shipmentIdList.Append("'"+dataToPost.Rows[i]["ShipmentID"]+"'");
                                }
                                else
                                {
                                    shipmentIdList.Append(",");
                                    shipmentIdList.Append("'" + dataToPost.Rows[i]["ShipmentID"] + "'");
                                }
                            }

                            int updateData = cpn.UpdateDataAfterSuccess(shipmentIdList.ToString(), tableType);

                            if (updateData > 0)
                                apiUtils.WriteLog("\n ******Success sync " + type + " " + totalRow + "(" + updateData + ") rows to database." + Environment.NewLine + "\nList ShipmentId:" + shipmentIdList.ToString());
                            else
                                apiUtils.WriteLog("\n ******Fail sync " + type + " " + totalRow + " rows to database." + Environment.NewLine + "\n List ShipmentId:" + shipmentIdList.ToString());
                        }
                        else
                        {
                            apiUtils.WriteLog("\n ******Post sync data success but get error: " + json.Values("message").ToString());
                        }
                        // Clean up the streams.  
                        reader.Close();
                        dataStream.Close();
                        response.Close();
                    }
                    else
                    {
                        apiUtils.WriteLog("\n******Post sync data fail : " + "Error Code");
                        //response.StatusCode + " : Message - " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                apiUtils.WriteLog(ex);
            }
        }
    }
}
