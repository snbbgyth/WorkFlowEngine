/********************************************************************************
** Class Name:   IDBHelp
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     IDBHelp interface class
*********************************************************************************/

using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DBHelp
{
    public interface IDBHelp
    {
        /// <summary>
        /// Gets the connection string
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the max connection count
        /// </summary>
        int MaxConnectionCount { get; set; }

        /// <summary>
        /// Gets or sets the sql source type
        /// </summary>
        SqlSourceType DataSqlSourceType { get; }

        /// <summary>
        /// Execute query by stored procedure 
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteQuery(string cmdText);

        /// <summary>
        /// Execute query by stored procedure 
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <param name="timeOut">millisecond time out</param>
        /// <returns>DataSet</returns>
        DataSet ExecuteQuery(string cmdText, int timeOut);

        /// <summary>
        /// Execute non query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <returns>execute count</returns>
        int ExecuteNonQuery(string cmdText);

        /// <summary>
        /// Execute non query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <param name="timeOut">millisecond time out</param>
        /// <returns>execute count</returns>
        int ExecuteNonQuery(string cmdText, int timeOut);

        /// <summary>
        /// Execute scalar by store procedure
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <returns>return value</returns>
        object ExecuteScalar(string cmdText);

        /// <summary>
        /// Execute scalar by store procedure
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="timeOut">millisecond time out</param>
        /// <returns>return value</returns>
        object ExecuteScalar(string cmdText, int timeOut);

        /// <summary>
        /// Get data base parameter by parameter name and parameter value
        /// </summary>
        /// <param name="key">parameter name</param>
        /// <param name="value">parameter value</param>
        /// <returns>sql parameter</returns>
        DbParameter GetDbParameter(string key, object value);

        /// <summary>
        /// Get data base parameter by parameter name and parameter value
        /// and parameter direction 
        /// </summary>
        /// <param name="key">parameter name</param>
        /// <param name="value">parameter value</param>
        /// <param name="direction">parameter direction </param>
        /// <returns>data base parameter</returns>
        DbParameter GetDbParameter(string key, object value, ParameterDirection direction);

        /// <summary>
        /// Read entity list by  store procedure
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="cmdText">store procedure</param>
        /// <returns>entity list</returns>
        List<T> ReadEntityList<T>(string cmdText) where T : new();

        /// <summary>
        /// Read entity list by  store procedure
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="cmdText">store procedure</param>
        /// <param name="timeOut">millisecond time out</param>
        /// <returns>entity list</returns>
        List<T> ReadEntityList<T>(string cmdText, int timeOut) where T : new();


        /// <summary>
        /// Get dictionary result by store procedure and parameters and string list
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="stringlist">string list</param>
        /// <returns>result list</returns>
        List<Dictionary<string, object>> GetDictionaryList(string cmdText,
                                                           List<string> stringlist);

        /// <summary>
        /// Batch execute ExecuteNonQuery by cmdText list
        /// </summary>
        /// <param name="cmdList">cmd text list</param>
        /// <returns>execute true or not</returns>
        bool BatchExecuteNonQuery(List<string> cmdList);
    }

}
