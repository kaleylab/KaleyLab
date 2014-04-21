using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KaleyLab.Data;
using KaleyLab.Data.EntityFramework;
using KaleyLab.Data.EntityFrameworkSample;
using KaleyLab.Data.EntityFrameworkSample.Models;
using KaleyLab.Data.EntityFrameworkSample.Repositories;

namespace KaleyLab.Data.EntityFrameworkSampleTest
{
    [TestClass]
    public class UserDataAccessTest
    {
        private IRepositoryContext context;
        private UserRepository userRepository;

        [TestInitialize]
        public void Init()
        {         
            context = RepositoryContextBuilder.Build<EntityFrameworkRepositoryContext<EntityFameworkDbContext>>();
            userRepository = new UserRepository(context);
        }

        [TestMethod]
        public void AddUserTest()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "BingYi",
                CreatedDate = DateTime.Now
            };

            userRepository.Add(user);
            this.context.Commit();

            Assert.IsTrue(user.Id != Guid.Empty);
        }
    }
}
