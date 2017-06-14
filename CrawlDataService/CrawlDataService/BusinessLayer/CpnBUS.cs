using CrawlDataService.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CrawlDataService.BusinessLayer
{
    class CpnBUS
    {
        private CpnDAO _cpnDao;

        public CpnBUS()
        {
            _cpnDao = new CpnDAO();
        }
        public DataTable GetAllDataNewWithStatusZero(int rowsAmount, string listFieldsToSelect)
        {
            return _cpnDao.GetAllDataNewWithStatusZero(rowsAmount, listFieldsToSelect);
        }

        public int UpdateDataAfterSuccess(string ids)
        {
            return _cpnDao.UpdateStatusByListId(ids);
        }
    }
}
