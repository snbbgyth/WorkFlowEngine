using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;

namespace TestCommunication.NhibernateTest
{
    using WorkFlowService.Model;
    using NHibernate.DomainModel.Entities;

    public  class OperationActionInfoTest:TestCase
    {
        protected override  IList Mappings
        {
            get { return new[] { "Mappings.OperationActionInfo.hbm.xml" }; }
        }

        [Test]
        public void TestOperationActionInfoCreateTable()
        {
            var entity = new OperationActionInfoModel { ActionDisplayName = "ActionDisplayName", ActionName = "ActionDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            OperationActionInfoModel result; 
            using (var session= sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<OperationActionInfoModel>(entity.Id);
            }
            Assert.AreEqual(entity.ActionDisplayName,result.ActionDisplayName);
        }
        
        [Test]
        public void TestOperationActionInfoModify()
        {
            var entity = new OperationActionInfoModel { ActionDisplayName = "Modify", ActionName = "Modify", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            OperationActionInfoModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<OperationActionInfoModel>(entity.Id);
                queryEntity.ActionDisplayName = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<OperationActionInfoModel>(entity.Id);
                Assert.AreEqual(queryEntity.ActionDisplayName, result.ActionDisplayName);
            }
        }

        [Test]
        public void TestOperationActionInfoDelete()
        {
            var insertEntity = new OperationActionInfoModel { ActionDisplayName = "Modify", ActionName = "Modify", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
  
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(OperationActionInfoModel));
                var entity = crit.List<OperationActionInfoModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<OperationActionInfoModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestOperationActionInfoQueryAll()
        {
            var insertEntity = new OperationActionInfoModel { ActionDisplayName = "Modify", ActionName = "Modify", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };

            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(OperationActionInfoModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from OperationActionInfoModel ").List<OperationActionInfoModel>();
                Assert.AreEqual(entityList.Count,resultList.Count);
            }
        }

        [Test]
        public void TestOperationActionInfoQueryByActionName()
        {
            var insertEntity = new OperationActionInfoModel { ActionDisplayName = "ActionDisplayName", ActionName = "ActionDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };

            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                OperationActionInfoModel entity = null;
                var resultList = session.CreateQuery("from OperationActionInfoModel ").List<OperationActionInfoModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(OperationActionInfoModel)).Add(Restrictions.Eq("ActionName", entity.ActionName));
                var result = crit.List<OperationActionInfoModel>().First();
                Assert.AreEqual(result.ActionName, entity.ActionName);
            }
        }
    }
}
