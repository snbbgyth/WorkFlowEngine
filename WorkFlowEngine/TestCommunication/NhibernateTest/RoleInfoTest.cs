using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.DomainModel.Entities;
using NUnit.Framework;

namespace TestCommunication.NhibernateTest
{
    using WorkFlowService.Model;
   public class RoleInfoModelTest:TestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Mappings.RoleInfo.hbm.xml" }; }
        }

        [Test]
        public void TestRoleInfoModelCreateTable()
        {
            var entity = new RoleInfoModel { RoleName = "RoleName", RoleDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            RoleInfoModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<RoleInfoModel>(entity.Id);
            }
            Assert.AreEqual(entity.RoleName, result.RoleName);
        }

        [Test]
        public void TestRoleInfoModelModify()
        {
            var entity = new RoleInfoModel { RoleName = "RoleName", RoleDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            RoleInfoModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<RoleInfoModel>(entity.Id);
                queryEntity.RoleName = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<RoleInfoModel>(entity.Id);
                Assert.AreEqual(queryEntity.RoleName, result.RoleName);
            }
        }

        [Test]
        public void TestRoleInfoModelDelete()
        {
            var insertEntity = new RoleInfoModel { RoleName = "RoleName", RoleDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(RoleInfoModel));
                var entity = crit.List<RoleInfoModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<RoleInfoModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestRoleInfoModelQueryAll()
        {
            var insertEntity = new RoleInfoModel { RoleName = "RoleName", RoleDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(RoleInfoModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from RoleInfoModel ").List<RoleInfoModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestRoleInfoModelByRoleName()
        {
            var insertEntity = new RoleInfoModel { RoleName = "RoleName", RoleDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                RoleInfoModel entity = null;
                var resultList = session.CreateQuery("from RoleInfoModel ").List<RoleInfoModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(RoleInfoModel)).Add(Restrictions.Eq("RoleName", entity.RoleName));
                var result = crit.List<RoleInfoModel>().First();

                Assert.AreEqual(result.RoleName, entity.RoleName);
            }
        }
    }
}
