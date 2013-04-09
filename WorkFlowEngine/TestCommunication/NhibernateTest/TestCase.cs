using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Dialect.Schema;
using NHibernate.Engine;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Mapping;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Type;
using NUnit.Framework;

namespace TestCommunication.NhibernateTest
{
    public abstract class TestCase
    {
        private const bool OutputDdl = false;
        protected Configuration cfg;
        protected ISessionFactoryImplementor sessions;



        protected Dialect Dialect
        {
            get { return Dialect.GetDialect(cfg.Properties); }
        }


        /// <summary>
        /// To use in in-line test
        /// </summary>
        protected bool IsAntlrParser
        {
            get
            {
                return sessions.Settings.QueryTranslatorFactory is ASTQueryTranslatorFactory;
            }
        }

        protected ISession lastOpenedSession;


        /// <summary>
        /// Mapping files used in the TestCase
        /// </summary>
        protected abstract IList Mappings { get; }

        /// <summary>
        /// Assembly to load mapping files from (default is NHibernate.DomainModel).
        /// </summary>
        protected virtual string MappingsAssembly
        {
            get { return "NHibernate.DomainModel"; }
        }

        static TestCase()
        {
            // Configure log4net here since configuration through an attribute doesn't always work.

        }

        /// <summary>
        /// Creates the tables used in this TestCase
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            try
            {
                Configure();
                if (!AppliesTo(Dialect))
                {
                    Assert.Ignore(GetType() + " does not apply to " + Dialect);
                }

                CreateSchema();
                try
                {
                    BuildSessionFactory();
                    if (!AppliesTo(sessions))
                    {
                        Assert.Ignore(GetType() + " does not apply with the current session-factory configuration");
                    }
                }
                catch
                {
                    DropSchema();
                    throw;
                }
            }
            catch (Exception e)
            {
                Cleanup();

                throw;
            }
        }

        /// <summary>
        /// Removes the tables used in this TestCase.
        /// </summary>
        /// <remarks>
        /// If the tables are not cleaned up sometimes SchemaExport runs into
        /// Sql errors because it can't drop tables because of the FKs.  This 
        /// will occur if the TestCase does not have the same hbm.xml files
        /// included as a previous one.
        /// </remarks>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // If TestFixtureSetup fails due to an IgnoreException, it will still run the teardown.
            // We don't want to try to clean up again since the setup would have already done so.
            // If cfg is null already, that indicates it's already been cleaned up and we needn't.
            if (cfg != null)
            {
                if (!AppliesTo(Dialect))
                    return;

                DropSchema();
                Cleanup();
            }
        }

        protected virtual void OnSetUp()
        {
        }

        /// <summary>
        /// Set up the test. This method is not overridable, but it calls
        /// <see cref="OnSetUp" /> which is.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            OnSetUp();
        }

        protected virtual void OnTearDown()
        {
        }

        /// <summary>
        /// Checks that the test case cleans up after itself. This method
        /// is not overridable, but it calls <see cref="OnTearDown" /> which is.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            OnTearDown();

            bool wasClosed = CheckSessionWasClosed();
            bool wasCleaned = CheckDatabaseWasCleaned();
            bool wereConnectionsClosed = CheckConnectionsWereClosed();
            bool fail = !wasClosed || !wasCleaned || !wereConnectionsClosed;

            if (fail)
            {
                //Assert.Fail("Test didn't clean up after itself. session closed: " + wasClosed + " database cleaned: " + wasCleaned
                //    + " connection closed: " + wereConnectionsClosed);
            }
        }

        private bool CheckSessionWasClosed()
        {
            if (lastOpenedSession != null && lastOpenedSession.IsOpen)
            {

                lastOpenedSession.Close();
                return false;
            }

            return true;
        }

        protected virtual bool CheckDatabaseWasCleaned()
        {
            if (sessions.GetAllClassMetadata().Count == 0)
            {
                // Return early in the case of no mappings, also avoiding
                // a warning when executing the HQL below.
                return true;
            }

            bool empty;
            using (ISession s = sessions.OpenSession())
            {
                IList objects = s.CreateQuery("from System.Object o").List();
                empty = objects.Count == 0;
            }

            if (!empty)
            {

                DropSchema();
                CreateSchema();
            }

            return empty;
        }

        private bool CheckConnectionsWereClosed()
        {

            return false;
        }

        private void Configure()
        {
            cfg = TestConfigurationHelper.GetDefaultConfiguration();

            AddMappings(cfg);

            Configure(cfg);

            ApplyCacheSettings(cfg);
        }

        protected virtual void AddMappings(Configuration configuration)
        {
            Assembly assembly = Assembly.Load(MappingsAssembly);

            foreach (string file in Mappings)
            {
                configuration.AddResource(MappingsAssembly + "." + file, assembly);
            }
        }

        protected virtual void CreateSchema()
        {
            //new SchemaExport(cfg).Create(OutputDdl, true);
            //new SchemaExport(cfg).Create(OutputDdl, true);
            new SchemaUpdate(cfg).Execute(OutputDdl,true);
        }

        private IConnectionHelper connectionHelper;

        //todo: create table if not exist.
        protected bool tableExists(String tableName)
        {
            Boolean result = false;

            var dialect = Dialect.GetDialect(cfg.Properties);
            IDictionary<string, string> props = new Dictionary<string, string>(dialect.DefaultProperties);
            foreach (var prop in cfg.Properties)
            {
                props[prop.Key] = prop.Value;
            }
            connectionHelper = new ManagedProviderConnectionHelper(props);
            var connection = connectionHelper.Connection;
            var meta = new DatabaseMetadata(connection, dialect);
          
            //ITableMetadata tableInfo = meta.GetTableMetadata(
            //            table.Name,
            //            table.Schema ?? defaultSchema,
            //            table.Catalog ?? defaultCatalog,
            //            table.IsQuoted);
            //if (tableInfo == null)
            //    throw new HibernateException("Missing table: " + table.Name);
            //else
            //    table.ValidateColumns(dialect, mapping, tableInfo);
            //if (connection != null)
            //{
            //    var tables = meta.GetTableMetadata();
            //    while (tables.next())
            //    {
            //        String currentTableName = tables.getString("TABLE_NAME");
            //        if (currentTableName.equals(tableName))
            //        {
            //            result = true;
            //        }
            //    }
            //    tables.close();
            //}  
            return result;
        }


        public void ValidateSchema(Configuration config)
        {
            new SchemaValidator(config).Validate();
        }

        protected virtual void DropSchema()
        {
             new SchemaExport(cfg).Drop(OutputDdl, true);
        }

        protected virtual void BuildSessionFactory()
        {
            sessions = (ISessionFactoryImplementor)cfg.BuildSessionFactory();

        }

        private void Cleanup()
        {
            if (sessions != null)
            {
                sessions.Close();
            }
            sessions = null;

            lastOpenedSession = null;
            cfg = null;
        }

        public int ExecuteStatement(string sql)
        {

            using (IConnectionProvider prov = ConnectionProviderFactory.NewConnectionProvider(cfg.Properties))
            {
                IDbConnection conn = prov.GetConnection();

                try
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    using (IDbCommand comm = conn.CreateCommand())
                    {
                        comm.CommandText = sql;
                        comm.Transaction = tran;
                        comm.CommandType = CommandType.Text;
                        int result = comm.ExecuteNonQuery();
                        tran.Commit();
                        return result;
                    }
                }
                finally
                {
                    prov.CloseConnection(conn);
                }
            }
        }

        public int ExecuteStatement(ISession session, ITransaction transaction, string sql)
        {
            using (IDbCommand cmd = session.Connection.CreateCommand())
            {
                cmd.CommandText = sql;
                if (transaction != null)
                    transaction.Enlist(cmd);
                return cmd.ExecuteNonQuery();
            }
        }

        protected ISessionFactoryImplementor Sfi
        {
            get { return sessions; }
        }

        protected virtual ISession OpenSession()
        {
            lastOpenedSession = sessions.OpenSession();
            return lastOpenedSession;
        }

        protected virtual ISession OpenSession(IInterceptor sessionLocalInterceptor)
        {
            lastOpenedSession = sessions.OpenSession(sessionLocalInterceptor);
            return lastOpenedSession;
        }

        protected virtual void ApplyCacheSettings(Configuration configuration)
        {
            if (CacheConcurrencyStrategy == null)
            {
                return;
            }

            foreach (PersistentClass clazz in configuration.ClassMappings)
            {
                bool hasLob = false;
                foreach (Property prop in clazz.PropertyClosureIterator)
                {
                    if (prop.Value.IsSimpleValue)
                    {
                        IType type = ((SimpleValue)prop.Value).Type;
                        if (type == NHibernateUtil.BinaryBlob)
                        {
                            hasLob = true;
                        }
                    }
                }
                if (!hasLob && !clazz.IsInherited)
                {
                    configuration.SetCacheConcurrencyStrategy(clazz.EntityName, CacheConcurrencyStrategy);
                }
            }

            foreach (var coll in configuration.CollectionMappings)
            {
                configuration.SetCollectionCacheConcurrencyStrategy(coll.Role, CacheConcurrencyStrategy);
            }
        }

        #region Properties overridable by subclasses

        protected virtual bool AppliesTo(Dialect dialect)
        {
            return true;
        }

        protected virtual bool AppliesTo(ISessionFactoryImplementor factory)
        {
            return true;
        }

        protected virtual void Configure(Configuration configuration)
        {
        }

        protected virtual string CacheConcurrencyStrategy
        {
            get { return "nonstrict-read-write"; }
            //get { return null; }
        }

        #endregion
    }
}
