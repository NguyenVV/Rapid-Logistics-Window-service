
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
        public DataTable GetAllDataNewWithStatusZero()
        {
            string query = string.Format("SELECT Id,ShipmentID,MSGCODE,TOKHAIID,VAN_DON  from CPN_OutputMSG where MSGCODE is not null and TRANG_THAI =0");
           
            return conn.executeSelectQuery(query);
        }

        public int UpdateStatusByListId(string ids)
        {
            string query = string.Format("update CPN_OutputMSG set TRANG_THAI=1 where ID in("+ids+")");

            return conn.executeUpdateQuery(query);
        }
    }
}
