using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Repositories;

namespace Codevil.TemplateRepository.Model.Entities
{
    public class Account : Entity
    {
        private Person owner;

        public Account()
        {
            AccountsRepository = new AccountsRepository();
            PeopleRepository = new PeopleRepository();
        }

        public Account(ACCOUNT row)
            : this()
        {
            Id = row.Id;
            Number = row.Number;
            OwnerId = row.OwnerId;
            Agency = row.Agency;
        }

        public long Number { get; set; }
        public short Agency { get; set; }
        public int OwnerId { get; set; }

        public Person Owner
        {
            get
            {
                if (owner == null)
                {
                    owner = PeopleRepository.FindSingle(p => p.Id == OwnerId);
                }

                return owner;
            }
            set
            {
                OwnerId = value.Id;
                owner = value;
            }
        }

        public IRepository<ACCOUNT, Account> AccountsRepository { get; set; }
        public IRepository<PERSON, Person> PeopleRepository { get; set; }

        public void Save()
        {
            var transaction = DataContextFactory.CreateTransaction();

            PeopleRepository.Save(Owner, transaction);

            OwnerId = Owner.Id;

            AccountsRepository.Save(this, transaction);

            transaction.Commit();
        }

        public override bool Equals(object obj)
        {
            var areEqual = false;

            if (obj is Account)
            {
                var that = obj as Account;

                areEqual =
                    Id == that.Id &&
                    Agency == that.Agency &&
                    Number == that.Number &&
                    OwnerId == that.OwnerId;
            }

            return areEqual;
        }
    }
}