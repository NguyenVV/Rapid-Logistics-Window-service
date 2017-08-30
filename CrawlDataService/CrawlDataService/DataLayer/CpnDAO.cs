
using System;
using System.Data;
using System.Data.SqlClient;

namespace CrawlDataService.DataLayer
{
    class CpnDAO
    {
        private SQLConnections conn;
        SqlConnection newConn;
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnectionStringRapidSolution"].ConnectionString;
        /// <constructor>
        /// Constructor UserDAO
        /// </constructor>
        public CpnDAO()
        {
            conn = new SQLConnections();
            newConn = new SqlConnection(connStr);
        }

        /// <method>
        /// Get User Email By Firstname or Lastname and return DataTable
        /// </method>
        public DataTable GetAllDataNewWithStatusZero(int rowSelect, string listFieldsToSelect)
        {
            var str = listFieldsToSelect.Replace(",MSGXML", "").Replace("MSGXML,", "").Replace(",SOTK", "").Replace("SOTK,", "").Split(',');
            bool isContainId = false;

            foreach (string x in str)
            {
                if (x.Equals("Id", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    isContainId = true;
                    break;
                }
            }

            if (!isContainId)
            {
                listFieldsToSelect += ",Id";
            }
            string query = string.Format("SELECT TOP {0} {1},MSGXML,SOTK from CPN_OutputMSG where MSGCODE is not null and TRANG_THAI =0", rowSelect, listFieldsToSelect);
            //string query = string.Format(@"SELECT TOP {0} s.*, a.[Msgxml].value('(/Root/Declaration/DeclarationNo)[1]', 'VARCHAR(MAX)') AS 'SOTK', a.[Msgxml].value('(/Root/ShipmentID)[1]', 'VARCHAR(MAX)') AS 'ShipmentNo' FROM(SELECT CAST(Msgxml AS XML) AS xmlMsgxml, {1} FROM CPN_OutputMSG where MSGCODE is not null and TRANG_THAI = 0) s CROSS APPLY xmlMsgxml.nodes('/') a (Msgxml)", rowSelect, listFieldsToSelect);
            DataTable result = conn.executeSelectQuery(query);

            return result;
        }

        public int UpdateStatusByListId(string ids)
        {
            string query = string.Format("update CPN_OutputMSG set TRANG_THAI=1 where ID in("+ids+")");

            return conn.executeUpdateQuery(query);
        }

        public void UpdateSotokhai(DataTable table)
        {
            if(table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    string query = string.Format("update [RapidSolution].[dbo].[ShipmentInfor] set DeclarationNo='{0}' where ShipmentId ='{1}'",row["sotk"],row["ShipmentID"]);
                    ExecuteUpdateQuery(query);
                }
            }
        }

        private SqlConnection OpenNewConn()
        {
            if (newConn.State == ConnectionState.Closed || newConn.State ==
                        ConnectionState.Broken)
            {
                newConn.Open();
            }

            return newConn;
        }

        /// <summary>
        /// Close Connection
        /// </summary>
        private void CloseConnection()
        {
            if (newConn.State == ConnectionState.Open || newConn.State ==
                        ConnectionState.Connecting)
            {
                newConn.Close();
            }
        }

        private int ExecuteUpdateQuery(string _query)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                if (connection.State == ConnectionState.Closed || connection.State ==
                        ConnectionState.Broken)
                {
                    connection.Open();
                }

                using (var myAdapter = new SqlDataAdapter(_query, connection))
                {
                    try
                    {
                        SqlCommand myCommand = new SqlCommand();
                        myCommand.Connection = connection;
                        myCommand.CommandText = _query;
                        myAdapter.UpdateCommand = myCommand;

                        return myCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.Write("Error - Connection.executeUpdateQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                        return 0;
                    }
                    finally
                    {
                        //CloseConnection();
                    }
                }
            }
                
            
        }
    }
}
