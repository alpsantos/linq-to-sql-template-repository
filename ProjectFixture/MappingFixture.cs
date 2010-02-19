using System;
using Codevil.TemplateRepository.Mappings;

namespace ProjectFixture
{
    public class MappingFixture : Mapping<DataFixture, EntityFixture>
    {
        public override void UpdateRow(DataFixture row, EntityFixture entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateEntity(EntityFixture entity, DataFixture row)
        {
            throw new NotImplementedException();
        }
    }

    public class EntityFixture {}

    public class DataFixture {}
}
