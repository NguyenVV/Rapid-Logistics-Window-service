using CrowlData.DataLayer;
using CrowlData.ValueObject;
using System;
using System.Collections.Generic;
using System.Data;

namespace CrowlData.BusinessLayer
{
    /// <summary>
    /// Summary description for UserBUS
    /// </summary>
    public class UserBUS
    {
        private UserDAO _userDAO;

        /// <constructor>
        /// Constructor UserBUS
        /// </constructor>
        public UserBUS()
        {
            //_userDAO = new UserDAO();
        }

        /// <method>
        /// Get User Email By Firstname or Lastname and return VO
        /// </method>
        public UserVO getUserEmailByName(string name)
        {
            UserVO userVO = new UserVO();
            DataTable dataTable = new DataTable();

            dataTable = _userDAO.searchByName(name);

            foreach (DataRow dr in dataTable.Rows)
            {
                userVO.idUser = Int32.Parse(dr["t01_id"].ToString());
                userVO.firstname = dr["t01_firstname"].ToString();
                userVO.lastname = dr["t01_lastname"].ToString();
                userVO.email = dr["t01_email"].ToString();
            }
            return userVO;
        }

        /// <method>
        /// Get User Email By Id and return DataTable
        /// </method>
        public UserVO getUserById(string _id)
        {
            UserVO userVO = new UserVO();
            DataTable dataTable = new DataTable();
            dataTable = _userDAO.searchById(_id);

            foreach (DataRow dr in dataTable.Rows)
            {
                userVO.idUser = Int32.Parse(dr["t01_id"].ToString());
                userVO.firstname = dr["t01_firstname"].ToString();
                userVO.lastname = dr["t01_lastname"].ToString();
                userVO.email = dr["t01_email"].ToString();
            }
            return userVO;
        }

        /// <method>
        /// Get all User and return UserVO
        /// </method>
        public List<UserVO> getAllUser()
        {
            List<UserVO> listData = new List<UserVO>();
            DataTable dataTable = new DataTable();
            dataTable = _userDAO.getAll();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    UserVO userVO = new UserVO();
                    userVO.idUser = Int32.Parse(dr["MaNV"].ToString());
                    userVO.firstname = dr["HoTen"].ToString();
                    userVO.lastname = dr["TenDN"].ToString();
                    userVO.email = dr["Quyen"].ToString();
                    listData.Add(userVO);
                }
            }

            return listData;
        }
    }
}
