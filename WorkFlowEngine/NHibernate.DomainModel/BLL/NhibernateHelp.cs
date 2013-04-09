using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;

namespace NHibernate.DomainModel.BLL
{
    internal class NhibernateHelp
    {
        private ISessionFactory _sessionFactory;

        private NhibernateHelp()
        {
            _sessionFactory = GetSessionFactory();
        }

        private static NhibernateHelp _instance;
        private static readonly object SyncObject = new object();

        public static NhibernateHelp Instance
        {
            get
            {
                lock (SyncObject)
                {
                    if (_instance == null)
                        _instance = new NhibernateHelp();
                }
                return _instance;
            }
        }

        private ISessionFactory GetSessionFactory()
        {
            return (new Configuration()).Configure().BuildSessionFactory();
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

    }
}
