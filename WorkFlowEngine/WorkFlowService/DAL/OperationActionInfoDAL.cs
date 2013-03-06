/********************************************************************************
** Class Name:   OperationActionInfoDAL 
** Author：      Spring Yang
** Create date： 2013-2-21
** Modify：      Spring Yang
** Modify Date： 2013-2-21
** Summary：     OperationActionInfoDAL class
*********************************************************************************/

namespace WorkFlowService.DAL
{
    using System;
    using CommonLibrary.Help;
    using Help;
    using Model;

    public class OperationActionInfoDAL : DataOperationActivityBase<OperationActionInfoModel>
    
    {
        public static OperationActionInfoDAL Current
        {
            get { return new OperationActionInfoDAL(); }
        }

        protected override string GetInsertByEntitySql(OperationActionInfoModel entity)
        {
            entity.ID = Guid.NewGuid().ToString();
     return string.Format(WFConstants.InsertOperationActionInfoSqlTags, entity.ID, entity.ActionName, entity.ActionDisplayName,
      
                          entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
      
        }

        protected override string GetModifyByEntitySql(OperationActionInfoModel entity)
        {
   return string.Format(WFConstants.InsertOrReplaceOperationActionInfoSqlTags, entity.ID, entity.ActionName, entity.ActionDisplayName, 
       
                          entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
      
        }

        protected override string GetDeleteByIDSql(string id)
        {
            return string.Format(WFConstants.DeleteOperationActionInfoByIDSqlTags, id);
        }

        protected override string GetQueryByIDSql(string id)
        {
 turn string.Format(WFConstants.QueryOperationActionInfoByIDSqlTags, id);
       }

  
        }

        protected override OperationActionInfoModel NullResult()
        {
            return null;
        }

        protected override string GetCreateTableSql()
        {
            return WFConstants.CreateOperationActionInfoTableSqlTags;
        }

        protected override string GetQueryAllSql()
        {
            return WFConstants.QueryAllOperationActionInfoSqlTags;
        }
    }
}
