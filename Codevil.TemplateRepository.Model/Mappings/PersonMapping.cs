using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Mappings;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Model.Mappings
{
    public class PersonMapping : Mapping<PERSON, Person>
    {
        public override void UpdateRow(PERSON row, Person entity)
        {
            row.Document = entity.Document;
            row.Name = entity.Name;
            row.Email = entity.Email;
        }

        public override void UpdateEntity(Person entity, PERSON row)
        {
            entity.Id = row.Id;
            entity.Name = row.Name;
            entity.Document = row.Document;
            entity.Email = row.Email;
        }
    }
}