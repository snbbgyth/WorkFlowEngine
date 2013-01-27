using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.IDAL;
using WorkFlowService.Model;

namespace WorkFlowService.DAL
{
    public class UserInfoDAL : IDataOperationActivity<UserInfoModel>
    {
        public int Insert(UserInfoModel entity)
        {
            throw new NotImplementedException();
        }

        public int Modify(UserInfoModel entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<UserInfoModel> QueryAll()
        {
            throw new NotImplementedException();
        }

        public UserInfoModel QueryByID(string id)
        {
            throw new NotImplementedException();
        }

        public int CreateTable()
        {
            throw new NotImplementedException();
        }
    }
}
