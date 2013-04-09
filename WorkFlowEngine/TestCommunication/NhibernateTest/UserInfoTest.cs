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
    public class UserInfoModelTest:TestCase
    {
        protected override IList Mappings
        {
            get { return new[] { "Mappings.UserInfo.hbm.xml" }; }
        }

        [Test]
        public void TestUserInfoModelCreateTable()
        {
            var entity = new UserInfoModel { UserName = "UserName", UserDisplayName = "UserDisplayName", Password = "Password", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            UserInfoModel result;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                result = session.Get<UserInfoModel>(entity.Id);
            }
            Assert.AreEqual(entity.UserName, result.UserName);
        }

        [Test]
        public void TestUserInfoModelModify()
        {
            var entity = new UserInfoModel { UserName = "UserName", UserDisplayName = "UserDisplayName", Password = "Password", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            UserInfoModel queryEntity;
            using (var session = sessions.OpenSession())
            {
                session.Save(entity);
                session.Flush();
                queryEntity = session.Get<UserInfoModel>(entity.Id);
                queryEntity.UserName = "Modified";
                session.SaveOrUpdate(queryEntity);
                session.Flush();
                var result = session.Get<UserInfoModel>(entity.Id);
                Assert.AreEqual(queryEntity.UserName, result.UserName);
            }
        }

        [Test]
        public void TestUserInfoModelDelete()
        {
            var insertEntity = new UserInfoModel { UserName = "UserName", UserDisplayName = "UserDisplayName", Password = "Password", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(UserInfoModel));
                var entity = crit.List<UserInfoModel>().First();
                session.Delete(entity);
                session.Flush();
                var result = session.Get<UserInfoModel>(entity.Id);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void TestUserInfoModelQueryAll()
        {
            var insertEntity = new UserInfoModel { UserName = "UserName", UserDisplayName = "UserDisplayName", Password = "Password", CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                ICriteria crit = session.CreateCriteria(typeof(UserInfoModel));
                var entityList = crit.List();
                var resultList = session.CreateQuery("from UserInfoModel ").List<UserInfoModel>();
                Assert.AreEqual(entityList.Count, resultList.Count);
            }
        }

        [Test]
        public void TestUserInfoModelByRoleName()
        {
            var insertEntity = new UserInfoModel { UserName = "UserName", UserDisplayName = "UserDisplayName",Password = "Password",CreateDateTime = DateTime.Now, Id = Guid.NewGuid().ToString(), LastUpdateDateTime = DateTime.Now };
            using (var session = sessions.OpenSession())
            {
                session.Save(insertEntity);
                session.Flush();
                UserInfoModel entity = null;
                var resultList = session.CreateQuery("from UserInfoModel ").List<UserInfoModel>();
                if (resultList.Count > 0)
                    entity = resultList.FirstOrDefault();
                ICriteria crit = session.CreateCriteria(typeof(UserInfoModel)).Add(Restrictions.Eq("UserName", entity.UserName));
                var result = crit.List<UserInfoModel>().First();
                Assert.AreEqual(result.UserName, entity.UserName);
            }
        }
    }
}
