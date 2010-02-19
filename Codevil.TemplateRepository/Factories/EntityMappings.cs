using System;
using System.Collections.Generic;
using System.Linq;
using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Factories
{
    public class EntityMappings
    {
        private static readonly IDictionary<Type, IMapping> Mappings;

        static EntityMappings()
        {
            Mappings = new Dictionary<Type, IMapping>();
        }

        public static void AddMapping<TRow, TEntity>(IMapping<TRow, TEntity> mapping) 
            where TEntity : new()
        {
            Mappings[typeof(TEntity)] = mapping;
        }

        public static IMapping GetMappingForEntity<Type>()
        {
            return GetMappingForEntity(typeof (Type));
        }

        public static IMapping GetMappingForEntity(Type typeOfEntity)
        {
            if (!Mappings.ContainsKey(typeOfEntity))
            {
                throw new MappingNotFoundException(string.Format("Mapping for entity of type '{0}' not found.", typeOfEntity.FullName));
            }

            return Mappings[typeOfEntity];
        }

        public static void AddFromTypeAssembly<Type>()
        {
            var mappingTypes = typeof (Type).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IMapping)));
            foreach (var mappingType in mappingTypes)
            {
                var genericArguments = mappingType.BaseType.GetGenericArguments();
                Mappings[genericArguments[1]] = (IMapping)Activator.CreateInstance(mappingType);
            }
        }
    }
}
