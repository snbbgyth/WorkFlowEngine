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
    public class UserGroupModelTest:TestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Mappings.UserGroup.hbm.xml" }; }
        }

        [Test]
        public void TestUserGroupModelCreateTable()
        {
            var entity = new UserGroupModel { GroupName = "GroupName", GroupDisplayName = "GroupDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            UserGroupModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<UserGroupModel>(entity.Id);
            }
            Assert.AreEqual(entity.GroupName, result.GroupName);
        }

        [Test]
        public void TestUserGroupModelModify()
        {
            var entity = new UserGroupModel { GroupName = "GroupName", GroupDisplayName = "GroupDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            UserGroupModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<UserGroupModel>(entity.Id);
                queryEntity.GroupName = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<UserGroupModel>(entity.Id);
                Assert.AreEqual(queryEntity.GroupName, result.GroupName);
            }
        }

        [Test]
        public void TestUserGroupModelDelete()
        {
            var insertEntity = new UserGroupModel { GroupName = "RoleName", GroupDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(UserGroupModel));
                var entity = crit.List<UserGroupModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<UserGroupModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestUserGroupModelQueryAll()
        {
            var insertEntity = new UserGroupModel { GroupName = "RoleName", GroupDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(UserGroupModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from UserGroupModel ").List<UserGroupModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestUserGroupModelByRoleName()
        {
            var insertEntity = new UserGroupModel { GroupName = "RoleName", GroupDisplayName = "RoleDisplayName", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                UserGroupModel entity = null;
                var resultList = session.CreateQuery("from UserGroupModel ").List<UserGroupModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(UserGroupModel)).Add(Restrictions.Eq("GroupName", entity.GroupName));
                var result = crit.List<UserGroupModel>().First();

                Assert.AreEqual(result.GroupName, entity.GroupName);
            }
        }
    }
}
