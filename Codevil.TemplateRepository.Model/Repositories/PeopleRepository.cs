using System.Data.Linq;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public class PeopleRepository : Repository<PERSON, Person>
    {
        protected override PERSON FindEntity(Person entity, DataContext context)
        {
            return FindSingle(p => p.Id == entity.Id, context);
        }
    }
}
