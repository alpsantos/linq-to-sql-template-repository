﻿using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Repositories;
using Codevil.TemplateRepository.Repositories;
using Codevil.TemplateRepository.Controllers;
using Codevil.TemplateRepository.Data.Factories;
using Codevil.TemplateRepository.Factories;

namespace Codevil.TemplateRepository.Model.Entities
{
    public class Account : Entity
    {
        public long Number { get; set; }
        public short Agency { get; set; }
        public int OwnerId { get; set; }
        private Person owner;
        public Person Owner
        {
            get
            {
                if (owner == null)
                {
                    this.owner = this.PeopleRepository.FindSingle(p => p.Id == this.OwnerId);
                }

                return this.owner;
            }
            set
            {
                this.OwnerId = value.Id;
                this.owner = value;
            }
        }
        public IRepository<ACCOUNT, Account> AccountsRepository { get; set; }
        public IRepository<PERSON, Person> PeopleRepository { get; set; }

        public Account()
            : base()
        {
            this.AccountsRepository = new AccountsRepository();
            this.PeopleRepository = new PeopleRepository();
        }

        public Account(ACCOUNT row)
            : this()
        {
            this.Id = row.Id;
            this.Number = row.Number;
            this.OwnerId = row.OwnerId;
            this.Agency = row.Agency;
        }

        public void Save()
        {
            UnitOfWork unitOfWork = this.DataContextFactory.CreateUnitOfWork();
            
            this.PeopleRepository.Save(this.Owner, unitOfWork);

            this.OwnerId = this.Owner.Id;

            this.AccountsRepository.Save(this, unitOfWork);

            unitOfWork.Commit();
        }

        public override bool Equals(object obj)
        {
            bool areEqual = false;

            if (obj is Account)
            {
                Account that = obj as Account;

                areEqual =
                    this.Id == that.Id &&
                    this.Agency == that.Agency &&
                    this.Number == that.Number &&
                    this.OwnerId == that.OwnerId;
            }

            return areEqual;
        }
    }
}
