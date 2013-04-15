using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.Engine;
using NHibernate.Tool.hbm2ddl;

namespace NHibernate.DomainModel.BLL
{
    public class NhibernateHelp
    {
        private ISessionFactory _sessionFactory;

        private NhibernateHelp()
        {
            _sessionFactory = GetSessionFactory();
        }

        private static NhibernateHelp _instance;
        private static readonly object SyncObject = new object();
        private const bool OutputDdl = true;
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

        private Configuration _configuration;

        private ISessionFactory GetSessionFactory()
        {
            return Cfg.BuildSessionFactory();
        }

        private static readonly object syncCfg = new object();

        private Configuration Cfg
        {
            get
            {
                lock (syncCfg)
                {
                    if (_configuration == null)
                    {
                        _configuration = (new Configuration()).Configure();

                        Assembly assembly = Assembly.Load("NHibernate.DomainModel");
                        _configuration.AddAssembly(assembly);
                        UpdateSchema();
                    }
                }
                return _configuration;
            }
        }

        public ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        public void CreateSchema()
        {
            new SchemaExport(Cfg).Create(OutputDdl, true);
        }

        protected void CreateDatabaseSchema()
        {
            new SchemaExport(Cfg).Execute(OutputDdl, true, false);
 
        }

        public void UpdateSchema()
        {
            new SchemaUpdate(Cfg).Execute(OutputDdl, true);
        }


        protected void DropSchema()
        {
            new SchemaExport(Cfg).Drop(OutputDdl, true);
        }

    }
}
