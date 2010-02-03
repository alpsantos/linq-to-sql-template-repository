using System.Collections.Generic;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Entities
{
    public class Person : Entity
    {
        public IList<Account> accounts;

        public Person()
        {
            AccountsRepository = new AccountsRepository();
            PeopleRepository = new PeopleRepository();
        }

        public Person(PERSON row)
            : this()
        {
            Id = row.Id;
            Name = row.Name;
            Document = row.Document;
            Email = row.Email;
        }

        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }

        public IList<Account> Accounts
        {
            get
            {
                if (accounts == null)
                {
                    accounts = AccountsRepository.Find(a => a.OwnerId == Id);
                }

                return accounts;
            }
        }

        public IRepository<PERSON, Person> PeopleRepository { get; set; }
        public IRepository<ACCOUNT, Account> AccountsRepository { get; set; }

        public void Save()
        {
            PeopleRepository.Save(this);

            foreach (var account in Accounts)
            {
                account.OwnerId = Id;

                AccountsRepository.Save(account);
            }
        }

        public override bool Equals(object obj)
        {
            var areEqual = false;

            if (obj is Person)
            {
                var that = obj as Person;

                areEqual =
                    Id == that.Id &&
                    Name == that.Name &&
                    Document == that.Document &&
                    Email == that.Email;
            }

            return areEqual;
        }
    }
}