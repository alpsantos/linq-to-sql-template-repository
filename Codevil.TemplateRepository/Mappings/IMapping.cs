using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;

namespace Codevil.TemplateRepository.Mappings
{
    public interface IMapping
    {
        string TableName { get; }
        object CreateRow(object entity);
        object CreateEntity(object row);
        void UpdateRow(object row, object entity);
        void UpdateEntity(object entity, object row);
    }

    public interface IMapping<TRow, TEntity> : IMapping
        where TEntity : new()
    {
        TRow Create(TEntity entity);
        TEntity Create(TRow row);
        void UpdateRow(TRow row, TEntity entity);
        void UpdateEntity(TEntity entity, TRow row);
    }

    public abstract class Mapping<TRow, TEntity> : IMapping<TRow, TEntity>
        where TEntity : new()
    {
        public string TableName
        {
            get
            {
                var attributes = typeof (TRow).GetCustomAttributes(typeof(TableAttribute), false);
                if (attributes.Length == 0)
                {
                    throw new InvalidRowTypeException(typeof(TRow));
                }

                var first = attributes.First() as TableAttribute;
                return first.Name;
            }
        }

        public object CreateRow(object entity)
        {
            return Create((TEntity)entity);
        }

        public object CreateEntity(object row)
        {
            return Create((TRow)row);
        }

        public void UpdateRow(object row, object entity)
        {
            UpdateRow((TRow)row, (TEntity)entity);
        }

        public void UpdateEntity(object entity, object row)
        {
            UpdateEntity((TEntity)entity, (TRow)row);
        }

        public virtual TRow Create(TEntity entity)
        {
            var instance = (TRow) Activator.CreateInstance(typeof(TRow));
            UpdateRow(instance, entity);
            return instance;
        }
        public virtual TEntity Create(TRow row)
        {
            var instance = (TEntity) Activator.CreateInstance(typeof(TEntity));
            UpdateEntity(instance, row);
            return instance;
        }

        public abstract void UpdateRow(TRow row, TEntity entity);
        public abstract void UpdateEntity(TEntity entity, TRow row);
    }
}