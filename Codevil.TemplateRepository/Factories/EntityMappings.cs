using System;
using System.Collections.Generic;
using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Factories
{
    public class EntityMappings
    {
        private static IDictionary<Type, IMapping> EntityToRowMappingDictionary;

        static EntityMappings()
        {
            EntityToRowMappingDictionary = new Dictionary<Type, IMapping>();
        }

        public static void AddMapping<TRow, TEntity>(IMapping<TRow, TEntity> mapping) 
            where TEntity : new()
        {
            EntityToRowMappingDictionary[typeof(TEntity)] = mapping;
        }

        public static IMapping GetMappingForEntity(Type typeOfEntity)
        {
            return EntityToRowMappingDictionary[typeOfEntity];
        }
    }
}
