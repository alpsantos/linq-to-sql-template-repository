using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Handlers;
using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Repositories
{
    /// <summary>
    /// <para>
    /// This is the base implementation of the IRepository interface for a generic type of Row and Entity
    /// </para>
    /// <para>
    /// It must be inherited and customized to match the specific needs of each type of entities in the model
    /// </para>
    /// </summary>
    /// <typeparam name="TRow"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TRow, TEntity> : IRepository<TRow, TEntity>
        where TRow : class
        where TEntity : class, new()
    {
        private readonly IMapping<TRow, TEntity> _mapping;

        public Repository(IDataContextFactory dataContextFactory, IRowFactory rowFactory)
            : this(dataContextFactory)
        {
            RowFactory = rowFactory;
        }

        public Repository(IDataContextFactory dataContextFactory)
        {
            DataContextFactory = dataContextFactory;
            RowFactory = new RowFactory();
            AutoRollbackOnError = true;
            _mapping = (IMapping<TRow, TEntity>)EntityMappings.GetMappingForEntity(typeof (TEntity));
        }

        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using an auto-commit transaction
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be persisted</param>
        public virtual void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            using (var context = DataContextFactory.Create())
            {
                var retrievedRow = FindEntity(entity, context);

                if (retrievedRow != null)
                {
                    Update(retrievedRow, entity, context);
                }
                else
                {
                    Create(entity, context);
                }
            }
        }

        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using an transaction to handle the transaction
        /// </para>
        /// <para>
        /// You can call it multiple times, for different repositories and entities
        /// and as long as you pass the same instance of a transaction as a parameter,
        /// all operations will be enclosed in the same transaction. After that, you
        /// can decide to commit or rollback the transaction
        /// </para>
        /// </summary>
        /// <param name="entity">The entity that is going to be persisted</param>
        /// <param name="transaction">The transaction in which the operation will take place</param>
        public virtual void Save(TEntity entity, Transaction transaction)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            var context = transaction.DataContext;

            try
            {
                Save(entity, context);
            }
            catch
            {
                if (AutoRollbackOnError)
                {
                    transaction.Rollback();
                }

                throw;
            }
        }

        public virtual TEntity FindSingle(Expression<Func<TRow, bool>> exp)
        {
            using (var context = DataContextFactory.Create())
            {
                var row = FindSingle(exp, context);

                return row == null
                           ? null
                           : _mapping.Create(row);
            }
        }

        public virtual TEntity FindSingle(Expression<Func<TRow, bool>> exp, Transaction transaction)
        {
            var context = transaction.DataContext;
            var row = FindSingle(exp, context);

            return row == null
                       ? null
                       : _mapping.Create(row);
        }

        public virtual IList<TEntity> Find(Expression<Func<TRow, bool>> exp, Transaction transaction)
        {
            var context = transaction.DataContext;

            IList<TEntity> list = new List<TEntity>();

            list = ToEntity(Find(exp, context));

            return list;
        }

        public virtual IList<TEntity> Find(Expression<Func<TRow, bool>> exp)
        {
            using (var context = DataContextFactory.Create())
            {
                return ToEntity(Find(exp, context));
            }
        }

        public virtual void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            using (var context = DataContextFactory.Create())
            {
                Delete(entity, context);
            }
        }

        public virtual void Delete(TEntity entity, Transaction transaction)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(
                    "entity", String.Format(CultureInfo.CurrentCulture, "Entity can't be null"));
            }

            var context = transaction.DataContext;

            try
            {
                Delete(entity, context);
            }
            catch
            {
                if (AutoRollbackOnError)
                {
                    transaction.Rollback();
                }

                throw;
            }
        }

        public IDataContextFactory DataContextFactory { get; set; }
        public IRowFactory RowFactory { get; set; }
        public bool AutoRollbackOnError { get; set; }

        /// <summary>
        /// <remarks>
        /// This method must be overriden
        /// </remarks>
        /// <para>
        /// This method should setup the data
        /// from the entity to the row that is going to be inserted or updated
        /// </para>
        /// <para>
        /// This method is called on the very beginning of every save (create/update)
        /// operation
        /// </para>
        /// </summary>
        /// <param name="row">The row that will be inserted</param>
        /// <param name="entity">The entity that holds the information</param>
        protected abstract void BeforeSave(TRow row, TEntity entity);

        /// <summary>
        /// <para>
        /// This method will persist (create or update) an entity on the database
        /// using a specific context. This way, you can manually setup a context
        /// for using a specific kind of transaction and/or isolation level
        /// </para>
        /// <param name="entity">The entity that is going to be persisted</param>
        /// <param name="context">The context in which the operation will take place</param>
        public virtual void Save(TEntity entity, DataContext context)
        {
            var retrievedRow = FindEntity(entity, context);

            if (retrievedRow != null)
            {
                Update(retrievedRow, entity, context);
            }
            else
            {
                Create(entity, context);
            }
        }

        /// <summary>
        /// This method is called on the very end of every Save operation
        /// </summary>
        /// <param name="row">The row that was persisted</param>
        /// <param name="entity">The entity that was used as base for the save operation</param>
        protected virtual void AfterSave(TRow row, TEntity entity) {}

        /// <summary>
        /// This method is called at the beginning of the Update operation (after the BeforeSave method)
        /// </summary>
        /// <param name="row">The row that will be persisted</param>
        /// <param name="entity">The entity that will be used as base for the update operation</param>
        protected virtual void BeforeUpdate(TRow row, TEntity entity)
        {
            BeforeSave(row, entity);
        }

        /// <summary>
        /// <para>
        /// This method will be called when on Save, the entity could be found on the database
        /// therefore updating an existing entry
        /// </para>
        /// </summary>
        /// <param name="row">The row that is going to be updated</param>
        /// <param name="entity">The entity which hold the new data</param>
        /// <param name="context">The context in which the operation is going to take place</param>
        protected virtual void Update(TRow row, TEntity entity, DataContext context)
        {
            BeforeUpdate(row, entity);
            context.SubmitChanges();
            AfterUpdate(row, entity);
        }

        /// <summary>
        /// This method is called at the end of the Update operation (before the AfterSave method)
        /// </summary>
        /// <param name="row">The new row that was persisted</param>
        /// <param name="entity">The entity that was used as base for the update operation</param>
        protected virtual void AfterUpdate(TRow row, TEntity entity)
        {
            AfterSave(row, entity);
        }

        /// <summary>
        /// This method is called at the beginning of the Create operation (after the BeforeSave method)
        /// </summary>
        /// <param name="row">The new row that will be persisted</param>
        /// <param name="entity">The entity that will be used as base for the create operation</param>
        protected virtual void BeforeCreate(TRow row, TEntity entity)
        {
            BeforeSave(row, entity);
        }

        /// <summary>
        /// <para>
        /// This method will be called when on Save, the entity could not be found on the database
        /// therefore creating a new entry
        /// </para>
        /// </summary>
        /// <param name="entity">The entity which hold the new data</param>
        /// <param name="context">The context in which the operation is going to take place</param>
        protected virtual void Create(TEntity entity, DataContext context)
        {
            var row = _mapping.Create(entity);
            var table = (Table<TRow>)RowFactory.CreateTable(typeof (TRow), context);

            BeforeCreate(row, entity);

            table.InsertOnSubmit(row);
            context.SubmitChanges();

            AfterCreate(row, entity);
        }

        /// <summary>
        /// This method is called at the end of the Create operation (before the AfterSave method)
        /// </summary>
        /// <param name="row">The new row that was persisted</param>
        /// <param name="entity">The entity that was used as base for the create operation</param>
        protected virtual void AfterCreate(TRow row, TEntity entity)
        {
            AfterSave(row, entity);
        }

        protected abstract TRow FindEntity(TEntity entity, DataContext context);

        protected virtual TRow FindSingle(Expression<Func<TRow, bool>> exp, DataContext context)
        {
            var rows = Find(exp, context);

            if (rows.Count > 1)
            {
                throw new InvalidOperationException("Query matches more than one entry");
            }

            return rows.Count == 0
                       ? null
                       : rows.Single();
        }

        protected virtual IList<TRow> Find(Expression<Func<TRow, bool>> exp, DataContext context)
        {
            var data = (from row in context.GetTable<TRow>()
                        select row);

            return data.Where(exp).ToList();
        }

        public virtual void BeforeDelete(TRow row, TEntity entity) {}

        public virtual void Delete(TEntity entity, DataContext context)
        {
            var row = FindEntity(entity, context);

            if (row != null)
            {
                var table = (Table<TRow>)RowFactory.CreateTable(typeof (TRow), context);

                BeforeDelete(row, entity);

                table.DeleteOnSubmit(row);
                context.SubmitChanges();

                AfterDelete(row, entity);
            }
        }

        public virtual void AfterDelete(TRow row, TEntity entity) {}

        /// <summary>
        /// This method converts a list of database entries to its specific kind of entity
        /// using the EntityMappings configured for the repository
        /// </summary>
        /// <param name="list">The list of rows to be converted</param>
        /// <returns>The corresponding list of entities</returns>
        protected virtual IList<TEntity> ToEntity(IList<TRow> list)
        {
            IList<TEntity> entityList = new List<TEntity>();

            foreach (var item in list)
            {
                entityList.Add(_mapping.Create(item));
            }

            return entityList;
        }
    }
}