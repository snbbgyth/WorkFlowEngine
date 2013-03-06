/********************************************************************************
** Class Name:   OracleHelp
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     OracleHelp class
*********************************************************************************/

using System.Data.Common;
using Oracle.DataAccess.Client;

namespace DBHelp
{
    public class OracleHelp : AbstractDBHelp
    {
        #region Protected Method

        protected override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new OracleDataAdapter(command as OracleCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }

        #endregion

        #region Public Mehtod

        public override DbParameter GetDbParameter(string key, object value)
        {
            return new OracleParameter(key, value);
        }

        public override DbParameter GetDbParameter(string key, string dateType, object value, int size = 0)
        {
            throw new System.NotImplementedException();
        }

        public override SqlSourceType DataSqlSourceType
        {
            get { return SqlSourceType.Oracle; }
        }

        #endregion

    }
}
