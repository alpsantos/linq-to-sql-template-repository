using System;
using Codevil.TemplateRepository.Factories;
using Codevil.TemplateRepository.Test.Mappings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Codevil.TemplateRepository.Test.Factories
{
    [TestClass]
    public class EntityFactoryTest
    {
        [TestMethod]
        public void ShouldReturnTheCorrectMappingBasedOnTheType()
        {
            var intToStringMapping = new DummyIntToDummyStringMapping();
            var doubleToDateTimeMapping = new DoubleToDateTimeMapping();
            EntityMappings.AddMapping(intToStringMapping);
            EntityMappings.AddMapping(doubleToDateTimeMapping);

            Assert.AreSame(intToStringMapping, EntityMappings.GetMappingForEntity(typeof(DummyString)));
            Assert.AreSame(doubleToDateTimeMapping, EntityMappings.GetMappingForEntity(typeof(DateTime)));
        }
    }
}
