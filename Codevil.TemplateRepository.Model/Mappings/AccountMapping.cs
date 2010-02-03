using Codevil.TemplateRepository.Data;
using Codevil.TemplateRepository.Mappings;
using Codevil.TemplateRepository.Model.Entities;

namespace Codevil.TemplateRepository.Model.Mappings
{
    public class AccountMapping : Mapping<ACCOUNT, Account>
    {
        public override void UpdateRow(ACCOUNT row, Account entity)
        {
            row.Agency = entity.Agency;
            row.Number = entity.Number;
            row.OwnerId = entity.OwnerId;
        }

        public override void UpdateEntity(Account entity, ACCOUNT row)
        {
            entity.Id = row.Id;
            entity.Agency = row.Agency;
            entity.Number = row.Number;
            entity.OwnerId = row.OwnerId;
        }
    }
}
