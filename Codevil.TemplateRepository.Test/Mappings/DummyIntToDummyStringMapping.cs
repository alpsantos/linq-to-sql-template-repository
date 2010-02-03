using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Test.Mappings
{
    public class DummyIntToDummyStringMapping : Mapping<DummyInt, DummyString>
    {
        public override void UpdateRow(DummyInt row, DummyString entity)
        {
            row.Int = int.Parse(entity.String);
        }

        public override void UpdateEntity(DummyString entity, DummyInt row)
        {
            entity.String = row.Int.ToString();
        }
    }
}