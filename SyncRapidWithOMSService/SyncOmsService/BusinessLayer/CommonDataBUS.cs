using CrowlData.DataLayer;
using System.Collections.Generic;
using System.Data;

namespace CrowlData.BusinessLayer
{
    class CommonDataBUS
    {
        private CommonDataDAO _commonDAO;

        public CommonDataBUS()
        {
            _commonDAO = new CommonDataDAO();
        }

        public DataTable getOneRowDataByProcName(string procName, List<KeyValuePair<string, object>> paramList = null)
        {
            return _commonDAO.getOneRowDataByProcName(procName, paramList);
        }

        public bool updateDataByProcName(string procName, List<KeyValuePair<string, object>> paramList)
        {
            return _commonDAO.updateDataByProcName(procName, paramList);
        }
    }
}
