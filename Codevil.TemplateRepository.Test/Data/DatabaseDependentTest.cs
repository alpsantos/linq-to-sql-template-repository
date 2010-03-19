using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Migrations;
using Codevil.TemplateRepository.Model.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Data
{
    public class DatabaseDependentTest
    {
        [TestInitialize]
        public void Setup()
        {
            EntityMappings.AddMapping(new PersonMapping());
            EntityMappings.AddMapping(new AccountMapping());
            Migrator.Down();
            Migrator.Up();
        }

        [TestCleanup]
        public void Cleanup()
        {
            Migrator.Down();
            Migrator.Up();
        }
    }
}
