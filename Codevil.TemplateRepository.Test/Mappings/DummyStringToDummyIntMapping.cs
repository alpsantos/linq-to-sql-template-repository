using System;
using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Test.Mappings
{
    public class DummyStringToDummyIntMapping : Mapping<DummyString, DummyInt>
    {
        public override void UpdateRow(DummyString row, DummyInt entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateEntity(DummyInt entity, DummyString row)
        {
            throw new NotImplementedException();
        }
    }
}
