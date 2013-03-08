/********************************************************************************
** Class Name:   DataOperationActivityBase 
** Author：      Spring Yang
** Create date： 2013-3-1
** Modify：      Spring Yang
** Modify Date： 2013-3-1
** Summary：     DataOperationActivityBase class,all data table operator base class.
*********************************************************************************/


namespace WorkFlowService.DAL
{
    using System.Collections.Generic;
    using DBHelp;
    using Help;
    using IDAL;

    public abstract class DataOperationActivityBase<T> : IDataOperationActivity<T> where T : new()
    {
        #region Private Variable

        private IDBHelp _dbHelpInstance;

        #endregion

        #region Private Property

        protected IDBHelp DBHelpInstance
        {
            get
            {
                if (_dbHelpInstance == null)
                {
                    _dbHelpInstance = new SQLiteHelp
                    {
                        ConnectionString = string.Format(WFConstants.SQLiteConnectionString,
                                                         WFUntilHelp.SqliteFilePath)
                    };
                }
                return _dbHelpInstance;
            }
        }

        #endregion

        public int Insert(T entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetInsertByEntitySql(entity));
        }

        protected abstract string GetInsertByEntitySql(T entity);


        public int Modify(T entity)
        {
            return DBHelpInstance.ExecuteNonQuery(GetModifyByEntitySql(entity));
        }

        protected abstract string GetModifyByEntitySql(T entity);

        public int DeleteByID(string id)
        {
            return DBHelpInstance.ExecuteNonQuery(GetDeleteByIDSql(id));
        }

        protected abstract string GetDeleteByIDSql(string id);


        public List<T> QueryAll()
        {
            return DBHelpInstance.ReadEntityList<T>(GetQueryAllSql());
        }

        protected abstract string GetQueryAllSql();

        public T QueryByID(string id)
        {
            var entityList = DBHelpInstance.ReadEntityList<T>(GetQueryByIDSql(id));
            return entityList != null && entityList.Count > 0 ? entityList[0] : default(T);
        }

        protected abstract string GetQueryByIDSql(string id);

        public int CreateTable()
        {
            return DBHelpInstance.ExecuteNonQuery(GetCreateTableSql());
        }

        protected abstract string GetCreateTableSql();

    }
}
