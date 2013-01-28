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
using System.Text;
usHelp;
using WorkFlowService.IDAL;

namespace WorkFlowService.BLL
{
    public class DataOperationBLL
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
            if (!_isInitDataBase) InitDataBase();
        }

        private void InitDataBase()
        {
 ublic     InitDataBaseFile();
            InitCreateTable();
            _isInitDataBase = true;
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

        public T QueryByEID<T>(string id)
        {
            return GetActivityByType<T>().QueryByID(id);
        }

        public List<T> QueryAll<T>()
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
                types.Where(t => new List<Type>(t.GetInterfaces()).Contains(typeof(ICreateDataTableActivity)) && !t.IsInterface).ToList();

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
                        if (ex.ErrorCode == 1) return;

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
