using System;
using System.Collections.Generic;
using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Factories
{
    public class EntityMappings
    {
        private static IDictionary<Type, IMapping> mappings;

        static EntityMappings()
        {
            mappings = new Dictionary<Type, IMapping>();
        }

        public static void AddMapping<TRow, TEntity>(IMapping<TRow, TEntity> mapping) 
            where TEntity : new()
        {
            mappings[typeof(TEntity)] = mapping;
        }

        public static IMapping GetMappingForEntity(Type typeOfEntity)
        {
            if (!mappings.ContainsKey(typeOfEntity))
            {
                throw new MappingNotFoundException(string.Format("Mapping for entity of type '{0}' not found.", typeOfEntity.FullName));
            }

            return mappings[typeOfEntity];
        }
    }
}
