using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary.Help;
using WorkFlowService.Help;
using WorkFlowService.Model;

namespace WorkFlowService.NHibernateDAL
{
    public class OperationActionInfoDAL : DataOperationActivityBase<OperationActionInfoModel>
    {
        public static  OperationActionInfoDAL Current
        {
            get { return new  OperationActionInfoDAL(); }
        }
    }
}
