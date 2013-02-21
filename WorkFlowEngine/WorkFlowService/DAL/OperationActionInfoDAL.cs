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
    using System.Collections.Generic;
    using CommonLibrary.Help;
    using DBHelp;
    using Help;
    using IDAL;
    using Model;

    public class OperationActionInfoDAL : IDataOperationActivity<OperationActionInfoModel>
    {
       public static OperationActionInfoDAL Current
       {
           get { return new OperationActionInfoDAL(); }
       }

       #region Private Variable

       private IDBHelp _dbHelpInstance;

       #endregion

       #region Private Property

       private IDBHelp DBHelpInstance
       {
           get
           {
               if (_dbHelpInstance == null)
               {
                   _dbHelpInstance = new SQLiteHelp();
                   _dbHelpInstance.ConnectionString = string.Format(WFConstants.SQLiteConnectionString,
                                                                    WFUntilHelp.SqliteFilePath);
               }
               return _dbHelpInstance;
           }
       }

       #endregion

       public int Insert(OperationActionInfoModel entity)
       {
           return DBHelpInstance.ExecuteNonQuery(GetInsertSqlByEntitySql(entity));
       }

       private string GetInsertSqlByEntitySql(OperationActionInfoModel entity)
       {
           entity.ID = Guid.NewGuid().ToString();
           return string.Format(WFConstants.InsertOperationActionInfoSqlTags, entity.ID, entity.ActionName, entity.ActionDisplayName,
                                entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
       }

       public int Modify(OperationActionInfoModel entity)
       {
           return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
       }

       private string GetModifyByEntitySql(OperationActionInfoModel entity)
       {
           return string.Format(WFConstants.InsertOrReplaceOperationActionInfoSqlTags, entity.ID, entity.ActionName, entity.ActionDisplayName, 
                                entity.CreateDateTime.ConvertSqliteDateTime(), entity.LastUpdateDateTime.ConvertSqliteDateTime(), Convert.ToInt32(entity.IsDelete));
       }

       public int DeleteByID(string id)
       {
           return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
       }

       private string GetDeleteByIDSql(string id)
       {
           return string.Format(WFConstants.DeleteOperationActionInfoByIDSqlTags, id);
       }

       public List<OperationActionInfoModel> QueryAll()
       {
           return DBHelpInstance.ReadEntityList<OperationActionInfoModel>(WFConstants.QueryAllOperationActionInfoSqlTags);
       }

       public OperationActionInfoModel QueryByID(string id)
       {
           var entityList = DBHelpInstance.ReadEntityList<OperationActionInfoModel>(GetQueryByIDSql(id));
           return entityList != null && entityList.Count > 0 ? entityList[0] : null;
       }

       private string GetQueryByIDSql(string id)
       {
           return string.Format(WFConstants.QueryOperationActionInfoByIDSqlTags, id);
       }

       public int CreateTable()
       {
           return DBHelpInstance.ExecuteNonQuery(WFConstants.CreateOperationActionInfoTableSqlTags);
       }
    }
}
