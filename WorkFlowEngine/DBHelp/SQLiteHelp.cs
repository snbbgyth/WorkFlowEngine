/********************************************************************************
** Class Name:   SQLiteHelp
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     SQLiteHelp class
*********************************************************************************/

using System.Data.Common;
using System.Data.SQLite;

namespace DBHelp
{
    public class SQLiteHelp : AbstractDBHelp
    {
        #region Protected Method

        protected override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new SQLiteDataAdapter(command as SQLiteCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }

        #endregion

        #region Public Mehtod

        public override DbParameter GetDbParameter(string key, object value)
        {
            return new SQLiteParameter(key, value);
        }

        public override DbParameter GetDbParameter(string key, string dateType, object value, int size = 0)
        {
            throw new System.NotImplementedException();
        }

        public override SqlSourceType DataSqlSourceType
        {
            get { return SqlSourceType.SQLite; }
        }

        #endregion
    }
}
