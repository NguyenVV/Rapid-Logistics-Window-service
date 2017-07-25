using CrawlDataService.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CrawlDataService.BusinessLayer
{
    class ShipmentBUS
    {
        private ShipmentDAO _cpnDao;

        public ShipmentBUS()
        {
            _cpnDao = new ShipmentDAO();
        }
        public DataTable GetAllShipmentInfoWithStatusZero(int rowsAmount, string listFieldsToSelect)
        {
            return _cpnDao.GetAllShipmentInfoWithStatusZero(rowsAmount, listFieldsToSelect);
        }

        public DataTable GetAllShipmentOutWithStatusZero(int rowsAmount, string listFieldsToSelect)
        {
            return _cpnDao.GetAllShipmentOutWithStatusZero(rowsAmount, listFieldsToSelect);
        }

        public int UpdateDataAfterSuccess(string shipmentIds, int tableType)
        {
            if (tableType == 1)
            {
                return _cpnDao.UpdateStatusShipmentInfoByListId(shipmentIds);
            }

            return _cpnDao.UpdateStatusShipmentOutByListId(shipmentIds);
        }
    }
}
