using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkFlowService.Model;

namespace WorkFlowService.IDAL
{
    public interface IUserInfoDAL : IDataOperationActivity<UserInfoModel>
    {
        string Login(string userName, string password);
        UserInfoModel QueryByUserName(string userName);
    }
}
