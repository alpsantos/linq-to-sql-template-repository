using System.Data.Linq;
using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Model.Repositories
{
    public class AccountsRepository : Repository<ACCOUNT, Account>
    {
        protected override ACCOUNT FindEntity(Account entity, DataContext context)
        {
            return FindSingle(a => a.Id == entity.Id, context);
        }
    }
}
