using System;
using Codevil.TemplateRepository.Mappings;

namespace Codevil.TemplateRepository.Test.Factories
{
    class DoubleToDateTimeMapping : Mapping<double, DateTime>
    {
        public override double Create(DateTime entity)
        {
            throw new NotImplementedException();
        }

        public override DateTime Create(double row)
        {
            throw new NotImplementedException();
        }

        public override void UpdateRow(double row, DateTime entity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateEntity(DateTime entity, double row)
        {
            throw new NotImplementedException();
        }
    }
}
