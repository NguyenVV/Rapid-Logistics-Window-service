using CrawlDataService;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace CrowlData.DataLayer
{
    class CommonDataDAO
    {
        private SQLConnections conn;

        /// <constructor>
        /// Constructor CommonDataDAO
        /// </constructor>
        public CommonDataDAO()
        {
            conn = new SQLConnections();
        }

        /// <method>
        /// Get all User and return DataTable
        /// </method>
        public DataTable getOneRowDataByProcName(string procName, List<KeyValuePair<string, object>> paramList)
        {
            return conn.executeStoredProceduceSelectData(procName, paramList);
        }

        public bool updateDataByProcName(string procName, List<KeyValuePair<string,object>> paramList)
        {
            return conn.executeStoredProceduce(procName, paramList);
        }
    }
}
