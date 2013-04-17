/********************************************************************************
** Class Name:   ThreadPoolManage
** Author：      Spring Yang
** Create date： 2012-9-1
** Modify：      Spring Yang
** Modify Date： 2012-9-25
** Summary：     ThreadPoolManage class
*********************************************************************************/

namespace CommonLibrary.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Threading; using System.Threading.Tasks;

    public class ThreadPoolManage
    {
        #region Private Variable

        private Dictionary<string, object> _threadPoolDic;

        private static TaskFactory _taskFactory;

        private int _maxThreadCount;

        private static ThreadPoolManage _instance;

        #endregion

        #region Private Property

        #endregion

        #region Construct Method

        private ThreadPoolManage()
        {
            _threadPoolDic = new Dictionary<string, object>();
            _maxThreadCount = 60;
        }

        #endregion

        #region Public Property

        public static ThreadPoolManage Instance
        {
            get
            {
                if (_instance == null) _instance = new ThreadPoolManage();
                return _instance;
            }
        }

        public static TaskFactory TaskFactoryInstance
        {
            get
            {
                if (_taskFactory == null)
                    _taskFactory = new TaskFactory();
                return _taskFactory;
            }
        }

        #endregion

        #region Private Method

        private void DisposeTaskByTokenSource(Task task, CancellationTokenSource tokenSource)
        {
            tokenSource.Cancel();
            task.Wait();
            tokenSource.Dispose();
            task.Dispose();
        }

        #endregion

        #region Public Mehtod

        public void Add(string name, object thread)
        {
            bool result = false;
            while (!result)
            {
                if (_threadPoolDic.Count <= _maxThreadCount)
                {
                    if (!_threadPoolDic.ContainsKey(name))
                    {
                        _threadPoolDic.Add(name, thread);
                        result = true;
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        public object Get(string name)
        {
            if (_threadPoolDic.ContainsKey(name))
                return _threadPoolDic[name];
            return null;
        }

        public bool Remove(string name)
        {
            if (_threadPoolDic.ContainsKey(name))
                _threadPoolDic.Remove(name);
            return true;
        }

        public T TaskExecute<T>(Func<string, T> action, string parameter, int timeOut) where T : new()
        {
            var result = new T();
            if (timeOut <= 0) return action(parameter);
            var tokenSource = new CancellationTokenSource();
            var newTask = TaskFactoryInstance.StartNew(
                () =>
                {
                    result = action(parameter);
                }, tokenSource.Token);
            if (!newTask.Wait(timeOut, tokenSource.Token))
                DisposeTaskByTokenSource(newTask, tokenSource);
            return result;
        }

        #endregion

        #region Public Event

        #endregion

    }
}
