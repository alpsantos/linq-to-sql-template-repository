using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Model.Entities;
using Codevil.TemplateRepository.Model.Mappings;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Test.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Model.Entities
{
    [TestClass]
    public class PeopleTest : DatabaseDependentTest
    {
        [ClassInitialize]
        public static void Initialize(TestContext ignore)
        {
            EntityMappings.AddMapping(new PersonMapping());
            EntityMappings.AddMapping(new AccountMapping());
        }

        [TestMethod]
        public void EqualsTest()
        {
            var person1 = new Person();
            person1.Document = "5555";
            person1.Email = "ansdklna@adnjka.com";
            person1.Name = "Eascn ASd";
            person1.Id = 4;

            var person2 = new Person();
            person2.Document = "5555";
            person2.Email = "ansdklna@adnjka.com";
            person2.Name = "Eascn ASd";
            person2.Id = 4;

            var person3 = new Person();
            person3.Document = "5555 ";
            person3.Email = "ansdklna@adnjka.com";
            person3.Name = "Eascn ASd";
            person3.Id = 4;

            var person4 = new Person();
            person4.Document = "5555";
            person4.Email = "ansdklna@ adnjka.com";
            person4.Name = "Eascn ASd";
            person4.Id = 4;

            var person5 = new Person();
            person5.Document = "5555";
            person5.Email = "ansdklna@adnjka.com";
            person5.Name = "Eascn ASd ";
            person5.Id = 4;

            var person6 = new Person();
            person6.Document = "5555";
            person6.Email = "ansdklna@adnjka.com";
            person6.Name = "Eascn ASd";
            person6.Id = 3;

            Assert.AreEqual(person1, person1);
            Assert.AreEqual(person2, person1);
            Assert.AreNotEqual(person3, person1);
            Assert.AreNotEqual(person4, person1);
            Assert.AreNotEqual(person5, person1);
            Assert.AreNotEqual(person6, person1);
        }

        [TestMethod]
        public void SaveTest()
        {
            var account1 = new Account();
            account1.Number = 2345235;
            account1.Agency = 166;

            var owner = new Person();
            owner.Name = "Ryu Ken";
            owner.Document = "3451345";
            owner.Email = "ansjkldnas@nfjanfjk.ew";

            account1.Owner = owner;

            account1.Save();

            var account2 = new Account();
            account2.Number = 464567;
            account2.Agency = 345;

            owner.Accounts.Add(account2);

            owner.Save();

            var accountsRepository = new AccountsRepository();

            var accounts = accountsRepository.Find(a => a.PERSON.Id == owner.Id);

            Assert.IsNotNull(accounts);
            Assert.AreEqual(2, accounts.Count);
            Assert.IsTrue(accounts.Contains(account1));
            Assert.IsTrue(accounts.Contains(account2));
        }
    }
}