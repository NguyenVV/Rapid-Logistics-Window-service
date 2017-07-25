using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CrawlDataService
{
    public class SQLConnections
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;
        private static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        /// <constructor>
        /// Initialise Connection
        /// </constructor>
        public SQLConnections()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(connStr);
        }

        /// <method>
        /// Open Database Connection if Closed or Broken
        /// </method>
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State ==
                        ConnectionState.Broken)
            {
                conn.Open();
            }

            return conn;
        }
        
        ///// <summary>
        ///// Close Connection
        ///// </summary>
        //private void CloseConnection()
        //{
        //    if (conn.State == ConnectionState.Open || conn.State ==
        //                ConnectionState.Connecting)
        //    {
        //        conn.Close();
        //    }
        //}

        /// <method>
        /// Select Query
        /// </method>
        public DataTable executeSelectQueryWithListParam(String _query, List<KeyValuePair<string, object>> paramList = null)
        {
            SqlParameter[] sqlParameter = getParameter(paramList);
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                {
                    myCommand.Parameters.AddRange(sqlParameter);
                }
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {
                //CloseConnection();
            }
            return dataTable;
        }

        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter = null)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if(sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {
                //CloseConnection();
            }
            return dataTable;
        }

        /// <method>
        /// Execute stored proceduce to select data
        /// </method>
        public DataTable executeStoredProceduceSelectData(string procName, List<KeyValuePair<string, object>> paramList = null)
        {
            SqlParameter[] sqlParameter = getParameter(paramList);
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr)) {
                    using (SqlCommand myCommand = new SqlCommand(procName, conn))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        if (sqlParameter != null && sqlParameter.Any())
                        {
                            myCommand.Parameters.AddRange(sqlParameter);
                        }
                        myCommand.ExecuteNonQuery();
                        myAdapter.SelectCommand = myCommand;
                        myAdapter.Fill(ds);
                        dataTable = ds.Tables[0];
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeProceduce - ProcName: " + procName + " \nException: " + e.StackTrace.ToString());
                return null;
            }

            return dataTable;
        }

        private SqlParameter[] getParameter(List<KeyValuePair<string, object>> paramList)
        {
            if (paramList == null || paramList.Count == 0)
                return null;

            SqlParameter[] result = new SqlParameter[paramList.Count];

            for ( int i = 0; i < paramList.Count; i++) {
                SqlParameter sqlParam;
                if (paramList[i].Value is string)
                {
                    sqlParam = new SqlParameter(paramList[i].Key, SqlDbType.NVarChar);
                    sqlParam.Value = paramList[i].Value.ToString();
                }else if(paramList[i].Value is Int32)
                {
                    sqlParam = new SqlParameter(paramList[i].Key, SqlDbType.Int);
                    sqlParam.Value = paramList[i].Value;
                }
                else if (paramList[i].Value is double)
                {
                    sqlParam = new SqlParameter(paramList[i].Key, SqlDbType.Float);
                    sqlParam.Value = paramList[i].Value;
                }
                else if (paramList[i].Value is DateTime)
                {
                    sqlParam = new SqlParameter(paramList[i].Key, SqlDbType.DateTime);
                    sqlParam.Value = paramList[i].Value;
                }
                else
                {
                    sqlParam = new SqlParameter(paramList[i].Key, paramList[i].Value);
                }

                result[i] = sqlParam;
            }

            return result;
        }
        /// <method>
        /// Execute stored proceduce to select data
        /// </method>
        public bool executeStoredProceduce(string procName, List<KeyValuePair<string, object>> paramList = null)
        {
            SqlParameter[] sqlParameter = getParameter(paramList);

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand myCommand = new SqlCommand(procName, conn))
                    {
                        myCommand.CommandType = CommandType.StoredProcedure;
                        conn.Open();

                        if (sqlParameter != null && sqlParameter.Any())
                        {
                            myCommand.Parameters.AddRange(sqlParameter);
                        }

                        bool result = myCommand.ExecuteNonQuery() > 0;
                        conn.Close();
                        conn.Dispose();
                        return result;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeNonQuery \nException: " + e.StackTrace.ToString());
            }

            return false;
        }

        /// <method>
        /// Insert Query
        /// </method>
        public bool executeInsertQuery(String _query, List<KeyValuePair<string, object>> paramList = null)
        {
            SqlParameter[] sqlParameter = getParameter(paramList);
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.Write("Error - Connection.executeInsertQuery - Query: " + _query + " \nException: \n" + e.StackTrace.ToString());
                return false;
            }
            finally
            {
                //CloseConnection();
            }
            return true;
        }

        /// <method>
        /// Update Query
        /// </method>
        public int executeUpdateQuery(String _query, List<KeyValuePair<string, object>> paramList = null)
        {
            SqlParameter[] sqlParameter = getParameter(paramList);
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                if (sqlParameter != null)
                    myCommand.Parameters.AddRange(sqlParameter);
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
