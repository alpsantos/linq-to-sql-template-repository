using System;
using Codevil.TemplateRepository.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectFixture;

namespace Codevil.TemplateRepository.Test.Factories
{
    [TestClass]
    public class EntityMappingsTest
    {
        [TestMethod]
        [ExpectedException(typeof(MappingNotFoundException))]
        public void ShouldThrowMappingNotFoundExceptionWhenMappingIsNotRegistered()
        {
            EntityMappings.GetMappingForEntity(typeof(String));
        }

        [TestMethod]
        public void ShouldAddMappingsFromAGivenTypeAssembly()
        {
            EntityMappings.AddFromTypeAssembly<MappingFixture>();

            EntityMappings.GetMappingForEntity<EntityFixture>();
        }
    }
}
