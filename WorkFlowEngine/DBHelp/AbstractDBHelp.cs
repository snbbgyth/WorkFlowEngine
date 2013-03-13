/********************************************************************************
** Class Name:   AbstractDBHelp
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     Abstract DBHelp class
*********************************************************************************/


using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Data;
using System.Configuration;
using System.Threading;
using CommonLibrary.DAL;

namespace DBHelp
{
    public abstract class AbstractDBHelp : IDBHelp
    {
        #region Private Property

        private static int _currentCount;

        private int _maxConnectionCount;

        private string _connectionString;

        #endregion

        #region Private Methods
        
        private void AddConnection()
        {
            if (_currentCount < MaxConnectionCount)
                _currentCount++;
            else
            {
                while (true)
                {
                    Thread.Sleep(5);
                    if (_currentCount < MaxConnectionCount)
                    {
                        _currentCount++;
                        break;
                    }
                }
            }
        }

        private void RemoveConnection()
        {
            _currentCount--;
        }


        /// <summary>
        /// Execute query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure and parameter list</param>
        /// <param name="parameters">parameter list</param>
        /// <returns>DataSet</returns>
        private DataSet ExecuteQuery(string cmdText, List<DbParameter> parameters)
        {
            using (var conn =   GetConnection(ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    var ds = new DataSet();
                    PrepareCommand(command, conn, cmdText, parameters);
                    var da = GetDataAdapter(command);
                    da.Fill(ds);
                    return ds;
                }
            }
        }


        /// <summary>
        /// Execute non query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <param name="parameters">parameter list</param>
        /// <returns>execute count</returns>
        private int ExecuteNonQuery(string cmdText, List<DbParameter> parameters)
        {
            using (var conn = GetConnection(ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    PrepareCommand(command, conn, cmdText, parameters);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool BatchExecuteNonQuery(List<string> cmdList)
        {
            using (var conn = GetConnection(ConnectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    foreach (var cmdText in cmdList)
                    {
                        if (string.IsNullOrEmpty(cmdText)) continue;
                        using (var command =conn.CreateCommand())
                        {
                            try
                            {
                                command.CommandText = cmdText;
                                command.Transaction = transaction;
                                command.ExecuteNonQuery();
                            }
                            finally
                            {
                                command.CommandText = null;
                                command.Dispose();
                            }
                        }
                    }
                    try
                    {
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                    finally
                    {
                        transaction.Dispose();
                        conn.Dispose();
                        conn.Close();
                        cmdList.Clear();
                    }
                }
            }

        }

        /// <summary>
        /// Execute reader by store procedure and parameter list
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="parameters">parameter list</param>
        /// <param name="conn">database connection </param>
        /// <returns>data reader</returns>
        public DbDataReader ExecuteReader(string cmdText, List<DbParameter> parameters, out DbConnection conn)
        {
            conn = GetConnection(ConnectionString);
            conn.Open();
            AddConnection();
            var command = conn.CreateCommand();
            PrepareCommand(command, conn, cmdText, parameters);
            var dataReader = command.ExecuteReader();
            RemoveConnection();
            return dataReader;
        }

        /// <summary>
        /// Execute reader by store procedure and parameter list
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="parameters">parameter list</param>
        /// <returns>data reader</returns> 
        private List<T> ReadEntityList<T>(string cmdText, List<DbParameter> parameters) where T : new()
        {
            using (var conn = GetConnection(ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    PrepareCommand(command, conn, cmdText, parameters);
                    var dataReader = command.ExecuteReader();
                    return ReadEntityListByReader<T>(dataReader);
                }
            }
        }

        /// <summary>
        /// Read entity list by reader
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="reader">data reader</param>
        /// <returns>entity</returns>
        private List<T> ReadEntityListByReader<T>(DbDataReader reader) where T : new()
        {
            var listT = new List<T>();
            using (reader)
            {
                while (reader.Read())
                {
                    var fileNames = new List<string>();
                    for (int i = 0; i < reader.VisibleFieldCount; i++)
                    {
                        fileNames.Add(reader.GetName(i));
                    }
                    var inst = new T();
                    foreach (var pi in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (!fileNames.Exists(fileName => string.Compare(fileName, pi.Name, StringComparison.OrdinalIgnoreCase) == 0))
                            continue;
                        object obj;
                        try
                        {
                            obj = reader[pi.Name];
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        if (obj == DBNull.Value || obj == null)
                            continue;
                        var si = pi.GetSetMethod();
                        if (si == null)
                            continue;
                        if (pi.PropertyType == typeof(bool?))
                            pi.SetValue(inst, Convert.ToBoolean(obj), null);
                        else if (pi.PropertyType == typeof(string))
                            pi.SetValue(inst, obj.ToString(), null);
                        else if(pi.PropertyType==typeof(Int32))
                            pi.SetValue(inst, Convert.ToInt32(obj), null);
                        else if (pi.PropertyType == typeof(Int64))
                            pi.SetValue(inst, Convert.ToInt64(obj), null);
                        else if (pi.PropertyType == typeof(decimal))
                            pi.SetValue(inst, Convert.ToDecimal(obj), null);
                        else
                            pi.SetValue(inst, obj, null);
                    }
                    listT.Add(inst);
                }
            }
            return listT;
        }

        /// <summary>
        /// Get Dictionary list by string list
        /// </summary>
        /// <param name="cmdText">Store procedure</param>
        /// <param name="parameters">parameter list</param>
        /// <param name="stringlist">string list</param>
        /// <returns>result list</returns>
        private List<Dictionary<string, object>> GetDictionaryList(string cmdText, List<DbParameter> parameters, List<string> stringlist)
        {
            using (var conn =   GetConnection(ConnectionString))
            {
                AddConnection();
                using (var command =  conn.CreateCommand())
                {
                    PrepareCommand(command, conn, cmdText, parameters);
                    var dataReader = command.ExecuteReader();
                    RemoveConnection();
                    return ReadStringListByReader(dataReader, stringlist);
                }
            }
        }




        /// <summary>
        /// Read dictionary list by reader and string list
        /// </summary>
        /// <param name="reader">Db data reader</param>
        /// <param name="stringlist">string</param>
        /// <returns>result list</returns>
        private List<Dictionary<string, object>> ReadStringListByReader(DbDataReader reader, List<string> stringlist)
        {
            var listResult = new List<Dictionary<string, object>>();
            using (reader)
            {
                while (reader.Read())
                {
                    var dicResult = new Dictionary<string, object>();
                    foreach (var key in stringlist)
                    {
                        if (!stringlist.Exists(fileName => string.Compare(fileName, key, StringComparison.OrdinalIgnoreCase) == 0))
                            continue;
                        object obj;
                        try
                        {
                            obj = reader[key];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        if (obj == DBNull.Value || obj == null)
                            continue;
                        dicResult.Add(key, obj);
                    }
                    listResult.Add(dicResult);
                }
            }
            return listResult;
        }


        /// <summary>
        /// Execute scalar by store procedure and parameter list
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="parameters">parameter list</param>
        /// <returns>return value</returns>
        private object ExecuteScalar(string cmdText, List<DbParameter> parameters)
        {
            using (var conn = GetConnection(ConnectionString))
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    PrepareCommand(command, conn, cmdText, parameters);
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Prepare the execute command
        /// </summary>
        /// <param name="cmd">my sql command</param>
        /// <param name="conn">my sql connection</param>
        /// <param name="cmdText">stored procedure</param>
        /// <param name="parameters">parameter list</param>
        private void PrepareCommand(DbCommand cmd, DbConnection conn, string cmdText, List<DbParameter> parameters)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (parameters != null)
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
        }

        #endregion

        #region Public Property

        public int MaxConnectionCount
        {
            get
            {
                if (_maxConnectionCount <= 0)
                    _maxConnectionCount = 100;
                return _maxConnectionCount;
            }
            set { _maxConnectionCount = value; }
        }

        public abstract SqlSourceType DataSqlSourceType { get; }
   
        #endregion

        #region Protected Method


        protected abstract DbDataAdapter GetDataAdapter(DbCommand command);

        protected abstract DbConnection GetConnection(string connectionString);

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the connection string
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                    _connectionString =ConfigurationManager.ConnectionStrings[""].ConnectionString;
                return _connectionString;
            }
            set { _connectionString = value; }
        }

        /// <summary>
        /// Execute query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure and parameter list</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteQuery(string cmdText)
        {
            try
            {
                AddConnection();
                return ExecuteQuery(cmdText, new List<DbParameter>());
            }
            finally
            {
                RemoveConnection();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <param name="timeOut">milliseconds timeout</param>
        /// <returns>data set</returns>
        public DataSet ExecuteQuery(string cmdText, int timeOut)
        {
            return ThreadPoolManage.Instance.TaskExecute(ExecuteQuery, cmdText, timeOut);
        }

        /// <summary>
        /// Execute non query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <param name="timeOut">millisecond timeout</param>
        /// <returns>execute count</returns>
        public int ExecuteNonQuery(string cmdText, int timeOut)
        {
            return ThreadPoolManage.Instance.TaskExecute(ExecuteNonQuery, cmdText, timeOut);
        }

        /// <summary>
        /// Execute non query by stored procedure and parameter list
        /// </summary>
        /// <param name="cmdText">stored procedure</param>
        /// <returns>execute count</returns>
        public int ExecuteNonQuery(string cmdText)
        {
            try
            {
                AddConnection();
                return ExecuteNonQuery(cmdText, new List<DbParameter>());
            }
            finally
            {
                RemoveConnection();
            }
        }

        /// <summary>
        /// Execute scalar by store procedure and parameter list
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <returns>return value</returns>
        public object ExecuteScalar(string cmdText)
        {
            try
            {
                AddConnection();
                return ExecuteScalar(cmdText, new List<DbParameter>());
            }
            finally
            {
                RemoveConnection();
            }
        }

        /// <summary>
        /// Execute scalar by store procedure and parameter list
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="timeOut">millisecond time out</param>
        /// <returns>return value</returns>
        public object ExecuteScalar(string cmdText, int timeOut)
        {
            return ThreadPoolManage.Instance.TaskExecute(ExecuteScalar, cmdText, timeOut);
        }

        /// <summary>
        /// Get data base parameter by parameter name and parameter value
        /// </summary>
        /// <param name="key">parameter name</param>
        /// <param name="value">parameter value</param>
        /// <returns>my sql parameter</returns>
        public abstract DbParameter GetDbParameter(string key, object value);

        /// <summary>
        /// Get data base parameter by parameter name and parameter value
        /// and parameter direction 
        /// </summary>
        /// <param name="key">parameter name</param>
        /// <param name="value">parameter value</param>
        /// <param name="direction">parameter direction </param>
        /// <returns>data base parameter</returns>
        public DbParameter GetDbParameter(string key, object value, ParameterDirection direction)
        {
            var parameter = GetDbParameter(key, value);
            parameter.Direction = direction;
            return parameter;
        }

        public abstract DbParameter GetDbParameter(string key, string dateType, object value, int size = 0);

        /// <summary>
        /// Get Dictionary list by string list
        /// </summary>
        /// <param name="cmdText">Store procedure</param>
        /// <param name="stringlist">string list</param>
        /// <returns>result list</returns>
        public List<Dictionary<string, object>> GetDictionaryList(string cmdText, List<string> stringlist)
        {
            return GetDictionaryList(cmdText, new List<DbParameter>(), stringlist);
        }

        /// <summary>
        /// Execute reader by store procedure
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <returns>data reader</returns> 
        public List<T> ReadEntityList<T>(string cmdText) where T : new()
        {
            try
            {
                AddConnection();
                return ReadEntityList<T>(cmdText, new List<DbParameter>());
            }
            finally
            {
                RemoveConnection();
            }
        }

        /// <summary>
        /// Execute reader by store procedure
        /// </summary>
        /// <param name="cmdText">store procedure</param>
        /// <param name="timeOut">millisecond timeout </param>
        /// <returns>data reader</returns> 
        public List<T> ReadEntityList<T>(string cmdText, int timeOut) where T : new()
        {
            return ThreadPoolManage.Instance.TaskExecute(ReadEntityList<T>, cmdText, timeOut);
        }

        #endregion
    }
}
