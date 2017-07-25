
using System.Data;

namespace CrawlDataService.DataLayer
{
    class CpnDAO
    {
        private SQLConnections conn;

        /// <constructor>
        /// Constructor UserDAO
        /// </constructor>
        public CpnDAO()
        {
            conn = new SQLConnections();
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
           DataTable result = conn.executeSelectQuery(query);

            return result;
        }

        public int UpdateStatusByListId(string ids)
        {
            string query = string.Format("update CPN_OutputMSG set TRANG_THAI=1 where ID in("+ids+")");

            return conn.executeUpdateQuery(query);
        }
    }
}
