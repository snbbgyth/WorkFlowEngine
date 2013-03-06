/********************************************************************************
** Class Name:   MSSqlHelp
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     MSSqlHelp class
*********************************************************************************/

using System.Data.Common;
using System.Data.SqlClient;

namespace DBHelp
{
    public class MSSqlHelp : AbstractDBHelp
    {
        #region Protected Method

        protected override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new SqlDataAdapter(command as SqlCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
        #endregion

        #region Public Mehtod

        public override SqlSourceType DataSqlSourceType
        {
            get { return SqlSourceType.MSSql; }
        }

        public override DbParameter GetDbParameter(string key, object value)
        {
            return new SqlParameter(key, value);
        }

        public override DbParameter GetDbParameter(string key, string dateType, object value, int size = 0)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
