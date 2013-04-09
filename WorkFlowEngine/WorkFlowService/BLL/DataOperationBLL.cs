/********************************************************************************
** Class Name:   DataOperationBLL 
** Author：      spring yang
** Create date： 2012-9-1
** Modify：      spring yang
** Modify Date： 2012-9-25
** Summary：     DataOperationBLL class
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using CommonLibrary.Help;
using WorkFlowService.Help;
using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
   internal  class DataOperationBLL : IDataOperationDAL
    {
        private static bool _isInitDataBase;

        public static DataOperationBLL Current
        {
            get
            {
                return new DataOperationBLL();
            }
        }

        public DataOperationBLL()
        {
            InitDataBase();
        }



        public void InitDataBase()
        {
            if (!_isInitDataBase)
            {
                try
                {
                    InitDataBaseFile();
                    InitCreateTable();
                }
                catch (Exception ex)
                {
                    LogHelp.Instance.Write(ex,MessageType.Error, GetType(),MethodBase.GetCurrentMethod().Name);
                    throw;
                }
              
                _isInitDataBase = true;
            }
        }

        private void InitDataBaseFile()
        {
            if (!File.Exists(WFUntilHelp.SqliteFilePath))
            {
                SQLiteConnection.CreateFile(WFUntilHelp.SqliteFilePath);
            }
        }

        public int Insert<T>(T entity)
        {
            return GetActivityByType<T>().Insert(entity);
        }

        public int Modify<T>(T entity)
        {
            return GetActivityByType<T>().Modify(entity);
        }

        public int Remove<T>(string id)
        {
            return GetActivityByType<T>().DeleteByID(id);
        }

        public T QueryByID<T>(string id)
        {
            return GetActivityByType<T>().QueryByID(id);
        }

        public IList<T> QueryAll<T>()
        {
            return GetActivityByType<T>().QueryAll();
        }

        private IDataOperationActivity<T> GetActivityByType<T>()
        {
            var types = GetExecutingTypes();
            var typeList =
                types.Where(t => new List<Type>(t.GetInterfaces()).Contains(typeof(IDataOperationActivity<T>))).ToList();
            typeList.Sort(new TypeNameComparer());
            return
                typeList.Select(eType => Activator.CreateInstance(eType) as IDataOperationActivity<T>).FirstOrDefault();
        }

        private void InitCreateTable()
        {
            var types = GetExecutingTypes();
            var typeList =
                types.Where(t => new List<Type>(t.GetInterfaces()).Contains(typeof(ICreateDataTableActivity)) && !t.IsInterface && !t.IsAbstract).ToList();

            typeList.ForEach(eType =>
            {
                var createDataTableActivity = Activator.CreateInstance(eType) as ICreateDataTableActivity;
                if (createDataTableActivity != null)
                    try
                    {
                        createDataTableActivity.CreateTable();
                    }
                    catch (SQLiteException ex)
                    {
                        if (ex.ErrorCode  == SQLiteErrorCode.Done)
                        {

                        }

                    }

            });
        }

        private IEnumerable<Type> GetExecutingTypes()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetTypes();
        }
    }
}
