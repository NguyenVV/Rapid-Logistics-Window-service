
using System.Data;

namespace CrawlDataService.DataLayer
{
    class ShipmentDAO
    {
        private SQLConnections conn;

        /// <constructor>
        /// Constructor UserDAO
        /// </constructor>
        public ShipmentDAO()
        {
            conn = new SQLConnections();
        }

        /// <method>
        /// Get all shipmentInfo and return DataTable
        /// </method>
        public DataTable GetAllShipmentInfoWithStatusZero(int rowSelect, string listFieldsToSelect)
        {
            var str = listFieldsToSelect.Split(',');
            bool isContainId = false;
            foreach (string x in str)
            {
                if (x.Equals("ShipmentID", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    isContainId = true;
                    break;
                }
            }
            if (!isContainId)
            {
                listFieldsToSelect += ",ShipmentID";
            }

            string query = string.Format("SELECT TOP {0} {1},CONVERT(INT, CAST (CONVERT(VARCHAR(23),DateCreated,126) as datetime)) as time from ShipmentInfor where (IsSyncOms is null or IsSyncOms = 0)", rowSelect, listFieldsToSelect);
           
            return conn.executeSelectQuery(query);
        }

        /// <method>
        /// Get all shipmentInfo and return DataTable
        /// </method>
        public DataTable GetAllShipmentOutWithStatusZero(int rowSelect, string listFieldsToSelect)
        {
            var str = listFieldsToSelect.Split(',');
            bool isContainId = false;
            foreach (string x in str)
            {
                if (x.Equals("ShipmentID", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    isContainId = true;
                    break;
                }
            }
            if (!isContainId)
            {
                listFieldsToSelect += ",ShipmentID";
            }

            string query = string.Format("SELECT TOP {0} {1},CONVERT(INT, CAST (CONVERT(VARCHAR(23),DateOut,126) as datetime)) as time from ShipmentOut where (IsSyncOms is null or IsSyncOms = 0)", rowSelect, listFieldsToSelect);

            return conn.executeSelectQuery(query);
        }

        public int UpdateStatusShipmentInfoByListId(string shipmentIds)
        {
            string query = string.Format("update ShipmentInfor set IsSyncOms=1 where ShipmentId in(" + shipmentIds + ")");

            return conn.executeUpdateQuery(query);
        }

        public int UpdateStatusShipmentOutByListId(string shipmentIds)
        {
            string query = string.Format("update ShipmentOut set IsSyncOms=1 where ShipmentId in(" + shipmentIds + ")");

            return conn.executeUpdateQuery(query);
        }
    }
}
